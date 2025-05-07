using ECommerceApi.Models;
using ECommerceApi.Models.Dtos.Users;

namespace ECommerceApi.Services.Users
{
    public interface IUserService
    {
        Task<string> Login(string username, string password);
        Task Registreren(LoginDto loginDto);
    }
}
