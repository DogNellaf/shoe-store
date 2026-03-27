using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ShoeStore.Helpers
{
    public class AuthOptions
    {
        public const string ISSUER = "ShoeStoreBackend";
        public const string AUDIENCE = "ShoeStoreFrontend";
        const string KEY = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
