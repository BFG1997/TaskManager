namespace TaskManager.App.Services.Interfaces
{
    public interface IErrorService
    {
        Task<(bool isSuccess, string? message)> IsRequestSuccessful(HttpResponseMessage? response);
    }
}
