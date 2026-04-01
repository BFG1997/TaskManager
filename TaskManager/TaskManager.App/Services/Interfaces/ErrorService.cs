

namespace TaskManager.App.Services.Interfaces
{
    public class ErrorService : IErrorService
    {
        public async Task<(bool isSuccess, string? message)> IsRequestSuccessful(HttpResponseMessage? response)
        {
            var message = string.Empty;
            if (response == null)
            {
                message = "Сервер недоступен";
                Console.Error.WriteLine("Сервер недоступен");
                return (false, message);
            }

            if (response.IsSuccessStatusCode)
            {
                return (true, string.Empty);

            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errors = await response.Content.ReadFromJsonAsync<Dictionary<string, string[]>>();

                if (errors != null)
                {
                    message = string.Join("\n", errors.SelectMany(x => x.Value));
                }
                else
                {
                    message = "Validation error";
                }
                return (false, message);
            }
            else
            {
                message = $"Ошибка: {response.StatusCode}";
                return (false, message);
            }
        }
    }
}
