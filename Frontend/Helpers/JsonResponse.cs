using ShoeStore.Helpers;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;

namespace Frontend.Helpers
{
    public class JsonResponse
    {
        public Dictionary<string, object>? Values = null;
        public HttpStatusCode Status;

        public JsonResponse(HttpResponseMessage? message)
        {
            // Если не передан message, считаем запрос неуспешным
            if (message == null)
            {
                Values = null;
                Status = HttpStatusCode.InternalServerError;
                return;
            }

            Status = message.StatusCode;

            // Если статус-код не успешен, значений нет
            if (!message.IsSuccessStatusCode)
            {
                Values = null;
                return;
            }

            var response = message.Content.ReadFromJsonAsync<Response?>().Result;
            if (response == null)
            {
                Values = null;
                return;
            }

            Values = response.Values;
        }
    }
}
