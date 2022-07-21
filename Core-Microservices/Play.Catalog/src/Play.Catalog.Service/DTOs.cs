using System;

namespace Play.Catalog.Service.DTOs
{
    public record ItemDto(Guid id, string name, decimal price);

    public record CreateItemDto(string name, decimal price);

    public record UpdateItemDto(string name, decimal price);
}