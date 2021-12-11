using System;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;

using Xamarin.Essentials;

using Company.Mobile.Helpers;

namespace Company.Mobile.Services
{
    public class ApiService
    {
        private static JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        private static HttpClient CreateClient(string url)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        private static HttpClient apiClient = CreateClient(Constants.ApiEndpoint);

        private static async Task<HttpResponseMessage> SendApiRequest(
                HttpMethod method, string controller, HttpContent content)
        {
            var message = new HttpRequestMessage(method, controller);

            if (content != null)
                message.Content = content;

            return await apiClient.SendAsync(message);
        }

        public static async Task<List<T>> GetItems<T>(string controller) where T : new()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                var response = await SendApiRequest(HttpMethod.Get, controller, null);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<List<T>>(content, options);
                }
            }

            return default;
        }

        public static async Task<T> GetItem<T>(string controller) where T : new()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                var response = await SendApiRequest(HttpMethod.Get, controller, null);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<T>(content, options);
                }
            }

            return default;
        }

        public static async Task<bool> AddItem<T>(string controller, T item) where T : new()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                var body = JsonSerializer.Serialize(item);
                var content = new StringContent(body, Encoding.UTF8, "application/json");

                var response = await SendApiRequest(HttpMethod.Post, controller, content);
                return response.IsSuccessStatusCode;
            }

            return default;
        }

        public static async Task<bool> UpdateItem<T>(string controller, T item) where T : new()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                var body = JsonSerializer.Serialize(item);
                var content = new StringContent(body, Encoding.UTF8, "application/json");

                var response = await SendApiRequest(HttpMethod.Put, controller, content);
                return response.IsSuccessStatusCode;
            }

            return default;
        }

        public static async Task<bool> DeleteItem(string controller, bool needsAuth = false)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                var response = await SendApiRequest(HttpMethod.Delete, controller, null);
                return response.IsSuccessStatusCode;
            }

            return default;
        }
    }
}
