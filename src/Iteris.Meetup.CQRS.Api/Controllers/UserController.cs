﻿using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Iteris.Meetup.CQRS.Command.Commands;
using Iteris.Meetup.CQRS.Query.Queries;
using Iteris.Meetup.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Iteris.Meetup.CQRS.Api.Controllers
{
    [ApiController]
    [Route("/")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("user")]
        [ProducesResponseType(typeof(int), (int) HttpStatusCode.Created)]
        [ProducesResponseType(typeof(List<string>), (int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(List<string>), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand createUserCommand)
        {
            var response = await _mediator.Send(createUserCommand);
            return StatusCode(response.StatusCode, response.GetResponse);
        }

        [HttpPost("address")]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.Created)]
        [ProducesResponseType(typeof(List<string>), (int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(List<string>), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddressCommand createAddressCommand)
        {
            var response = await _mediator.Send(createAddressCommand);
            return StatusCode(response.StatusCode, response.GetResponse);
        }

        [HttpGet("address/{userId}")]
        [ProducesResponseType(typeof(List<UserAddress>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(List<string>), (int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(List<string>), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAddresses([FromRoute] int userId)
        {
            var query = new UserAddressesQuery(userId);
            var response = await _mediator.Send(query);
            return StatusCode(response.StatusCode, response.GetResponse);
        }
    }
}