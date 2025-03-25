using Library.Dto.Employee;
using ShoeStore.Dto.Employee;
using System.Net;
using System.Text.Json;

namespace ShoeStore.Helpers.ServerContexts
{
    /// <summary>
    /// Реализует методы отправки сотрудников на сервер
    /// </summary>
    internal static class EmployeeContext
    {
        private static string _backendHostUrl;

        static EmployeeContext()
        {
            _backendHostUrl = ServerContext.BackendHostUrl;
        }

        /// <summary>
        /// Возвращает сотрудника по его имени
        /// </summary>
        /// <param name="login">Имя сотрудника для поиска</param>
        /// <returns>Сотрудника или null, если он не был найден</returns>
        internal static async Task<EmployeeInfoDto?> GetEmployee(string login)
        {
            var url = $"{_backendHostUrl}/api/employees/{login}";
            var response = await ServerContext.Get(url);

            return JsonSerializer.Deserialize<EmployeeInfoDto>(
                response.Values["result"].ToString() ?? "[]"
            );
        }

        /// <summary>
        /// Возвращает список всех сотрудников
        /// </summary>
        /// <returns>Список всех сотрудников</returns>
        internal static async Task<List<EmployeeInfoDto>?> GetEmployees()
        {
            var url = $"{_backendHostUrl}/api/employees/";
            var response = await ServerContext.Get(url);

            return JsonSerializer.Deserialize<List<EmployeeInfoDto>>(
                response.Values["result"].ToString() ?? "[]"
            );
        }

        /// <summary>
        /// Создает нового сотрудника
        /// </summary>
        /// <param name="dto">Класс с данными сотрудника</param>
        /// <returns>Статус-код запроса</returns>
        internal static async Task<HttpStatusCode> CreateEmployee(EmployeeInfoDto dto)
        {
            var url = $"{_backendHostUrl}/api/employees/create";

            var json = JsonSerializer.Serialize(
                new EmployeeCreateDto()
                {
                    Login = dto.Login,
                    Password = dto.Password,
                    Role = dto.Role
                }
            );

            var response = await ServerContext.Post(url, json);

            return response.Status;
        }

        /// <summary>
        /// Обновляет существующего сотрудника в соответствии с dto
        /// </summary>
        /// <param name="dto">Класс с данными сотрудника</param>
        /// <returns>Статус-код запроса</returns>
        internal static async Task<HttpStatusCode> UpdateEmployee(EmployeeInfoDto dto)
        {
            var url = $"{_backendHostUrl}/api/employees/update";

            var json = JsonSerializer.Serialize(dto);
            var response = await ServerContext.Put(url, json);

            return response.Status;
        }
    }
}
