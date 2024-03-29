﻿using RecipeAppDAL.Entity;
using RecipeAppDAL.Repositories;
using RecipeAppDAL.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppBLL.Search
{
    internal class SearchByName : ISearch
    {

        public IEnumerable<Recipe> Search(string recipeName, IRecipeRepository _recipeRepository)
        {
            IEnumerable<Recipe> matchingRecipes = _recipeRepository.GetRecipesByName(recipeName);
            return matchingRecipes;
        }
    }
}

