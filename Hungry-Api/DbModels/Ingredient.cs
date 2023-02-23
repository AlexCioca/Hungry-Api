using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hungry_Api.DbModels
{
    public class Ingredient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IngredientsId { get; set; }
        public string IngredientsName { get; set; }
        public int Calories { get; set; }
        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; } = null;
    }
}
