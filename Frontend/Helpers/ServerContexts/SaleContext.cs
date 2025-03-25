using ShoeStore.Dto.Sale;
using ShoeStore.Helpers.ServerContexts;
using System.Collections;
using System.Net;
using System.Text.Json;

namespace ShoeStore.Helpers.ServerContexts
{
    /// <summary>
    /// Реализует методы отправки сотрудников на сервер
    /// </summary>
    internal static class SaleContext
    {
        private static readonly string _backendHostUrl;

        static SaleContext()
        {
            _backendHostUrl = ServerContext.BackendHostUrl;
        }

        internal static async Task<List<string>?> GetRoles()
        {
            var url = $"{_backendHostUrl}/api/roles/";

            var response = await ServerContext.Get(url);

            return JsonSerializer.Deserialize<List<string>>(
                response.Values["result"].ToString() ?? "[]"
            );
        }

        internal static async Task<HttpStatusCode> CreateSale(SaleInfoDto dto)
        {
            var url = $"{_backendHostUrl}/api/sales/create";

            ArgumentNullException.ThrowIfNull(dto.EmployeeLogin);
            ArgumentNullException.ThrowIfNull(dto.ItemTitle);

            var employee = await EmployeeContext.GetEmployee(dto.EmployeeLogin);
            ArgumentNullException.ThrowIfNull(employee);

            var item = await ItemContext.GetItem(dto.ItemTitle);
            ArgumentNullException.ThrowIfNull(item);

            var json = JsonSerializer.Serialize(
                new SaleCreateDto()
                {
                    ItemId = item.Id,
                    EmployeeId = employee.Id,
                    Count = 1,
                    IsReturned = false
                }
            );

            var response = await ServerContext.Post(url, json);
            return response.Status;
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
