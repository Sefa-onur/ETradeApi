using ETradeApi.Application.Features.Commands.CreateUser;
using ETradeApi.Application.Features.Commands.GoogleLogin;
using ETradeApi.Application.Features.Commands.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETradeApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest req)
        {
            CreateUserCommandResponse res = await _mediator.Send(req);
            return Ok(res);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> LoginUser(LoginUserCommandRequest req)
        {
            LoginUserCommandResponse res = await _mediator.Send(req);
            return Ok(res);
        }
        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest req)
        {
            GoogleLoginCommandResponse response = await _mediator.Send(req);
            return Ok(response);
        }
        
    }
}
