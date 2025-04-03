using System.Net.Http;
using System.Net.Http.Json;

namespace KooliProjekt.WinFormsApp.Api
{
    public class ApiClient : IApiClient, IDisposable
    {
        private readonly HttpClient _httpClient;

        public ApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7136/api/Category/");
        }

        public async Task<Result<List<Category>>> List()
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<Category>>("");

                if (result == null || result.Count == 0)
                {
                    return (Result<List<Category>>)Result<List<Category>>.Failure("Andmete laadimine ebaõnnestus või andmeid pole.");
                }

                return (Result<List<Category>>)Result<List<Category>>.Success();
            }
            catch (Exception ex)
            {
                return (Result<List<Category>>)Result<List<Category>>.Failure($"Viga andmete laadimisel: {ex.Message}");
            }
        }


        public async Task<Result> Save(Category category)
        {
            try
            {
                HttpResponseMessage response;
                if (category.Id == 0)
                {
                    response = await _httpClient.PostAsJsonAsync("", category);
                }
                else
                {
                    response = await _httpClient.PutAsJsonAsync(category.Id.ToString(), category);
                }

                if (!response.IsSuccessStatusCode)
                {
                    return Result.Failure($"Ошибка: {response.StatusCode}");
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }


        public async Task<Result> Delete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(id.ToString());
                if (!response.IsSuccessStatusCode)
                {
                    return Result.Failure($"Viga kustutamisel: {response.ReasonPhrase}");
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Viga kustutamisel: {ex.Message}");
            }
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}