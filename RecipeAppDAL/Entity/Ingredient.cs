using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppDAL.Entity
{
    public class Ingredient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
       
        public decimal? Quantity { get; set; }

        public string? TypeMeasurement { get; set; }

        [Required]
        [ForeignKey("Recipe")]
        public int RecipeID { get; set; }
        public virtual Recipe Recipe { get; set; }


    }
}
