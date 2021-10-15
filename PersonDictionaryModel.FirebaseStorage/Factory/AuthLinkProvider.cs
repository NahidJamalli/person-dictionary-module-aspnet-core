using Firebase.Auth;
using System.Threading.Tasks;
using static PersonDictionaryModel.FirebaseStorage.Constant.CredentialConstants;

namespace PersonDictionaryModel.FirebaseStorage.Factory
{
    public static class AuthLinkProvider
    {
        static FirebaseAuthProvider _auth = null;
        static FirebaseAuthLink _authLink = null;

        public static async Task<FirebaseAuthLink> GetFirebaseAuthLink()
        {
            if (_authLink is null) _authLink = await _auth.SignInWithEmailAndPasswordAsync(AUTH_EMAIL, AUTH_PASSWORD);
            return _authLink;
        }

        public static FirebaseAuthProvider GetFirebaseAuthProvider()
        {
            if (_auth is null) _auth = new FirebaseAuthProvider(new FirebaseConfig(API_KEY));
            return _auth;
        }
    }
}
