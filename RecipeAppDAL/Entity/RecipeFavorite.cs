using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppDAL.Entity
{
    public class RecipeFavorite
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
