using ShoeStore.Helpers.ServerContexts;
using System.Net;

namespace Frontend.Helpers.ServerContexts
{
    internal static class AuthContext
    {
        private static readonly string _backendHostUrl;

        static AuthContext()
        {
            _backendHostUrl = ServerContext.BackendHostUrl;
        }

        internal static async Task<HttpStatusCode> Login(string login, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
                {
                    return HttpStatusCode.BadRequest;
                }

                var url = $"{_backendHostUrl}/api/auth/login/{login}?password={password}";
                var response = await ServerContext.Get(url);

                ServerContext.Token = response.Values["token"].ToString();
                ServerContext.Role = response.Values["role"].ToString();
                ServerContext.Login = login;
                return HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}
