using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BatiksProject.Dto;
using BatiksProject.Infrastructure;
using BatiksProject.Models;
using BatiksProject.ViewModels;
using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;

namespace BatiksProject.Services
{
    public interface IUserService
    {
        Task<UserDto> Get(int userId);
        Task<IEnumerable<UserDto>> GetAll();
        Task<int> CountAll();

        Task Add(UserEditViewModel model);
        Task Update(UserEditViewModel model);
        Task Remove(int userId);

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

        public async Task Add(UserEditViewModel model)
        {
            try
            {
                var user = _mapper.Map<User>(model);
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

        public async Task Update(UserEditViewModel model)
        {
            try
            {
                var user = _mapper.Map<User>(model);
                _batikContext.Attach(user);
                var entry = _batikContext.Entry(user);

                if (!string.IsNullOrWhiteSpace(user.Password))
                {
                    var passwordBytes = Encoding.UTF8.GetBytes(user.Password);
                    user.Password = Convert.ToBase64String(_hasher.ComputeHash(passwordBytes));
                    entry.Property(x => x.Password).IsModified = true;
                }
                else
                {
                    entry.Property(x => x.Password).IsModified = false;
                }

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
