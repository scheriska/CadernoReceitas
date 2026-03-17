
namespace SocialCook.Aplication.DTOs.Recipes
{
    public class RecipeResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string PreparationMethod { get; set; }
        public string Status { get; set; }
        public string Visibility { get; set; } 
        public DateTime CreatedAt { get; set; }


        public List<RecipeIngredientResponse> Ingredients { get; set; }

        public List<RecipeImageResponse> Images { get; set; }
        public List<RecipeCategoryResponse> Categories { get; set; }
        public List<RecipeBeverageResponse> Beverages { get; set; }
    }

    public class RecipeIngredientResponse
    {
        public string Name { get; set; }

        public string Quantity { get; set; }

        public string Unit { get; set; }
    }

    public class RecipeImageResponse
    {
        public string Url { get; set; }
        public int Order { get; set; }
    }

    public class RecipeCategoryResponse
    {
        public string Name { get; set; }
    }

    public class RecipeBeverageResponse
    {
        public string Name { get; set; }
        public string Note { get; set; }
    }
}