//using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecipeAppDAL.DataContext;
using RecipeAppDAL.Entity;
using RecipeAppDAL.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppDAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
         where T : class
    {
        private readonly DataContext.RecipeDbContext _recipeDbContext;
       
        public GenericRepository(DataContext.RecipeDbContext recipeDbContext)
        {
            _recipeDbContext = recipeDbContext;
        } 

        public IEnumerable<T> GetAll()
        {
            return _recipeDbContext.Set<T>().ToList();
        }

        public void Add(T entity)
        {
            _recipeDbContext.Add(entity);
            _recipeDbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _recipeDbContext.Update(entity);
            _recipeDbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _recipeDbContext.Remove(entity);
            _recipeDbContext.SaveChanges();
        }

        public T GetById(int id)
        {
            return _recipeDbContext.Set<T>().Find(id);
        }

        public IQueryable<T> GetQueryable()
        {
            return _recipeDbContext.Set<T>();
        }
    }
}
