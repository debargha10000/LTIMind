using System;

namespace SalesApi.DTOs.Input;

public record ProductInputDto(string ProductName, string Category, decimal Price);