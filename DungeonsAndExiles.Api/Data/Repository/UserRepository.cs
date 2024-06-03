using AutoMapper;
using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.DTOs.User;
using DungeonsAndExiles.Api.Exceptions;
using DungeonsAndExiles.Api.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DungeonsAndExiles.Api.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(AppDbContext appDbContext, IMapper mapper, ILogger<UserRepository> logger)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<User> RegisterUserAsync(UserRegisterDto userRegisterDto)
        {
            if (userRegisterDto == null)
                throw new ArgumentNullException(nameof(userRegisterDto));

            _logger.LogInformation("Attempting to register a new user with email: {Email}", userRegisterDto.Email);

            var existingCustomer = await _appDbContext.Users
                .AnyAsync(o => o.Email == userRegisterDto.Email);
            if (existingCustomer)
            {
                throw new InvalidOperationException("Email already in use by another customer");
            }

            var newUser = _mapper.Map<User>(userRegisterDto);
            var defaultRole = await _appDbContext.Roles.FirstOrDefaultAsync(n => n.Name == "User") ?? throw new NotFoundException("Role User not found");
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            newUser.RoleId = defaultRole.Id;

            _appDbContext.Users.Add(newUser);
            await _appDbContext.SaveChangesAsync();

            _logger.LogInformation("User with email {Email} registered successfully", userRegisterDto.Email);
            return newUser;
        }

        public async Task<User?> FindUserInDatabase(UserLoginDto userLoginDto)
        {
            if (userLoginDto == null)
                throw new ArgumentNullException(nameof(userLoginDto));

            _logger.LogInformation("Attempting to find user with email: {Email}", userLoginDto.Email);

            var loggedUser = await _appDbContext.Users
                .SingleOrDefaultAsync(o => o.Email == userLoginDto.Email);

            if (loggedUser == null)
            {
                var message = $"User with email {userLoginDto.Email} not found";
                throw new NotFoundException(message);
            }

            return loggedUser;
        }

        public async Task<User?> FindUserByIdAsync(Guid userId)
        {
            _logger.LogInformation("Attempting to find user with ID: {UserId}", userId);

            var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                var message = $"User with ID {userId} not found";
                throw new NotFoundException(message);
            }

            return user;
        }

        public async Task UpdateUserAsync(Guid userId, UserUpdateDto userUpdateDto)
        {
            _logger.LogInformation("Attempting to update user with ID: {UserId}", userId);

            var currentUser = await _appDbContext.Users.FindAsync(userId) ?? throw new NotFoundException($"User with ID {userId} not found");
            var emailInDatabase = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Email == userUpdateDto.Email);

            if (emailInDatabase != null && currentUser.Email != userUpdateDto.Email)
            {
                throw new InvalidOperationException("Selected email is taken by other user");
            }

            userUpdateDto.Password = BCrypt.Net.BCrypt.HashPassword(userUpdateDto.Password);
            _mapper.Map(userUpdateDto, currentUser);

            await _appDbContext.SaveChangesAsync();

            _logger.LogInformation("User with ID {UserId} updated successfully", userId);

        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            _logger.LogInformation("Attempting to delete user with ID: {UserId}", userId);

            var user = _appDbContext.Users.FirstOrDefault(x => x.Id == userId);

            if (user == null)
            {
                _logger.LogWarning("User with ID {UserId} not found", userId);
                return false;
            }

            _appDbContext.Users.Remove(user);
            await _appDbContext.SaveChangesAsync();

            _logger.LogInformation("User with ID {UserId} deleted successfully", userId);

            return true;
        }

        public async Task<Role> GetUserRole(Guid roleId)
        {
            _logger.LogInformation("Attempting to find role with ID: {RoleId}", roleId);

            var role = await _appDbContext.Roles.FindAsync(roleId) ?? throw new NotFoundException($"Role with ID {roleId} does not exist");
            _logger.LogInformation("Role with ID {RoleId} found successfully", roleId);

            return role;
        }

        public async Task UpdateUserToken(User user)
        {
            _logger.LogInformation("Attempting to update token for user with ID: {user.Id}", user.Id);
            _appDbContext.Users.Update(user);
            await _appDbContext.SaveChangesAsync();
            _logger.LogInformation("Token updated successfully for user with {user.Id}", user.Id);
        }
    }
}
