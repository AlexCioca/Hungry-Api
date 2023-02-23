using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hungry_Api.DbModels
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<Like> Users { get; } = new List<Like>();
        public ICollection<RecipeCategory> Recipes { get; } = new List<RecipeCategory>();
    }
}
