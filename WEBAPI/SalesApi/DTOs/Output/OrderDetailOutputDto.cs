namespace SalesApi.DTOs.Output;

public record class OrderDetailOutputDto(int OrderId, int ProductId, int Quantity);