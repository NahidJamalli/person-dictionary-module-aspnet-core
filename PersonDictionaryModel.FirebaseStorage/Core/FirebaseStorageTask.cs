using Newtonsoft.Json;
using PersonDictionaryModel.FirebaseStorage.Factory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace PersonDictionaryModel.FirebaseStorage.Core
{
    public class FirebaseStorageTask
    {
        private readonly Task<string> uploadTask;

        public FirebaseStorageTask(FirebaseStorageOptions options, string url, string downloadUrl, Stream stream, CancellationToken cancellationToken, string mimeType = null)
        {
            TargetUrl = url;
            uploadTask = UploadFile(options, url, downloadUrl, stream, cancellationToken, mimeType);
        }

        public string TargetUrl
        {
            get;
            private set;
        }

        public TaskAwaiter<string> GetAwaiter()
        {
            return uploadTask.GetAwaiter();
        }

        private async Task<string> UploadFile(FirebaseStorageOptions options, string url, string downloadUrl, Stream stream, CancellationToken cancellationToken, string mimeType = null)
        {
            var responseData = "N/A";

            try
            {
                using (var client = await options.CreateHttpClientAsync())
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, url)
                    {
                        Content = new StreamContent(stream)
                    };

                    if (!string.IsNullOrEmpty(mimeType))
                    {
                        request.Content.Headers.ContentType = new MediaTypeHeaderValue(mimeType);
                    }

                    var response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
                    responseData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    response.EnsureSuccessStatusCode();
                    var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseData);

                    return downloadUrl + data["downloadTokens"];
                }
            }
            catch (TaskCanceledException)
            {
                if (options.ThrowOnCancel)
                {
                    throw;
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                throw new FirebaseStorageException(url, responseData, ex);
            }
        }
    }
}
