using ETradeApi.Application.Abstractions.Services;
using ETradeApi.Application.Abstractions.Token;
using ETradeApi.Application.DTOs;
using ETradeApi.Application.DTOs.Login;
using ETradeApi.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeApi.Application.Features.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly IUserService _userService;
        public LoginUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            LoginUserResponse res = await _userService.LoginUserAsync(new() { Password=request.Password,UserNameorEmail = request.UserNameorEmail});
            if(res.token != null)
            {
                return new LoginUserSuccessCommandResponse() { Token = res.token };
            }
            else
            {
                return new LoginUserErrorCommandResponse() { Message = res.Message };
            }
        }
    }
}
