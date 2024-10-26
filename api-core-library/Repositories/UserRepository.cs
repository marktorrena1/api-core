using System;
using api_core_library.Intefaces;
using api_core_library.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace api_core_library.Repositories;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            return await connection.QueryAsync<User>("SELECT * FROM Users");
        }
    }

    public async Task<UserDto> GetUserByUsernameAsync(string username)
    {
         var sql = @"
        SELECT u.Id, u.username, u.passwordHash, u.Email, u.CompanyId, r.RoleName 
        FROM Users u
        INNER JOIN UserRoles ur ON u.Id = ur.UserId
        INNER JOIN Roles r ON ur.RoleId = r.Id
        WHERE u.username = @username;";

        using (var connection = new SqlConnection(_connectionString))
        {
            return await connection.QuerySingleOrDefaultAsync<UserDto>(sql, new { username });
        }
    }

    public async Task<IEnumerable<UserDto>> GetUserAsync(UserDto user)
    {
        var sql = @"
        SELECT u.Id, u.username, u.passwordHash, u.Email, u.CompanyId, r.RoleName 
        FROM Users u
        INNER JOIN UserRoles ur ON u.Id = ur.UserId
        INNER JOIN Roles r ON ur.RoleId = r.Id
        WHERE (@Username IS NULL OR @Username = '' OR u.username = @Username)
        AND (@RoleName IS NULL OR @RoleName = '' OR r.RoleName = @RoleName)
        AND u.CompanyId = @CompanyId;";

        using (var connection = new SqlConnection(_connectionString))
        {
            return await connection.QueryAsync<UserDto>(sql, new { user.Username, user.RoleName, user.CompanyId});
        }
    }

    public async Task AddUserAsync(User user)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            var sql = "INSERT INTO Users (Username, PasswordHash, Email, CompanyId, CreatedAt, UpdatedAt) VALUES (@Username, @PasswordHash, @Email, @CompanyId, @CreatedAt, @UpdatedAt)";
            await connection.ExecuteAsync(sql, user);
        }
    }

    public async Task UpdateUserAsync(User user)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sql = "UPDATE Users SET Username = @Username, PasswordHash = @PasswordHash, Email = @Email, CompanyId = @CompanyId, UpdatedAt = @UpdatedAt WHERE Id = @Id";
            await connection.ExecuteAsync(sql, user);
        }
    }

    public async Task DeleteUserAsync(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.ExecuteAsync("DELETE FROM Users WHERE Id = @Id", new { Id = id });
        }
    }

    
}
