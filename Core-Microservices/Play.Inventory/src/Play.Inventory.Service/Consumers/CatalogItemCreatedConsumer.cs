using System.Threading.Tasks;
using MassTransit;
using Play.Common;
using Play.Contracts;
using Play.Inventory.Service.Entites;
using PLay.Inventory.Service.Entites;

namespace Play.Inventory.Service.Consumers
{
    public class CatalogItemCreatedConsumer : MassTransit.IConsumer<CatalogItemUpdated>
    {

        private readonly IRepository<CatalogItem> itemRepository;

        public CatalogItemCreatedConsumer(IRepository<CatalogItem> itemRepository)
        {
            this.itemRepository = itemRepository;
        }
        public async Task Consume(ConsumeContext<CatalogItemUpdated> context)
        {
            var message = context.Message;

            var item = await itemRepository.GetAsync(message.ItemId);
            
            if(item == null)
            {
                item = new CatalogItem
                {
                    Id = message.ItemId,
                    Name = message.Name,
                    //Price = message.Price
                } ;

                await itemRepository.CreateAsync(item);   
            }
            else
            {
                item.Name = message.Name;
                //item.Price = message.Price;

                await itemRepository.CreateAsync(item);
            }              
        }
    }
}