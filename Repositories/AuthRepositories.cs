using Dapper;
using Npgsql;
using EcommerceApp.Models;
using EcommerceApp.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace EcommerceApp.Repositories
{
    public class AuthRepositories : IAuthRepositories
    {
        private readonly string _connectionString;

        public AuthRepositories(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }

        public async Task<Users?> RegisterUserAsync(Users user)
        {
            using var connection = GetConnection();
            await connection.OpenAsync();

            const string sql = @"
                INSERT INTO users (full_name, email, password_hash, phone, role, created_at, updated_at)
                VALUES (@FullName, @Email, @PasswordHash, @Phone, @Role::user_role, @CreatedAt, @UpdatedAt)
                RETURNING id;
            ";

            var parameters = new
            {
                FullName = user.Name,
                Email = user.Email,
                PasswordHash = user.Password,
                Phone = user.Phone,
                Role = user.Role.ToString().ToLower(),
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };

            var newUserId = await connection.ExecuteScalarAsync<int>(sql, parameters);
            user.Id = newUserId;

            return user;
        }

        public async Task<Users?> LoginUserAsync(Users user)
        {
            using var connection = GetConnection();
            await connection.OpenAsync();

            const string sql = @"
                SELECT 
                    id, 
                    full_name AS Name, 
                    email, 
                    password_hash AS Password, 
                    phone, 
                    role, 
                    created_at AS CreatedAt, 
                    updated_at AS UpdatedAt
                FROM users
                WHERE email = @Email;
            ";

            var existingUser = await connection.QueryFirstOrDefaultAsync<Users>(sql, new { Email = user.Email });

            return existingUser;
        }
    }
}
