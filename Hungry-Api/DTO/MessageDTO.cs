namespace Hungry_Api.DTO
{
    public class MessageDTO
    {
        public int MessageId { get; set; }
        public int SenderId { get; set; }
        public int ReciverId { get; set; }
        public string MessageText { get; set; }
        public bool Seen { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
