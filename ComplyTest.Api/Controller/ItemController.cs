using System.Collections.Concurrent;
using System.Net.Mime;
using ComplyTest.Api.Model;
using ComplyTest.Core.Entity;
using ComplyTest.Core.Service;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Caching.Memory;

namespace ComplyTest.Api.Controller;

[ApiController]
[Route("api/item")]
public class ItemController : ControllerBase
{
    private readonly IItemService _service;
    private readonly MemoryCache _cache;

    public ItemController(IItemService service, MemoryCache cache)
    {
        _service = service;
        _cache = cache;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Item>), StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
    public ActionResult List()
    {
        var items = _service.GetItemList().ToList().Select(item => new ItemDetailsDto
            {
                Id = item.Id,
                Name = item.Name,
                DateCreated = item.DateCreated,
                DateModified = item.DateModified,
            });

        return Ok(items);
    }

    [HttpGet("factorial")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
    public IEnumerable<ItemDetailsDto> Factorial()
    {
        var items = _service.GetItemList().Select((item, index) => new ItemDetailsDto
            {   
                Id = item.Id,
                Name =item.Name,
                DateCreated = item.DateCreated,
                DateModified = item.DateModified,
                Row = index + 1,
            });
        var results = new ConcurrentBag<ItemDetailsDto>();
        var tasks = items
            .AsParallel()
            .AsUnordered()
            .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
            .WithDegreeOfParallelism(items.Count());

        tasks.ForAll(item =>
        {
            item.Factorial = _cache.GetOrCreate(item.Id, options =>
            {
                options.Priority = CacheItemPriority.Normal;
                options.Size = 1;

                return _service.CalculateFactorial(item.Row);
            });

            results.Add(item);
        });

        return results.OrderBy(item => item.Row);
    }

    [HttpGet("{id}", Name = nameof(Details))]
    [ProducesResponseType(typeof(Item), StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound, MediaTypeNames.Text.Plain)]
    public async Task<ActionResult> Details([FromRoute] Guid id)
    {
        var item = await _service.GetItemDetails(id);

        return item == null ? NotFound("Item not found") : Ok(item);
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Item), StatusCodes.Status201Created, MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ValidationProblem), StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)]
    public async Task<ActionResult> Create([FromBody] ItemSaveDto dto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var item = await _service.AddItem(dto.ToEntity());

        return CreatedAtAction(nameof(Details), new { id = item.Id }, item);
    }
    
    [HttpPut("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Item), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound, MediaTypeNames.Text.Plain)]
    [ProducesResponseType(typeof(ValidationProblem), StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)]
    public async Task<ActionResult> Update([FromRoute] [Exists] Guid id, [FromBody] ItemSaveDto dto)
    {
        if (ModelState["id"]!.ValidationState != ModelValidationState.Valid)
        {
            return NotFound($"{nameof(Item)} not found for update");
        }
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        await _service.UpdateItem(id, dto.ToEntity());

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Item), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound, MediaTypeNames.Text.Plain)]
    public async Task<ActionResult> Delete([FromRoute] [Exists] Guid id)
    {
        if (ModelState["id"]!.ValidationState != ModelValidationState.Valid)
        {
            return NotFound($"{nameof(Item)} not found for delete");
        }

        await _service.DeleteItem(id);
        _cache.Clear();

        return NoContent();
    }
}
