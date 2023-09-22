using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppDTO.RecipeDTO
{
    public class RecipeToReturnDTO
    {
        public int Id { get; set; }
        public string RecipeName { get; set; }
        public string Ingredients { get; set; }
        public string Steps { get; set; }
        public string? DietaryRestrictions { get; set; }
        public string Categories { get; set; }
        public int UserId { get; set; }
        public int Popularity { get; set; } 
        public float Rating { get; set; } 
    }
}

