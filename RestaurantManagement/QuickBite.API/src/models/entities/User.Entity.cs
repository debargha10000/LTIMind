using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using QuickBite.API.src.enums;
using QuickBite.API.src.models.dto.user;

namespace QuickBite.API.src.models.entities;

public class UserEntity
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [Length(minimumLength: 3, maximumLength: 50, ErrorMessage = "Name should be between 3 and 50 characters")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; } = null!;

    [Required(ErrorMessage = "Contact number is required")]
    [Phone(ErrorMessage = "Invalid phone number")]
    public required string ContactNumber { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password, ErrorMessage = "Invalid password")]
    public required string Password { get; set; }

    public required UserRole Role { get; set; }


    public UserEntity() { }

    [SetsRequiredMembers]

    public UserEntity(UserCreateRequestBodyDTO data)
    {
        this.Name = data.Name;
        this.Address = data.Address;
        this.ContactNumber = data.ContactNumber;
        this.Email = data.Email;
        // this.Role = data. Role;
        this.Password = data.Password;
    }

    [SetsRequiredMembers]
    public UserEntity(UserEntity data)
    {
        this.Id = data.Id;
        this.Name = data.Name;
        this.Address = data.Address;
        this.ContactNumber = data.ContactNumber;
        this.Email = data.Email;
        this.Role = data.Role;
        this.Password = data.Password;
    }

}
