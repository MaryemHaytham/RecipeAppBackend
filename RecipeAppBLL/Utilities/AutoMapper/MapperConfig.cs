using AutoMapper;
using RecipeAppDAL.Entity;
using RecipeAppDAL.Entity.RecipeAppDAL.Entity;
using RecipeAppDTO.MealPlanDTO;
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
            CreateMap<RecipeDTO, Recipe>().ReverseMap();
            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<Rating, RatingDto>().ReverseMap();
            CreateMap<Plans,MealPlanDTO>().ReverseMap();
            CreateMap<MealPlanDTO, Plans>().ReverseMap();

        }
    }
}
