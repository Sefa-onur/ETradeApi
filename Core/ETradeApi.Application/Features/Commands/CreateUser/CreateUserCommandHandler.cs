using ETradeApi.Application.Abstractions.Services;
using ETradeApi.Application.DTOs.User;
using ETradeApi.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity; 

namespace ETradeApi.Application.Features.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly IUserService _userService;
        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            CreateUserResponse res = await _userService.CreateUserAsync(new()
            {
                Email = request.Email,
                Username = request.UserName,
                Password = request.Password,
                PasswordConfirm = request.PasswordConfirm,
                NameSurname = request.NameSurname
            });

            return new()
            {
                Message = res.Message,
                Success = res.Succeeded
            };
        }
    }
}
