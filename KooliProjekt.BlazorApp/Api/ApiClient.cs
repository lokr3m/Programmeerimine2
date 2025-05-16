using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace KooliProjekt.BlazorApp
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7136/api/");
        }

        public async Task<Result<List<Category>>> List()
        {
            var result = new Result<List<Category>>();

            try
            {
                result.Value = await _httpClient.GetFromJsonAsync<List<Category>>("Category");
            }
            catch (Exception ex)
            {
                result.AddError("_", ex.Message);
            }

            return result;
        }

        public async Task<Result> Save(Category category)
        {
            HttpResponseMessage response;

            if (category.Id == 0)
            {
                response = await _httpClient.PostAsJsonAsync("Category", category);
            }
            else
            {
                response = await _httpClient.PutAsJsonAsync("Category/" + category.Id, category);
            }

            using (response)
            {
                if (!response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<Result>();
                    return result;
                }
            }

            return new Result();
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync("Category/" + id);
        }

        public async Task<Result<Category>> Get(int id) 
        {
            var result = new Result<Category>();

            try
            {
                result.Value = await _httpClient.GetFromJsonAsync<Category>("Category/" + id);
            }
            catch (Exception ex)
            {
                result.AddError("_", ex.Message);
            }

            return result;
        }
    }
}