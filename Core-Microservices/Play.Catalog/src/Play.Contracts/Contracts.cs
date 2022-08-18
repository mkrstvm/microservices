using System;

namespace Play.Contracts
{
    public record CatalogItemCreated(Guid ItemID, string name, decimal price);
    public record CatalogItemUpdated(Guid ItemID, string name, decimal price);
    public record CatalogItemDeleted(Guid ItemID);
}