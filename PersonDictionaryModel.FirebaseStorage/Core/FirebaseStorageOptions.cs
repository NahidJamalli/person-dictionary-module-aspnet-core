using System;
using System.Threading.Tasks;

namespace PersonDictionaryModel.FirebaseStorage.Core
{
    public class FirebaseStorageOptions
    {
        public Func<Task<string>> AuthTokenAsyncFactory { get; set; }
        public bool ThrowOnCancel { get; set; }
        public TimeSpan HttpClientTimeout { get; set; }
    }
}
