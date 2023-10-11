using RecipeAppDAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppDTO.ShoppingListDTO
{
    public class ShoppingListDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<ShoppingListItem> Items { get; set; }
    }
}
