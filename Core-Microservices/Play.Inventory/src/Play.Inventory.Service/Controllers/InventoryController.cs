using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Play.Common;
using Play.Inventory.Service.DTOs;
using Play.Inventory.Service.Entites;
using System.Linq;
using Play.Inventory.Service.Clients;

namespace Play.Inventory.Service.Controllers
{
    [ApiController]
    [Route("Inventories")]
    public class InventoryController : ControllerBase
    {
            private readonly IRepository<InventoryItem> itemRepository;
            private readonly CatalogClient  _client;

            public InventoryController(IRepository<InventoryItem> itemRepository,CatalogClient catalogClient)
            {
                this.itemRepository = itemRepository;
                _client = catalogClient;
            }


            [HttpGet]
            public async Task<ActionResult<IEnumerable<InventorItemDto>>> GetAsync(Guid userId)
            {
                var catalogItems = await _client.GetCategoryItemsAsunc();
                var inventoryItems = (await itemRepository.GetAllAsync(itemRepository => itemRepository.UserId == userId));

                var inventoryItemDtos = inventoryItems.Select( inventory =>
                {
                    var catalogItem = catalogItems.SingleOrDefault( item => item.id == inventory.CatelogId);
                    return inventory.AsDto(catalogItem==null? "" :  catalogItem.name, catalogItem == null ? 0 : catalogItem.price);
                });

                return Ok(inventoryItemDtos);
            }

            [HttpPost]
            public async Task<ActionResult> PostAsync(GrantItemDto dto)
            {
                var inventory = (await itemRepository.GetAllAsync(item => item.UserId == dto.userId && item.CatelogId == dto.catelogId)).FirstOrDefault();

                if(inventory ==null)
                {
                    inventory = new InventoryItem
                    {
                        CatelogId = dto.catelogId,
                        UserId = dto.userId,
                        Quantity = dto.quantity,
                        AcquiredDate = DateTimeOffset.Now
                    };

                    await itemRepository.CreateAsync(inventory);
                }
                else
                {
                    inventory.Quantity += dto.quantity;                    
                }

                return Ok();
            }


    } 
}