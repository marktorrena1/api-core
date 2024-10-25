using System;
using api_core_library.Intefaces;
using api_core_library.Models;
using Microsoft.AspNetCore.Mvc;


namespace api_core_library.Repositories;

public class UserRepository
{
    // temporary storage - in memory
    private static List<User> users = new List<User>();
    private static int nextId = 1;

    public IEnumerable<User> GetAll()
    {
        return users;
    }

    public User? GetById(int userId)
    {
        return users.FirstOrDefault(u => u.UserId == userId);
    }

    public void Add(User user)
    {
        user.UserId = nextId++;
        users.Add(user);
    }

    public void Update(User user)
    {
        var existingUser = GetById(user.UserId);
        if (existingUser != null)
        {
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
        }
    }

    public void Delete(int userId)
    {
        var user = GetById(userId);
        if (user != null)
        {
            users.Remove(user);
        }
    }
}
