using System;
using Play.Common;

namespace PLay.Inventory.Service.Entites
{
    public class CatalogItem: IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}