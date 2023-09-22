using AutoMapper;
using RecipeAppDAL.Entity;
using RecipeAppDTO.RecipeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppBLL.Utilities.AutoMaper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Recipe, AddRecipeDTO>().ReverseMap();
            CreateMap<Recipe, EditRecipeDTO>().ReverseMap();
            CreateMap<RecipeToReturnDTO, Recipe>().ReverseMap();
        }
    }
}
