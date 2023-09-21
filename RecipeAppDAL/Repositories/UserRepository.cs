using Microsoft.EntityFrameworkCore;
using RecipeAppDAL.DataContext;
using RecipeAppDAL.Entity;
using RecipeAppDAL.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

public class UserRepository : IUserRepository
{
    private readonly RecipeDbContext _recipeDbContext;

    public UserRepository(RecipeDbContext recipeDbContext)
    {
        _recipeDbContext = recipeDbContext;
    }

    public User GetUserById(int id)
    {
        return _recipeDbContext.Users.Find(id);
    }
    public User GetUserByEmail(string email)
    {
        return _recipeDbContext.Users.FirstOrDefault(u => u.Email == email);
    }

    public IEnumerable<User> GetAll()
    {
        return _recipeDbContext.Users.ToList();
    }

    public void Add(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        _recipeDbContext.Users.Add(user);
        _recipeDbContext.SaveChanges();
    }

    public void UpdateUser(User user)
    {
        _recipeDbContext.Users.Update(user);
        _recipeDbContext.SaveChanges();
    }

    public void Remove(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        _recipeDbContext.Users.Remove(user);
        _recipeDbContext.SaveChanges();
    }
}
