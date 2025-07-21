using System.ComponentModel.DataAnnotations;
using QuickBite.API.src.enums;

namespace QuickBite.API.src.models.dto.menuItem;

public class MenuItemRequestBodyDTO
{
    [Required(ErrorMessage = "Item Name is required")]
    [Length(minimumLength: 3, maximumLength: 50, ErrorMessage = "Item Name must be between 3 and 50 characters")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Item Description is required")]
    [Length(minimumLength: 3, maximumLength: 100, ErrorMessage = "Item Description must be between 3 and 100 characters")]
    public required string Description { get; set; }

    [Required(ErrorMessage = "Item Diet Info is required")]
    [EnumDataType(typeof(DietType), ErrorMessage = "Invalid Diet Type")]
    public required DietType DietInfo { get; set; }

    [Required(ErrorMessage = "Item Price is required")]
    [Range(0.01, 100000, ErrorMessage = "Item Price must be between 0.01 and 10,000.00")]
    [DataType(DataType.Currency, ErrorMessage = "Invalid currency format")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Item Image URL is required")]
    [DataType(DataType.ImageUrl, ErrorMessage = "Invalid ImageURL format")]
    public required string ImageUrl { get; set; }

    [Required(ErrorMessage = "Item Availability status is required")]
    public required bool IsAvailable { get; set; } = true;
}
