using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeAppBLL.Services.IService;
using RecipeAppDAL.DataContext;
using RecipeAppDAL.Entity;
using RecipeAppDTO.RecipeDTO;
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
        private readonly IMapper _mapper;

        public ReviewService(RecipeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Review> CreateReviewAsync(ReviewDto reviewDto)
        {
            var review = _mapper.Map<ReviewDto, Review>(reviewDto);
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
            return await _context.Reviews.Where(r => r.RecipeId == recipeId).ToListAsync();
        }

        public async Task UpdateReviewAsync(int reviewId, ReviewDto reviewDto)
        {
            var existingReview = await _context.Reviews.FindAsync(reviewId);
            if (existingReview != null)
            {
                // Update review properties from reviewDto
                existingReview.Text = reviewDto.Text;

                await _context.SaveChangesAsync();
            }
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
