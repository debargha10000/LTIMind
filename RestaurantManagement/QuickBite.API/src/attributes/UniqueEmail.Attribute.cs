using System.ComponentModel.DataAnnotations;
using QuickBite.API.src.config;

namespace QuickBite.API.src.attributes;

public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var dbContext = (AppDbContext)validationContext.GetService(typeof(AppDbContext))!;
        var email = (string)value!;
        var user = dbContext.Users.FirstOrDefault(u => u.Email == email);
        if (user == null)
        {
            return ValidationResult.Success;
        }
        return new ValidationResult("Email already exists");
    }
}
