using ETradeApi.Application.DTOs.Login;
using ETradeApi.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeApi.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateUserAsync(CreateUser model);
        Task<LoginUserResponse> LoginUserAsync(LoginUserRequest model);
        Task UpdateRefreshToken(string refreshtoken,string id, DateTime accessTokenDate, int refreshTokenLifeTime);
    }
}
