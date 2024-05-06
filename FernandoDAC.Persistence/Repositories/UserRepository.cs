using System.Data;
using Dapper;
using FernandoDAC.Domain.Entities;
using FernandoDAC.Domain.Repositories;

namespace FernandoDAC.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FernandoDACDapperDbContext _context;

        public UserRepository(FernandoDACDapperDbContext context)
        {
            _context = context;
        }

        public async Task CreateUser(User user)
        {
            string query = "Insert into Users(UserName,Password,Role) values (@username,@password,@role)";
            var parameters = new DynamicParameters();
            parameters.Add("username", user.UserName, DbType.String);
            parameters.Add("password", user.Password, DbType.String);
            parameters.Add("role", user.Role, DbType.String);
            using (var connectin = _context.CreateConnection())
            {
                await connectin.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            var query = "SELECT * FROM Users";
            using (var connection = _context.CreateConnection())
            {
                var users = await connection.QueryAsync<User>(query);
                return users.ToList();
            }
        }

        public async Task<User> GetUserByUserNameAndPasswordAsync(string userName, string password)
        {
            string query = "Select * From Users where username=@username and password=@password";
            using (var connectin = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("username", userName, DbType.String);
                parameters.Add("password", password, DbType.String);
                var users = await connectin.QueryFirstOrDefaultAsync<User>(query, parameters);
                return users!;
            }
        }
    }
}