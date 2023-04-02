namespace Hungry_Api.DbModels
{
    public class RecipeSteps
    {

        public int RecipeStepsId { get; set; }
        public string Description { get; set; }
        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; } = null;


    }
}
