using Aelgi.Dragh2.Core.Crafting.Recipes;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Aelgi.Dragh2.Core.Crafting
{
    public class RecipeManager
    {
        public List<Recipe> GetAllRecipes()
        {
            var recipes = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsClass && x.Namespace.EndsWith("Recipes") && x.Name != "Recipe");

            var requiredItems = recipes.Select(x => x.GetMethod("RequiredItems"));

            return null;
        }
    }
}
