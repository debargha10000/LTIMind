using System;
using System.ComponentModel.DataAnnotations;
using QuickBite.API.src.attributes;

namespace QuickBite.API.src.models.dto.user;

public class UserCreateRequestBodyDTO
{

    [Required(ErrorMessage = "Name is Required")]
    [Length(minimumLength: 3, maximumLength: 50, ErrorMessage = "Name should be between 3 and 50 characters")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Address is Required")]
    public required string Address { get; set; }

    [Required(ErrorMessage = "Phone Number is Required")]
    [Phone(ErrorMessage = "Invalid Phone Number")]
    public required string ContactNumber { get; set; }

    [DataType(DataType.Password, ErrorMessage = "Invalid Password")]
    [Required(ErrorMessage = "Password is Required")]
    public required string Password { get; set; }

    [Required(ErrorMessage = "Email is Required")]
    [EmailAddress(ErrorMessage = "Invalid Email Number")]
    [UniqueEmail(ErrorMessage = "This Email is already registered")]
    public required string Email { get; set; } // public UserRole Role {get; set;) UserRole.Customer;
}
