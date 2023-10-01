using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppDTO.RecipeDTO
{
    public class RatingDto
    {
        public int Value { get; set; }
        public int UserId { get; set; }
        public int RecipeId { get; set; }
    }
}
