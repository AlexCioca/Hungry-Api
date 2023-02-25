using Hungry_Api.DbModels;

namespace Hungry_Api.DTO
{
    public class LikeDTO
    {
        public int LikeId { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
    }
}
