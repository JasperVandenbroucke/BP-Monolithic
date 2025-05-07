using ECommerceApi.Auth;
using ECommerceApi.Data;
using ECommerceApi.Models;
using ECommerceApi.Models.Dtos.Users;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Services.Users
{
    public class UserService(AppDbContext context, PasswordHasher passwordHasher, JwtTokenProvider tokenProvider) : IUserService
    {
        private readonly AppDbContext _context = context;
        private readonly PasswordHasher _passwordHasher = passwordHasher;
        private readonly JwtTokenProvider _tokenProvider = tokenProvider;

        public async Task<string> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username) ??
                throw new Exception("Unauthorized - No user with the given username");

            bool passwordIsValid = _passwordHasher.VerifyPassword(password, user.PasswordHash);
            if (!passwordIsValid)
                throw new Exception("Unauthorized - Invalid password");

            return _tokenProvider.CreateToken(user);
        }

        public async Task Registreren(LoginDto loginDto)
        {
            if (await _context.Users.AnyAsync(u => u.Username == loginDto.Username))
                throw new Exception("Username already exists.");

            var hashedPwd = _passwordHasher.HashPassword(loginDto.Password);
            _context.Users.Add(new User() { Username = loginDto.Username, PasswordHash = hashedPwd });
            await _context.SaveChangesAsync();
        }
    }
}
