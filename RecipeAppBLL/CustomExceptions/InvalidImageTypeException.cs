using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppBLL.CustomExceptions 
{
    public class InvalidImageTypeException : Exception
    {
        public InvalidImageTypeException() : base("Invalid image type. Only JPG, JPEG, PNG, and GIF images are allowed.")
        {
        }
    }
}
