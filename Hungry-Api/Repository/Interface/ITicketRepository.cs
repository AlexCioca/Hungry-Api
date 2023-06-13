using Hungry_Api.DbModels;

namespace Hungry_Api.Repository.Interface
{
    public interface ITicketRepository:IBaseRepository<Ticket>
    {

        Task<Ticket> GetById(int ticketId);
    }
}
