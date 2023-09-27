using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeAppBLL.Services.IService;
using RecipeAppDAL.DataContext;
using RecipeAppDAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppBLL.Services
{
    public class RatingService : IRatingService
    {
        private readonly RecipeDbContext _context;
        private readonly ILogger<RecipeService> _logger;
        private readonly IMapper _mapper;

        public RatingService(RecipeDbContext context, ILogger<RecipeService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Rating> CreateRatingAsync(Rating rating)
        {
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
            return rating;
        }

        public async Task<Rating> GetRatingByIdAsync(int Id)
        {
            return await _context.Ratings.FindAsync(Id);
        }

        public async Task<List<Rating>> GetRatingsForRecipeAsync(int recipeId)
        {
            return await _context.Ratings.Where(r => r.Id == recipeId).ToListAsync();
        }

        public async Task UpdateRatingAsync(Rating rating)
        {
            _context.Entry(rating).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRatingAsync(int ratingId)
        {
            var rating = await _context.Ratings.FindAsync(ratingId);
            if (rating != null)
            {
                _context.Ratings.Remove(rating);
                await _context.SaveChangesAsync();
            }
        }
    }
}
