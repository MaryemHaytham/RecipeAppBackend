﻿using RecipeAppDAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppDAL.Repositories.IRepositories
{
    public interface IGenericRepository<T>
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        public IQueryable<T> GetQueryable();
        public void AddRange(IEnumerable<T> entities);
        IQueryable<T> Get(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes);

    }
}
