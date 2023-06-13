using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hungry_Api.DbModels
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MessageId { get; set; }
        public int SenderId { get; set; }
        public int ReciverId { get; set; }
        public string MessageText { get; set; }
        public bool Seen { get; set; }
        public DateTime TimeStamp { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual User Sender { get; set; } = null;
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual User Reciver { get; set; } = null;
    }
}
