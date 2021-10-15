using System.IO;
using System.Threading.Tasks;
using PersonDictionaryModel.FirebaseStorage.Core;
using static PersonDictionaryModel.FirebaseStorage.Constant.CredentialConstants;

namespace PersonDictionaryModel.FirebaseStorage.Service
{
    public static class StorageService
    {
        public static async Task<string> UploadPhoto(Stream stream, string fullFileName)
        {
            try
            {
                var auth = Factory.AuthLinkProvider.GetFirebaseAuthProvider();
                var authLink = await Factory.AuthLinkProvider.GetFirebaseAuthLink();

                var task = new Core.FirebaseStorage(
                    BUCKET,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(authLink.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child("assets")
                    .Child(fullFileName)
                    .PutAsync(stream);

                return await task;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
