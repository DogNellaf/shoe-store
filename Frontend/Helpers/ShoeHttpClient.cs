using Library.Dto.Employee;
using ShoeStore.Dto.Employee;
using ShoeStore.Dto.Sale;
using System.Collections;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ShoeStore.Helpers
{
    public static class ShoeHttpClient
    {
        internal static string? Token = null;
        internal static string? Role = null;
        internal static string? UserLogin = null;
        internal static string BackendHostUrl = "https://localhost:7217";

        //private static Dictionary<string, string> getHeaders()
        //{
        //    return new Dictionary<string, string>()
        //    {
        //        {"Authorization", $"Bearer { Token }" }
        //    };
        //}

        internal static async Task<(Response, HttpStatusCode)> Get(string url)
        {
            using var client = new HttpClient();
            var request = await client.GetAsync(url);

            return null;

            //if (!request.IsSuccessStatusCode)
            //{
            //    return request.StatusCode;
            //}

            //var response = await request.Content.ReadFromJsonAsync<Response?>();

            //if (response == null)
            //{
            //    return HttpStatusCode.InternalServerError;
            //}

            //if (response.Values == null)
            //{
            //    return HttpStatusCode.InternalServerError;
            //}

            //return 
        }

        internal static async Task<HttpStatusCode> Login(string login, string password)
        {
            try
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

                if (response.Values == null)
                {
                    return HttpStatusCode.InternalServerError;
                }

                Token = response.Values["token"].ToString();
                Role = response.Values["role"].ToString();
                UserLogin = login;
                return HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        internal static async Task<HttpStatusCode> GetEmployee(string login)
        {

        }

        internal static async Task<List<EmployeeInfoDto>> GetEmployees()
        {
            var url = $"{BackendHostUrl}/api/employees/";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            var request = await client.GetAsync(url);
            var response = await request.Content.ReadFromJsonAsync<Response?>();


            string itemsRaw = response.Values["result"].ToString();
            return JsonSerializer.Deserialize<List<EmployeeInfoDto>>(itemsRaw);
        }

        internal static async Task<HttpStatusCode> CreateEmployee(EmployeeInfoDto dto)
        {
            var url = $"{BackendHostUrl}/api/employees/create";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            var createDto = new EmployeeCreateDto()
            {
                Login = dto.Login,
                Password = dto.Password,
                Role = dto.Role
            };
            var json = JsonSerializer.Serialize(createDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = await client.PostAsync(url, content);
            var response = await request.Content.ReadFromJsonAsync<Response?>();

            if (request == null)
            {
                return HttpStatusCode.InternalServerError;
            }

            return (HttpStatusCode)response.StatusCode;
        }

        internal static async Task<HttpStatusCode> UpdateEmployee(EmployeeInfoDto dto)
        {
            var url = $"{BackendHostUrl}/api/employees/update";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = await client.PutAsync(url, content);
            var response = await request.Content.ReadFromJsonAsync<Response?>();

            if (request == null)
            {
                return HttpStatusCode.InternalServerError;
            }

            return (HttpStatusCode)response.StatusCode;
        }

        internal static async Task<List<string>> GetRoles()
        {
            var url = $"{BackendHostUrl}/api/roles/"; //TODO: проверить

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            //var request = new HttpRequestMessage()
            //{
            //    RequestUri = new Uri(url),
            //    Headers = headers,
            //    Method = HttpMethod.Get,
            //};
            //client. = $"Bearer {Token }";
            var request = await client.GetAsync(url);
            var response = await request.Content.ReadFromJsonAsync<Response?>();

            // TODO: добавить проверку на отсутствие ответа от сервера
            //if (response == null)
            //{
            //    return HttpStatusCode.InternalServerError;
            //}

            //if (response.Values == null)
            //{
            //    return HttpStatusCode.InternalServerError;
            //}

            string itemsRaw = response.Values["result"].ToString();
            return JsonSerializer.Deserialize<List<string>>(itemsRaw);
        }

        internal static async Task<HttpStatusCode> CreateSale(SaleInfoDto dto)
        {
            var url = $"{BackendHostUrl}/api/sales/create";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            // TODO: найти сотрудника по логину
            var employeeId = dto.EmployeeLogin;
            

            // TODO: найти товар по названию
            var itemId = dto.ItemTitle;

            // TODO: подумать, как переписать серверную часть
            var createDto = new SaleCreateDto() 
            {
                ItemId = -1,
                EmployeeId = -1,
                Count = 1,
                IsReturned = false
            };
            var json = JsonSerializer.Serialize(createDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = await client.PostAsync(url, content);
            var response = await request.Content.ReadFromJsonAsync<Response?>();

            if (request == null)
            {
                return HttpStatusCode.InternalServerError;
            }

            return (HttpStatusCode)response.StatusCode;
        }

        internal static async Task<HttpStatusCode> UpdateSale(SaleInfoDto dto)
        {
            throw new NotImplementedException();
        }

        internal static async Task<IEnumerable> GetSales()
        {
            throw new NotImplementedException();
        }
    }
}
