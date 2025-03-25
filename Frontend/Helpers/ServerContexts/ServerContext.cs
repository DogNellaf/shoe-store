using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace ShoeStore.Helpers.ServerContexts
{
    /// <summary>
    /// Базовый класс для взаимодействия с сервером посредством HTTP запросов
    /// </summary>
    public static class ServerContext
    {
        // Токен текущего пользователя
        internal static string? Token = null;

        // Роль текущего пользователя
        internal static string? Role = null;

        // Логин текущего пользователя
        internal static string? Login = null;

        // Адрес сервера для отправки запросов
        internal static readonly string BackendHostUrl = "https://localhost:7217";

        /// <summary>
        /// Базовый метод для отправки любого из поддерживаемых типов запросов
        /// </summary>
        /// <param name="type">Тип запроса</param>
        /// <param name="url">Ссылка для отправки</param>
        /// <param name="body">Содержимое тела запроса (опционально)</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">Если метод еще не реализован</exception>
        /// <exception cref="HttpRequestException">Если тело ответа пустое или не содержит Values</exception>
        private static async Task<HttpResponse> SendRequest(RequestType type, string url, string? body = null)
        {
            using var client = new HttpClient();
            if (Token != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            }

            HttpResponseMessage? httpResponse = null;
            StringContent? content = null;
            if (body != null)
            {
                content = new StringContent(body, Encoding.UTF8, "application/json");
            }

            switch (type)
            {
                case RequestType.GET:
                    httpResponse = await client.GetAsync(url);
                    break;
                case RequestType.PATCH:
                    throw new NotImplementedException("Данный метод не реализован");
                case RequestType.DELETE:
                    throw new NotImplementedException("Данный метод не реализован");
                case RequestType.POST:
                    ArgumentNullException.ThrowIfNull(content);
                    httpResponse = await client.PostAsync(url, content);
                    break;
                case RequestType.PUT:
                    ArgumentNullException.ThrowIfNull(content);
                    httpResponse = await client.PutAsync(url, content);
                    break;
                default:
                    throw new NotImplementedException($"Неизвестный тип метода: {type}");
            }

            var response = await httpResponse.Content.ReadFromJsonAsync<Response?>();

            if (response == null)
            {
                string message = $"Ответ не содержит тела запроса (статус {httpResponse.StatusCode})";
                throw new HttpRequestException(message);
            }

            if (response.Values == null)
            {
                string message = $"Тело ответа не содержит поле Values";
                throw new HttpRequestException(message);
            }

            return new HttpResponse(httpResponse);
        }

        /// <summary>
        /// Реализует обертку вокруг стандартного метода GET
        /// </summary>
        /// <param name="url">Адрес для отправки запроса</param>
        /// <returns>Класс с информацией о статусе и данными из тела</returns>
        /// <exception cref="HttpRequestException">Если ответ не имеет тела запроса или не имеет значений внутри</exception>
        internal static async Task<HttpResponse> Get(string url)
        {
            return await SendRequest(RequestType.GET, url);
        }

        /// <summary>
        /// Реализует обертку вокруг стандартного метода POST
        /// </summary>
        /// <param name="url">Адрес для отправки запроса</param>
        /// <param name="body">Тело запроса для отправки</param>
        /// <returns>Класс с информацией о статусе и данными из тела</returns>
        /// <exception cref="HttpRequestException">Если ответ не имеет тела запроса или не имеет значений внутри</exception>
        internal static async Task<HttpResponse> Post(string url, string body)
        {
            return await SendRequest(RequestType.POST, url, body);
        }

        /// <summary>
        /// Реализует обертку вокруг стандартного метода PUT
        /// </summary>
        /// <param name="url">Адрес для отправки запроса</param>
        /// <param name="body">Тело запроса для отправки</param>
        /// <returns>Класс с информацией о статусе и данными из тела</returns>
        /// <exception cref="HttpRequestException">Если ответ не имеет тела запроса или не имеет значений внутри</exception>
        internal static async Task<HttpResponse> Put(string url, string body)
        {
            return await SendRequest(RequestType.PUT, url, body);
        }
    }
}
