using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Fls.Supervision.Test.Integration.Helpers
{
    public static class TestRequestHelper
    {
        public static async Task<T> PostJsonAsync<T>(this HttpClient client, string endpoint, object postObject, T respObject, string token = null)
        {
            var responseString = await PostJsonAsync(client, endpoint, postObject, token);
            return JsonSerializer.Deserialize<T>(responseString);
        }

        public static async Task<T> PostFormAsync<T>(this HttpClient client, string endpoint, KeyValuePair<string, string>[] formData, T respObject, string token = null)
        {
            var responseString = await PostFormAsync(client, endpoint, formData, token);
            return JsonSerializer.Deserialize<T>(responseString);
        }

        public static async Task<T> GetAsync<T>(this HttpClient client, Func<string> getEndpoint, T respObject, string token = null)
        {
            var responseString = await GetAsync(client, getEndpoint, token);
            return JsonSerializer.Deserialize<T>(responseString);
        }

        public static async Task<string> PostJsonAsync(this HttpClient client, string endpoint, object postObject, string token = null)
        {
            const string jsonContentType = "application/json";
            var json = JsonSerializer.Serialize(postObject);

            return await ReadStringResponseAsync(client, async x =>
            {
                x.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(jsonContentType));
                return await x.PostAsync(endpoint, new StringContent(json, Encoding.UTF8, jsonContentType));
            }, token);
        }

        public static async Task<string> PostFormAsync(this HttpClient client, string endpoint, IEnumerable<KeyValuePair<string, string>> formData, string token = null)
        {
            return await ReadStringResponseAsync(client, async x => await x.PostAsync(endpoint, new FormUrlEncodedContent(formData)), token);
        }

        public static async Task<string> GetAsync(this HttpClient client, Func<string> getEndpoint, string token = null)
        {
            return await ReadStringResponseAsync(client, async x => await x.GetAsync(getEndpoint()), token);
        }

        private static async Task<string> ReadStringResponseAsync(this HttpClient client, Func<HttpClient, Task<HttpResponseMessage>> executeRequestAsync, string token = null)
        {
            if (!string.IsNullOrWhiteSpace(token))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using (var response = await executeRequestAsync(client))
            {
                using (var responseContent = response.Content)
                {
                    return await responseContent.ReadAsStringAsync();
                }
            }
        }
    }
}