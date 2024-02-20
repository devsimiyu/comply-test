using System.Net.Mime;
using ComplyTest.Api.Model;
using ComplyTest.Core.Entity;
using ComplyTest.Core.Service;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ComplyTest.Api.Controller;

[ApiController]
[Route("api/item")]
public class ItemController : ControllerBase
{
    private readonly IItemService _service;

    public ItemController(IItemService service)
        => _service = service;

    [HttpGet]
    [ProducesResponseType<List<Item>>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
    public ActionResult List()
    {
        var items = _service.GetItemList().ToList();

        return Ok(items);
    }

    [HttpGet("{id}", Name = nameof(Details))]
    [ProducesResponseType<Item>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
    [ProducesResponseType<string>(StatusCodes.Status404NotFound, MediaTypeNames.Text.Plain)]
    public async Task<ActionResult> Details([FromRoute] Guid id)
    {
        var item = await _service.GetItemDetails(id);

        return item == null ? NotFound("Item not found") : Ok(item);
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType<Item>(StatusCodes.Status201Created, MediaTypeNames.Application.Json)]
    [ProducesResponseType<ValidationProblem>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)]
    public async Task<ActionResult> Create([FromBody] ItemDto dto)
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
    [ProducesResponseType<Item>(StatusCodes.Status204NoContent)]
    [ProducesResponseType<string>(StatusCodes.Status404NotFound, MediaTypeNames.Text.Plain)]
    [ProducesResponseType<ValidationProblem>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)]
    public async Task<ActionResult> Update([FromRoute] [Exists] Guid id, [FromBody] ItemDto dto)
    {
        if (!ModelState.IsValid)
        {
            var exists = ModelState["id"]!;

            return exists.ValidationState != ModelValidationState.Valid
                ? NotFound($"{nameof(Item)} not found for update")
                : ValidationProblem(ModelState);
        }

        await _service.UpdateItem(id, dto.ToEntity());

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType<Item>(StatusCodes.Status204NoContent)]
    [ProducesResponseType<string>(StatusCodes.Status404NotFound, MediaTypeNames.Text.Plain)]
    public async Task<ActionResult> Delete([FromRoute] [Exists] Guid id)
    {
        var exists = ModelState["id"]!;

        if (exists.ValidationState != ModelValidationState.Valid)
        {
            return NotFound($"{nameof(Item)} not found for delete");
        }

        await _service.DeleteItem(id);

        return NoContent();
    }
}
