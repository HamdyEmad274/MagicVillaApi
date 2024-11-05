using MagicVillaWeb.Models;
using MagicVillaWeb.Services.IServices;
using Newtonsoft.Json;
using System.Text;

namespace MagicVillaWeb.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse responseModel { get ; set; }
        public IHttpClientFactory httpClient { get; set; }
        public BaseService(IHttpClientFactory httpClient)
        {
            this.httpClient = httpClient;
            this.responseModel = new();
        }
        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client = httpClient.CreateClient("MagicAPI");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }
                switch (apiRequest.APIType)
                {
                    case MagicVillaUtility.SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case MagicVillaUtility.SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case MagicVillaUtility.SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
                HttpResponseMessage apiResponse = null;
                apiResponse = await client.SendAsync(message);
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return APIResponse;
            }
            catch(Exception e)
            {
                var dto = new APIResponse
                {
                    ErrorMessages = new List<string>
                    {
                        Convert.ToString(e.Message)
                    },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;
            }
        }
    }
}
