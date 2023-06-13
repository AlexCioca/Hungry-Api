using AutoMapper;
using Hungry_Api.DbModels;
using Hungry_Api.DTO;
using Hungry_Api.Hubs;
using Hungry_Api.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Hungry_Api.Controllers
{
    [Authorize]
    [Route("api/chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {

        private IMapper Mapper { get; }
        private readonly IUnitOfWork _unitOfWork;
        public ChatController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.Mapper = mapper;
            this._unitOfWork = unitOfWork;

        }

        [HttpGet("GetConversationMessages")]
        public async Task<IActionResult> GetConversationMessages(int receiverId)
        {
            try
            {
                var bearer_token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(bearer_token) as JwtSecurityToken;

                var userId = jsonToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value;
                var messages = await _unitOfWork.MessageRepository.GetMessagesForConversation(receiverId, int.Parse(userId));
                var mappedMessages = Mapper.Map<ICollection<Message>, ICollection<MessageDTO>>(messages);
                
                foreach (var m in mappedMessages.Where(data => data.Seen == false).ToList())
                {
                    if (int.Parse(userId) != m.SenderId)
                    {
                        var message = await _unitOfWork.MessageRepository.SeenMessage(m);
                        await _unitOfWork.MessageRepository.UpdateAsync(message);
                    }
                }
                await _unitOfWork.CompleteAsync();
                return Ok(mappedMessages);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetNewMessagesForUser")]
        public async Task<IActionResult> GetNewMessagesForUser()
        {
            try
            {
                var bearer_token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(bearer_token) as JwtSecurityToken;

                var userId = jsonToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value;
                var messages = await _unitOfWork.MessageRepository.GetNewMessagesForUser(int.Parse(userId));
                var mappedMessages = Mapper.Map<ICollection<Message>, ICollection<MessageDTO>>(messages);

             
                return Ok(mappedMessages);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("SeenMessage")]
        public async Task<IActionResult> SeenMessage([FromBody] MessageDTO  message)
        {
            try
            {
                var mappedMessage = await _unitOfWork.MessageRepository.SeenMessage(message);

                
                await _unitOfWork.MessageRepository.UpdateAsync(mappedMessage);
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
