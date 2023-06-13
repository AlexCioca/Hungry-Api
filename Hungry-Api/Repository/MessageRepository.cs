using Hungry_Api.DbModels;
using Hungry_Api.DTO;
using Hungry_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Hungry_Api.Repository
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(HungryDbContext context) : base(context) { }

        public async Task<ICollection<Message>> GetMessagesForConversation(int receiverId, int senderId)
        {
            var messages = await _dbSet.Where(m => (m.ReciverId == receiverId && m.SenderId == senderId) || (m.ReciverId==senderId && m.SenderId==receiverId))
            .ToListAsync();
            return messages;
        }
        public async Task<ICollection<Message>> GetNewMessagesForUser(int userId)
        {
            var messages = await _dbSet.Where(message => message.ReciverId==userId && message.Seen==false).ToListAsync();
            return messages;
        }
        public async Task<Message> SeenMessage(MessageDTO message)
        {
            var m = _dbSet.FirstOrDefaultAsync(mess => mess.SenderId == message.SenderId && mess.ReciverId == message.ReciverId && mess.TimeStamp == message.TimeStamp).Result;
            m.Seen = true;
            return m;
        }
    

    }
}
