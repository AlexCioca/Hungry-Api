using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Hungry_Api.DbModels
{
    public class RecipeReview
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecipeReviewId { get; set; }
        public int RecipeId { get; set; }
        public string Comment { get; set;}
        public int Rating { get; set; }
        public int UserId { get; set; }
        public virtual Recipe Recipe { get; set; } = null;
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual User User { get; set; } = null;
    }
}
