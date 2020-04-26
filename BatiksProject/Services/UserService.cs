using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BatiksProject.Dto;
using BatiksProject.Infrastructure;
using BatiksProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BatiksProject.Services
{
    public interface IUserService
    {
        Task Register(User user);
        Task Unregister(User user);
        Task Update(User user);
        Task<UserVerifyDto> Verify(string username, string password);
    }

    public class UserService : IUserService
    {
        private readonly SHA256 _hasher;
        private readonly BatikContext _batikContext;

        public UserService(BatikContext batikContext)
        {
            _batikContext = batikContext;
            _hasher = new SHA256Managed();
        }

        public async Task Register(User user)
        {
            var result = await _batikContext.Users.Where(x => x.Username == user.Username).ToListAsync();
            if (result != null)
            {
                throw new ServicesException("Username sudah digunakan.");
            }

            var passwordBytes = Encoding.UTF8.GetBytes(user.Password);
            user.Password = Convert.ToBase64String(_hasher.ComputeHash(passwordBytes));

            await _batikContext.Users.AddAsync(user);
            await _batikContext.SaveChangesAsync();
        }

        public async Task Unregister(User user)
        {
            throw new NotImplementedException();
        }

        public async Task Update(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<UserVerifyDto> Verify(string username, string password)
        {
            var result = await _batikContext.Users.Where(x => x.Username == username).SingleOrDefaultAsync();
            if (result == null)
            {
                throw new ServicesException("Username atau password salah.");
            }

            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var hashedPassword = Convert.ToBase64String(_hasher.ComputeHash(passwordBytes));
            if (result.Password != hashedPassword)
            {
                throw new ServicesException("Username atau password salah.");
            }

            return new UserVerifyDto(username);
        }
    }
}
