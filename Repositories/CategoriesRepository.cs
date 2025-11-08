using Dapper;
using EcommerceApp.Models;
using EcommerceApp.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace EcommerceApp.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly string _connectionString;

        public CategoriesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }

        public async Task<IEnumerable<Categories>> GetAllAsync()
        {
            var sql = "SELECT * FROM categories ORDER BY id;";

            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                var categories = await connection.QueryAsync<Categories>(sql);
                return categories;
            }
        }

        public async Task<Categories?> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM categories WHERE id = @Id;";
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync<Categories>(sql, new { Id = id });
            }
        }

        public async Task<Categories> AddAsync(Categories category)
        {
            var sql = @"INSERT INTO categories (name, description, parent_id)
                        VALUES (@Name, @Description, @ParentId);
                        SELECT CAST(SCOPE_IDENTITY() as int);";

            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                var newId = await connection.ExecuteScalarAsync<int>(sql, category);
                category.Id = newId;
                return category;
            }
        }

        public async Task<Categories?> UpdateAsync(Categories category)
        {
            var sql = @"UPDATE categories
                        SET name = @Name, description = @Description, parent_id = @ParentId
                        WHERE id = @Id;";

            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteAsync(sql, category);
                return result > 0 ? category : null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sql = "DELETE FROM categories WHERE id = @Id;";
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result > 0;
            }
        }
    }
}
