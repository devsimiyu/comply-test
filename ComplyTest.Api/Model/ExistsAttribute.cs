using System.ComponentModel.DataAnnotations;
using ComplyTest.Core.Repository;

namespace ComplyTest.Api.Model;

public class ExistsAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var repository = validationContext.GetRequiredService<IItemRepository>();
        var exists = repository.GetItemList().Any(x => x.Id.Equals(value));

        return exists ? ValidationResult.Success : new ValidationResult(string.Empty);
    }
}
