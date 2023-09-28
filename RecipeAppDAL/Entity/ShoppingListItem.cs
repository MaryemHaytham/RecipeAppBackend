using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppDAL.Entity
{
    public class ShoppingListItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsPurchased { get; set; } = false;
        public int ShoppingListId { get; set; }
        public ShoppingList ShoppingList { get; set; }
        public string? Category { get; set; }
    }
}
