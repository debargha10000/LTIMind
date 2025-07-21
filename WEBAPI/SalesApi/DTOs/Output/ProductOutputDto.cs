namespace SalesApi.DTOs.Output;

public record class ProductOutputDto(int ProductId, string ProductName, string Category, decimal Price);