using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Langchips.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Method to get data from the API
        public async Task<string> GetApiMessageAsync()
        {
            var response = await _httpClient.GetAsync("api/test");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            else
            {
                return "Error calling the API";
            }
        }
    }
}
