using AutoMapper;
using Hungry_Api.DbModels;
using Hungry_Api.DTO;

namespace Hungry_Api.Profiles
{
    public class TicketProfile:Profile
    {
        public TicketProfile()
        {
            CreateMap<Ticket, TicketDTO>();
            CreateMap<TicketDTO, Ticket>();
        }
    }
}
