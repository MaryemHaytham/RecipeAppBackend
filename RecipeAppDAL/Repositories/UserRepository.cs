using Microsoft.EntityFrameworkCore;
using RecipeAppDAL.DataContext;
using RecipeAppDAL.Entity;
using RecipeAppDAL.Repositories;
using RecipeAppDAL.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly RecipeDbContext _recipeDbContext;

    public UserRepository(RecipeDbContext recipeDbContext) : base(recipeDbContext)
    {
        _recipeDbContext = recipeDbContext;
    }

    public User GetUserByEmail(string email)
    {
        return _recipeDbContext.Users.FirstOrDefault(u => u.Email == email);
    }

   
}
