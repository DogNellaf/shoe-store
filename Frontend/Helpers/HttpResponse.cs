using System.Net;
using System.Net.Http;
using System.Net.Http.Json;

namespace ShoeStore.Helpers
{
    public class HttpResponse
    {
        public HttpStatusCode Status;
        public Dictionary<string, object> Values;

        public HttpResponse(HttpResponseMessage response)
        {
            Status = response.StatusCode;

            var body = response.Content.ReadFromJsonAsync<Response?>().Result;
            if (body != null)
            {
                if (body.Values != null)
                {
                    Values = body.Values;
                }
                else
                {
                    throw new HttpRequestException("Тело не содержит значения");
                }
            }
            else
            {
                throw new HttpRequestException("В ответе отсутствует тело запроса");
            }
        }
    }
}
