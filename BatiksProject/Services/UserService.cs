using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BatiksProject.Dto;
using BatiksProject.Infrastructure;
using BatiksProject.Models;
using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;

namespace BatiksProject.Services
{
    public interface IUserService
    {
        Task<UserDto> Get(int userId);
        Task<IEnumerable<UserDto>> GetAll();
        Task<int> CountAll();
        Task Add(User user);
        Task Remove(int userId);
        Task Update(User user);
        Task<UserDto> Verify(string username, string password);
    }

    public class UserService : IUserService
    {
        private readonly SHA256 _hasher;
        private readonly IMapper _mapper;
        private readonly BatikContext _batikContext;

        public UserService(BatikContext batikContext, IMapper mapper)
        {
            _batikContext = batikContext;
            _mapper = mapper;
            _hasher = new SHA256Managed();
        }

        public async Task<UserDto> Get(int userId)
        {
            var entity = await _batikContext.Users.FindAsync(userId);
            return _mapper.Map<UserDto>(entity);
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var list = await _batikContext.Users.ToListAsync();
            return _mapper.Map<List<UserDto>>(list);
        }

        public async Task<int> CountAll()
        {
            return await _batikContext.Users.CountAsync();
        }

        public async Task Add(User user)
        {
            try
            {
                var passwordBytes = Encoding.UTF8.GetBytes(user.Password);
                user.Password = Convert.ToBase64String(_hasher.ComputeHash(passwordBytes));

                await _batikContext.Users.AddAsync(user);
                await _batikContext.SaveChangesAsync();
            }
            catch (UniqueConstraintException)
            {
                throw new ServicesException("Admin dengan username yang sama sudah terdaftar.");
            }
        }

        public async Task Remove(int userId)
        {
            var entry = await _batikContext.Users.FindAsync(userId);
            _batikContext.Users.Remove(entry);
            await _batikContext.SaveChangesAsync();
        }

        public async Task Update(User user)
        {
            try
            {
                _batikContext.Attach(user);

                var entry = _batikContext.Entry(user);
                entry.Property(x => x.Username).IsModified = true;
                entry.Property(x => x.Password).IsModified = false;
                if (!string.IsNullOrWhiteSpace(user.Password))
                {
                    var passwordBytes = Encoding.UTF8.GetBytes(user.Password);
                    user.Password = Convert.ToBase64String(_hasher.ComputeHash(passwordBytes));
                    entry.Property(x => x.Password).IsModified = true;
                }

                await _batikContext.SaveChangesAsync();
            }
            catch (UniqueConstraintException)
            {
                throw new ServicesException("Admin dengan username yang sama sudah terdaftar.");
            }
        }

        public async Task<UserDto> Verify(string username, string password)
        {
            var result = await _batikContext.Users.SingleOrDefaultAsync(x => x.Username == username);
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

            return _mapper.Map<UserDto>(result);
        }
    }
}
