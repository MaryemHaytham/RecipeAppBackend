using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppBLL.CustomExceptions
{
    public class RecipeNotFoundException :Exception
    {
        public RecipeNotFoundException() : base("Recipe not found.")
        {
        }

    }
}
