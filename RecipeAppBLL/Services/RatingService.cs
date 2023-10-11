using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeAppBLL.Services.IService;
using RecipeAppDAL.DataContext;
using RecipeAppDAL.Entity;
using RecipeAppDAL.Repositories;
using RecipeAppDAL.Repositories.IRepositories;
using RecipeAppDTO.RecipeDTO;
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
        private readonly IMapper _mapper;

        public RatingService(RecipeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
          
        }

        public async Task<Rating> CreateRatingAsync(RatingDto ratingDto)
        {
            var rating = _mapper.Map<RatingDto, Rating>(ratingDto);
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
            return rating;
        }

        public async Task<Rating> GetRatingByIdAsync(int ratingId)
        {
            return await _context.Ratings.FindAsync(ratingId);
        }

        public async Task<List<Rating>> GetRatingsForRecipeAsync(int recipeId)
        {
            return await _context.Ratings.Where(r => r.RecipeId == recipeId).ToListAsync();
        }

        public async Task UpdateRatingAsync(int ratingId, RatingDto ratingDto)
        {
            var existingRating = await _context.Ratings.FindAsync(ratingId);
            if (existingRating != null)
            {
                // Update rating properties from ratingDto
                existingRating.Value = ratingDto.Value;

                await _context.SaveChangesAsync();
            }
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
