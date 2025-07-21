namespace SalesApi.DTOs.Output;

public record class CustomerOutputDto(int Id, string CustomerName, string? City, IEnumerable<int> OrderIds);