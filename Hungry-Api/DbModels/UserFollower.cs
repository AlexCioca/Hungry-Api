using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hungry_Api.DbModels
{
    public class UserFollower
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserFollowerId { get; set; }
        public int? CurrentUserId { get; set; }
        public int? FollowerId { get; set; }
        [ForeignKey("CurrentUserId")]
        public User? CurrentUser { get; set; }
        [ForeignKey("FollowerId")]
        public User? Follower { get; set; }
    }
}
