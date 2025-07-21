using System.ComponentModel.DataAnnotations;

namespace QuickBite.API.src.models.dto.user;

public class UserUpdateRequestBodyDTO
{
    [Required(ErrorMessage = "Name is required")]
    [Length(minimumLength: 3, maximumLength: 50, ErrorMessage = "Name should be between 3 and 50 characters")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Address is required")]
    public required string Address { get; set; }

    [Required(ErrorMessage = "Contact Number is required")]
    [Phone(ErrorMessage = "Contact Number is not valid")]
    public required string ContactNumber { get; set; }
}
