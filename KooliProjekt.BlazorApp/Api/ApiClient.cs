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
                result.Error = ex.Message;
            }

            return result;
        }

        public async Task<Result> Save(Category category)
        {
            var result = new Result();

            try
            {
                if (category.Id == 0)
                {
                    await _httpClient.PostAsJsonAsync("Category", category);
                }
                else
                {
                    await _httpClient.PutAsJsonAsync("Category/" + category.Id, category);
                }
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }

            return result;
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
                result.Error = ex.Message;
            }

            return result;
        }
    }
}