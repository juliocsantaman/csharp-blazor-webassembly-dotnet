using BlazorApp2.Models;
using System.Text.Json;

namespace BlazorApp2.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<Category>?> Get() 
        {
            var response = await _httpClient.GetAsync("v1/categories");
            var content = await response.Content.ReadAsStringAsync();
            if(!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            return JsonSerializer.Deserialize<List<Category>>(content, _jsonSerializerOptions);
        }
    }
}

public interface ICategoryService
{
    Task<List<Category>?> Get();
}