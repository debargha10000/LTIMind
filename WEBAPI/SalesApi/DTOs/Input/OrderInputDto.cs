using System;

namespace SalesApi.DTOs.Input;

public record OrderInputDto(int CustomerId, DateOnly OrderDate);
