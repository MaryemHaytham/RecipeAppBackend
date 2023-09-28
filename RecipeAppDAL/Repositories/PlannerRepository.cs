using RecipeAppDAL.DataContext;
using RecipeAppDAL.Entity.RecipeAppDAL.Entity;
using RecipeAppDAL.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppDAL.Repositories
{
    public class PlannerRepository : GenericRepository<Plans> ,IPlannerRepository
    {
        private readonly DataContext.RecipeDbContext _recipeDbContext;
        public PlannerRepository(RecipeDbContext recipeDbContext) : base(recipeDbContext)
        {
            _recipeDbContext = recipeDbContext;
        }

        public IEnumerable<Plans> GetPlansForUser(int userId)
        {
            var mealPlans = _recipeDbContext.Plans
        .Where(mp => mp.UserId == userId)
        .ToList();

            return mealPlans;
        }
    }
}
