namespace SalesApi.DTOs.Output;

public record class OrderOutputDto(int Id, int CustomerId, DateOnly OrderDate, IEnumerable<int> OrderDetailIds);