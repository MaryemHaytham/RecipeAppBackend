using RecipeAppDAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppBLL.Utilities.Validators.IValidators
{
    public interface IRecipeValidator
    {
        public Recipe validateRecipe(int RecipeId);
    }
}
