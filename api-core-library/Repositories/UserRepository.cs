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
    private SqlConnection CreateConnection() => new SqlConnection(_connectionString);

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        try
        {
            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<User>("SELECT * FROM Users");
            }
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving users.", ex);
        }
    }

    public async Task<UserDto> GetUserByUsernameAsync(string username)
    {
        const string sql = @"
            SELECT u.Id, u.username, u.passwordHash, u.Email, u.CompanyId, r.RoleName 
            FROM Users u
            INNER JOIN UserRoles ur ON u.Id = ur.UserId
            INNER JOIN Roles r ON ur.RoleId = r.Id
            WHERE u.username = @username;";

        try
        {
            using (var connection = CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<UserDto>(sql, new { username });
            }
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving the user by username.", ex);
        }
    }

    public async Task<IEnumerable<UserDto>> GetUserAsync(UserDto user)
    {
        const string sql = @"
            SELECT u.Id, u.username, u.passwordHash, u.Email, u.CompanyId, r.RoleName 
            FROM Users u
            INNER JOIN UserRoles ur ON u.Id = ur.UserId
            INNER JOIN Roles r ON ur.RoleId = r.Id
            WHERE (@Username IS NULL OR @Username = '' OR u.username = @Username)
            AND (@RoleName IS NULL OR @RoleName = '' OR r.RoleName = @RoleName)
            AND u.CompanyId = @CompanyId;";

        try
        {
            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<UserDto>(sql, new { user.Username, user.RoleName, user.CompanyId });
            }
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving users with filters.", ex);
        }
    }

    public async Task AddUserAsync(User user)
    {
        const string sql = "INSERT INTO Users (Username, PasswordHash, Email, CompanyId, CreatedAt, UpdatedAt) VALUES (@Username, @PasswordHash, @Email, @CompanyId, @CreatedAt, @UpdatedAt)";

        try
        {
            using (var connection = CreateConnection())
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
                await connection.ExecuteAsync(sql, user);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while adding a new user.", ex);
        }
    }

    public async Task UpdateUserAsync(User user)
    {
        const string sql = "UPDATE Users SET Username = @Username, PasswordHash = @PasswordHash, Email = @Email, CompanyId = @CompanyId, UpdatedAt = @UpdatedAt WHERE Id = @Id";
        user.UpdatedAt = DateTime.UtcNow;

        try
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync(sql, user);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while updating the user.", ex);
        }
    }

    public async Task DeleteUserAsync(int id)
    {
        const string sql = "DELETE FROM Users WHERE Id = @Id";

        try
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while deleting the user.", ex);
        }
    }


}
