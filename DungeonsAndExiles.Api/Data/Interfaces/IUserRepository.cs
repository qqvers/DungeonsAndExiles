using DungeonsAndExiles.Api.DTOs.User;
using DungeonsAndExiles.Api.Models.Domain;

namespace DungeonsAndExiles.Api.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<User> RegisterUserAsync(UserRegisterDto userDto);
        Task<User?> FindUserInDatabase(UserLoginDto userDto);
        Task<bool> UpdateUserAsync(Guid userId, UserUpdateDto userUpdateDto);
        Task<User?> FindUserByIdAsync(Guid userId);
        Task<bool> DeleteUserAsync(Guid userId);
        Task<Role> GetUserRole(Guid userId);
        Task<bool> UpdateUserToken(User user);
    }
}
