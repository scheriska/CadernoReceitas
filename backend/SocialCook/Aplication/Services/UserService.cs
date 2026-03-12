using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialCook.Aplication.DTOs;
using SocialCook.Domain.Entities;
using SocialCook.Infrastructure.Data;

namespace SocialCook.Aplication.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> RegisterUserAsync(RegisterUserRequest request)
        {
            var user = new User(request.Name, request.Email, BCrypt.Net.BCrypt.HashPassword(request.Password), request.ProfileType);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}