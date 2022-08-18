using Play.Inventory.Service.DTOs;
using Play.Inventory.Service.Entites;

namespace Play.Inventory.Service
{
    public static class Extensions
    {
        public static InventorItemDto AsDto(this InventoryItem  dTOs, string name, decimal price)
        {
            return new InventorItemDto(dTOs.CatelogId, name, price, dTOs.Quantity,dTOs.AcquiredDate);
        }
    }
    
}