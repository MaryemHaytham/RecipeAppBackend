using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppDAL.Entity
{
    public class Categories
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public List<Recipe> Recipes { get; set; }

    }
}
