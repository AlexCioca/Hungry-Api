using Hungry_Api.DbModels;
using Hungry_Api.DTO;

namespace Hungry_Api.Repository.Interface
{
    public interface IMessageRepository:IBaseRepository<Message>
    {
        
        Task<ICollection<Message>> GetMessagesForConversation(int reciverId, int senderId);
        Task<ICollection<Message>> GetNewMessagesForUser(int userId);
        Task<Message> SeenMessage(MessageDTO message);
    }
}
