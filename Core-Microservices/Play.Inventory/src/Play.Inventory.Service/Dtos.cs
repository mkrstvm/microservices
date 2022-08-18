using System;

namespace Play.Inventory.Service.DTOs
{
    public record GrantItemDto(Guid userId, Guid catelogId, int quantity);
    public record InventorItemDto(Guid catelogId, string name, decimal price, int quantity, DateTimeOffset aquiredDate);
    public record CategoryItemDto(Guid id, string name, decimal price);
}