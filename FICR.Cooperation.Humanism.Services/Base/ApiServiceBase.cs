using FICR.Cooperation.Humanism.Services.Contracts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FICR.Cooperation.Humanism.Services.Base
{
    public class ApiServiceBase : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseEndpoint;

        public ApiServiceBase(HttpClient httpClient, string baseEndpoint)
        {
            _httpClient = httpClient;
            _baseEndpoint = baseEndpoint;
            _httpClient.BaseAddress = new Uri(_baseEndpoint);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "EAAO3oJZA25QYBOZB5VW7olg18Th6mZCZCSqYmbqFbTTxKDoOZBZBwlMpPwlnt65IZBQoOBR5v32k9BMZAuoXMxFo9s1pJMXJ1lQQOTzUwd9o1aglAAS0Y673RfTn6EryaK35znZA5ZBGZBoD2kV6IVlgVzWTZAVbvpkYoeCkM6YoRZAzltpP6jJQGZBt5A2iy0p8Kxc3NqaB2KUz53SIlFPsMOpx0ZD"); // Use a variável de ambiente ou um serviço de gerenciamento de segredos
           
        }

        public async Task CallApiAsync(string endpoint, object parameters)
        {
            
                var fullUrl = _baseEndpoint + endpoint;

                var jsonContent = JsonConvert.SerializeObject(parameters);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(fullUrl, content);

           
            var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Status Code: {response.StatusCode}");
                Console.WriteLine($"Response Body: {responseBody}");

                response.EnsureSuccessStatusCode(); // Throw if not success
            
        }

    }
}
