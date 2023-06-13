using AutoMapper;
using Hungry_Api.DbModels;
using Hungry_Api.DTO;

namespace Hungry_Api.Profiles
{
    public class MessageProfile:Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageDTO>();
            CreateMap<MessageDTO, Message>();
        }
        
    }
}
