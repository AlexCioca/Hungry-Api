using Hungry_Api.DbModels;
using Hungry_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Hungry_Api.Repository
{
    public class TicketRepository:BaseRepository<Ticket>,ITicketRepository
    {
        public TicketRepository(HungryDbContext context) : base(context)
        {

        }
        public async Task<Ticket> GetById(int ticketId)
        {
            var ticket = await _dbSet.FirstOrDefaultAsync(x => x.TicketId == ticketId);
            return ticket;
        }
    }
}
