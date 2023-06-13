using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Hungry_Api.DbModels
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }
        public int UserId { get; set; }
        public string Reason { get; set; }
        public int? RecipeId { get; set; }
        public virtual Recipe? Recipe { get; set; } = null;
        public virtual User User { get; set; } = null;
    }
}
