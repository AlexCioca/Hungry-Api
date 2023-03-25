﻿using AutoMapper;
using Hungry_Api.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Hungry_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IMapper Mapper { get; }
        private readonly IUnitOfWork _unitOfWork;
        public UserController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.Mapper = mapper;
            this._unitOfWork = unitOfWork;

        }
    }
}
