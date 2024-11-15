using System.Net;
using System.Net.Http;
using System.Net.Http.Json;

namespace ShoeStore.Helpers
{
    internal class ShoeHttpClient
    {
        internal string? Token = null;
        internal string BackendHostUrl = "http://localhost:5000";
        internal async Task<HttpStatusCode> Login(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                return HttpStatusCode.BadRequest;
            }

            var url = $"{BackendHostUrl}/api/auth/login/{login}?password={password}";

            using var client = new HttpClient();
            var request = await client.GetAsync(url);

            if (!request.IsSuccessStatusCode)
            {
                return request.StatusCode;
            }

            var response = await request.Content.ReadFromJsonAsync<Response?>();

            if (response == null)
            {
                return HttpStatusCode.InternalServerError;
            }

            if (string.IsNullOrWhiteSpace(response.Text))
            {
                return HttpStatusCode.InternalServerError;
            }

            Token = response.Text;
            return HttpStatusCode.OK;
        }
    }
}
