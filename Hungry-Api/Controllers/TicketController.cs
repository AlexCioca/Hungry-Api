using AutoMapper;
using Hungry_Api.DbModels;
using Hungry_Api.DTO;
using Hungry_Api.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hungry_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]"), Authorize(Roles = "ADMIN")]
    public class TicketController:ControllerBase
    {
        private IMapper Mapper { get; }
        private readonly IUnitOfWork _unitOfWork;
        public TicketController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.Mapper = mapper;
            this._unitOfWork = unitOfWork;

        }

        [HttpGet("GetTicketById")]
        public async Task<IActionResult> GetTicketById(int ticketId)
        {
            try
            {
                var ticket = await _unitOfWork.TicketRepository.GetById(ticketId);
                var mappedTicket = Mapper.Map<Ticket, TicketDTO>(ticket);

                return Ok(mappedTicket);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetTickets")]
        public async  Task<IActionResult> GetTickets()
        {
            try
            {
                var tickets = await _unitOfWork.TicketRepository.GetAllAsync();
                var mappedTickets = Mapper.Map<ICollection<Ticket>, ICollection<TicketDTO>>(tickets);

                return Ok(tickets);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("CreateTicket")]
        public async Task<IActionResult>CreateTicket(TicketDTO ticket)
        {
            try
            {
                var mappedTicket = Mapper.Map<TicketDTO, Ticket>(ticket);
                await _unitOfWork.TicketRepository.AddAsync(mappedTicket);
                await _unitOfWork.CompleteAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpDelete("CompleteTicket")]
        public async Task<IActionResult> CompleteTicket(int ticketId)
        {
            try
            {
                var ticket = await _unitOfWork.TicketRepository.GetById(ticketId);
                await _unitOfWork.TicketRepository.DeleteAsync(ticket);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
