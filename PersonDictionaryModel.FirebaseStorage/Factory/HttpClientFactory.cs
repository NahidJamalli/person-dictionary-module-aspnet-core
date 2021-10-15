using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using PersonDictionaryModel.FirebaseStorage.Core;

namespace PersonDictionaryModel.FirebaseStorage.Factory
{
    public static class HttpClientFactory
    {
        public static async Task<HttpClient> CreateHttpClientAsync(this FirebaseStorageOptions options)
        {
            var client = new HttpClient();

            if (options.HttpClientTimeout != default(TimeSpan))
            {
                client.Timeout = options.HttpClientTimeout;
            }

            if (options.AuthTokenAsyncFactory != null)
            {
                var auth = await options.AuthTokenAsyncFactory().ConfigureAwait(false);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Firebase", auth);
            }

            return client;
        }
    }
}
