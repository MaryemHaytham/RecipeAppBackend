using System.ComponentModel.DataAnnotations;

namespace RecipeApp.ViewModels
{
    public class AddRecipeVM
    {
        [MinLength(4)] // Enforce minimum length of 4 characters
        public string RecipeName { get; set; }

        [Required]
        [MinLength(1)] // Enforce minimum length of 1 character
        public string Ingredients { get; set; }

        [Required]
        [MinLength(4)] // Enforce minimum length of 1 character
        public string Steps { get; set; }

        public string? Image { get; set; }


        public int UserId { get; set; } // Foreign key
    }
}
