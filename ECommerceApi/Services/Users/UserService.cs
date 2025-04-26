using ECommerceApi.Auth;
using ECommerceApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace ECommerceApi.Services.Users
{
    public class UserService(AppDbContext context, PasswordHasher passwordHasher, JwtTokenProvider tokenProvider) : IUserService
    {
        private readonly AppDbContext _context = context;
        private readonly PasswordHasher _passwordHasher = passwordHasher;
        private readonly JwtTokenProvider _tokenProvider = tokenProvider;

        public async Task<string> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && _passwordHasher.VerifyPassword(password, u.PasswordHash));
            if (user is null) throw new Exception("Unauthorized - No user with the given credentials");

            string token = _tokenProvider.CreateToken(user);
            return token;
        }
    }
}
