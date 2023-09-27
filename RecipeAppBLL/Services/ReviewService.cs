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
    public class ReviewService : IReviewService
    {
        private readonly RecipeDbContext _context;
        private readonly ILogger<RecipeService> _logger;
        private readonly IMapper _mapper;

        public ReviewService(RecipeDbContext context, ILogger<RecipeService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Review> CreateReviewAsync(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<Review> GetReviewByIdAsync(int reviewId)
        {
            return await _context.Reviews.FindAsync(reviewId);
        }

        public async Task<List<Review>> GetReviewsForRecipeAsync(int recipeId)
        {
            return await _context.Reviews.Where(r => r.Id == recipeId).ToListAsync();
        }

        public async Task UpdateReviewAsync(Review review)
        {
            _context.Entry(review).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReviewAsync(int reviewId)
        {
            var review = await _context.Reviews.FindAsync(reviewId);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }
    }

}
