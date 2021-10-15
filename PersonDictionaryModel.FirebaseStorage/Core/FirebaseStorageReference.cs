using Newtonsoft.Json;
using PersonDictionaryModel.FirebaseStorage.Factory;
using PersonDictionaryModel.FirebaseStorage.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PersonDictionaryModel.FirebaseStorage.Core
{
    public class FirebaseStorageReference
    {
        private const string FirebaseStorageEndpoint = "https://firebasestorage.googleapis.com/v0/b/";
        private readonly FirebaseStorage storage;
        private readonly List<string> children;

        internal FirebaseStorageReference(FirebaseStorage storage, string childRoot)
        {
            children = new List<string>();

            this.storage = storage;
            children.Add(childRoot);
        }

        public FirebaseStorageTask PutAsync(Stream stream, CancellationToken cancellationToken, string mimeType = null)
        {
            return new FirebaseStorageTask(storage.Options, GetTargetUrl(), GetFullDownloadUrl(), stream, cancellationToken, mimeType);
        }

        public FirebaseStorageTask PutAsync(Stream fileStream)
        {
            return PutAsync(fileStream, CancellationToken.None);
        }

        public async Task<MetadataDto> GetMetaDataAsync()
        {
            var data = await PerformFetch<MetadataDto>();

            return data;
        }

        public async Task<string> GetDownloadUrlAsync()
        {
            var data = await PerformFetch<Dictionary<string, object>>();

            object downloadTokens;

            if (!data.TryGetValue("downloadTokens", out downloadTokens))
            {
                throw new ArgumentOutOfRangeException($"Could not extract 'downloadTokens' property from response. Response: {JsonConvert.SerializeObject(data)}");
            }

            return GetFullDownloadUrl() + downloadTokens;
        }

        public async Task DeleteAsync()
        {
            var url = GetDownloadUrl();
            var resultContent = "N/A";

            try
            {
                using (var http = await storage.Options.CreateHttpClientAsync().ConfigureAwait(false))
                {
                    var result = await http.DeleteAsync(url).ConfigureAwait(false);

                    resultContent = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

                    result.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                throw new FirebaseStorageException(url, resultContent, ex);
            }
        }

        public FirebaseStorageReference Child(string name)
        {
            children.Add(name);
            return this;
        }

        private async Task<T> PerformFetch<T>()
        {
            var url = GetDownloadUrl();
            var resultContent = "N/A";

            try
            {
                using (var http = await storage.Options.CreateHttpClientAsync().ConfigureAwait(false))
                {
                    var result = await http.GetAsync(url);
                    resultContent = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var data = JsonConvert.DeserializeObject<T>(resultContent);

                    result.EnsureSuccessStatusCode();

                    return data;
                }
            }
            catch (Exception ex)
            {
                throw new FirebaseStorageException(url, resultContent, ex);
            }
        }

        private string GetTargetUrl()
        {
            return $"{FirebaseStorageEndpoint}{storage.StorageBucket}/o?name={GetEscapedPath()}";
        }

        private string GetDownloadUrl()
        {
            return $"{FirebaseStorageEndpoint}{storage.StorageBucket}/o/{GetEscapedPath()}";
        }

        private string GetFullDownloadUrl()
        {
            return GetDownloadUrl() + "?alt=media&token=";
        }

        private string GetEscapedPath()
        {
            return Uri.EscapeDataString(string.Join("/", children));
        }
    }
}
