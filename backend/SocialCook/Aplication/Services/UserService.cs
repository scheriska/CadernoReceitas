using Microsoft.EntityFrameworkCore;
using SocialCook.Aplication.DTOs.Users;
using SocialCook.Domain.Entities;
using SocialCook.Infrastructure.Data;

namespace SocialCook.Aplication.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;

        public UserService(AppDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<User> RegisterUserAsync(RegisterUserRequest request)
        {
            var user = new User(request.Name, request.Email, BCrypt.Net.BCrypt.HashPassword(request.Password), request.ProfileType);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        ///// <summary>
        ///// Authenticates a user and returns a JWT token if successful.
        ///// </summary>
        public async Task<LoginUserResponse?> Login(LoginUserRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user ==null)
                return null;

            var validPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!validPassword)
                return null;

            var token = _tokenService.GenerateToken(user);

            return new LoginUserResponse
            { 
                Token = token 
            };
        }
    }
}