using ECommerceApi.Models;

namespace ECommerceApi.Services.Users
{
    public interface IUserService
    {
        Task<string> Login(string username, string password);
    }
}
