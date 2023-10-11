using RecipeAppDAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppDTO.RecipeDTO
{
    public class RecipeWithReviewsDTO
    {
        public int Id { get; set; }
        public string RecipeName { get; set; }
        public string Image { get; set; }
        public string Ingredients { get; set; }
        public string Steps { get; set; }
        public string? DietaryRestrictions { get; set; }
        public int UserId { get; set; }
        public int Popularity { get; set; }
        public float Rating { get; set; }
        //public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
