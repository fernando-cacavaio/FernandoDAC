using FernandoDAC.Domain.Entities;

namespace FernandoDAC.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();

        Task CreateUser(User user);

        Task<User> GetUserByUserNameAndPasswordAsync(string email, string passwordHash);
    }
}