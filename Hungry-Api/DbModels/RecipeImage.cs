using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hungry_Api.DbModels
{
    public class RecipeImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecipeImageId { get; set; }
        public string Image { get; set; }
        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; } = null;

    }
}
