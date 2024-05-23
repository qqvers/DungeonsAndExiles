using AutoMapper;
using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.DTOs.User;
using DungeonsAndExiles.Api.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DungeonsAndExiles.Api.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public UserRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<User> RegisterUserAsync(UserRegisterDto userRegisterDto)
        {
            if (userRegisterDto == null) throw new ArgumentNullException(nameof(userRegisterDto));

            var existingCustomer = await _appDbContext.Users
                .AnyAsync(o => o.Email == userRegisterDto.Email);
            if (existingCustomer)
            {
                throw new InvalidOperationException("Email already in use by another customer");
            }

            var newUser = _mapper.Map<User>(userRegisterDto);
            var defaultRole = await _appDbContext.Roles.FirstOrDefaultAsync(n => n.Name == "User");

            if (defaultRole == null)
            {
                throw new InvalidOperationException("Role User not found");
            }
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            newUser.RoleId = defaultRole.Id;

            _appDbContext.Users.Add(newUser);
            await _appDbContext.SaveChangesAsync();

            return newUser;
        }

        public async Task<User> FindUserInDatabase(UserLoginDto userLoginDto)
        {
            if (userLoginDto == null) throw new ArgumentNullException(nameof(userLoginDto));

            var loggedUser = await _appDbContext.Users
                        .SingleOrDefaultAsync(o => o.Email == userLoginDto.Email);


            return loggedUser;
        }

        public async Task<User> FindUserByIdAsync(Guid userId)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

            return user;
        }

        public async Task<bool> UpdateUserAsync(Guid userId, UserUpdateDto userUpdateDto)
        {
            var currentUser = await _appDbContext.Users.FindAsync(userId);
            if (currentUser == null) { throw new ArgumentNullException(nameof(currentUser)); }

            var emailInDatabase = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Email == userUpdateDto.Email);

            if(emailInDatabase != null) 
            {
                throw new InvalidOperationException("Selected email is taken by other user");
            }

            userUpdateDto.Password = BCrypt.Net.BCrypt.HashPassword(userUpdateDto.Password);
            _mapper.Map(userUpdateDto, currentUser); 


            await _appDbContext.SaveChangesAsync();

            return true;
        }


        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var user = _appDbContext.Users.FirstOrDefault(x => x.Id == userId);

            if (user == null) return false;

            _appDbContext.Users.Remove(user);
            await _appDbContext.SaveChangesAsync();

            return true;
        }

    }
}
