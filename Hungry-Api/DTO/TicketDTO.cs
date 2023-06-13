namespace Hungry_Api.DTO
{
    public class TicketDTO
    {
        public int TicketId { get; set; }
        public int UserId { get; set; }
        public string Reason { get; set; }
        public int? RecipeId { get; set; }
    }
}
