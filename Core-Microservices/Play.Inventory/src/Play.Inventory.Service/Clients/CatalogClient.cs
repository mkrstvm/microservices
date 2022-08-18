using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Play.Inventory.Service.DTOs;

namespace Play.Inventory.Service.Clients
{
    public class CatalogClient
    {
        private readonly HttpClient _httpClient;
        
        public CatalogClient(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<IReadOnlyCollection<CategoryItemDto>> GetCategoryItemsAsunc()
        {
            var items = await _httpClient.GetFromJsonAsync<IReadOnlyCollection<CategoryItemDto>>("/items");
            return items;
        }
    }
}