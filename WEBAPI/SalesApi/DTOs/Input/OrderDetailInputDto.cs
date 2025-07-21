using System;

namespace SalesApi.DTOs.Input;

public record OrderDetailInputDto(int OrderId, int ProductId, int Quantity);