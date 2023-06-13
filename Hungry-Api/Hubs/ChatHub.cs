using AutoMapper;
using Hungry_Api.DbModels;
using Hungry_Api.DTO;
using Hungry_Api.Repository.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Hungry_Api.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private IMapper Mapper { get; }
        private readonly IUnitOfWork _unitOfWork;
        public ChatHub(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.Mapper = mapper;
            this._unitOfWork = unitOfWork;

        }

        public async Task SendMessage(MessageDTO message)
        {
            var sender = Context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;


            var user =  await _unitOfWork.UserRepository.GetUserByUsername(sender);

            message.SenderId = user.UserId;
            message.TimeStamp = DateTime.UtcNow;
            var reciver = await _unitOfWork.UserRepository.GetUserById(message.ReciverId);
            var mappedMessage = Mapper.Map<MessageDTO,Message>(message);
            await _unitOfWork.MessageRepository.AddAsync(mappedMessage);
            await _unitOfWork.CompleteAsync();

            await Clients.User(reciver.Username).SendAsync("ReceiveMessage", message);
        }



    }
}
