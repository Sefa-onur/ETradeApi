using ETradeApi.Application.Abstractions.Services;
using ETradeApi.Application.Abstractions.Token;
using ETradeApi.Application.DTOs;
using ETradeApi.Application.DTOs.Login;
using ETradeApi.Application.DTOs.User;
using ETradeApi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeApi.Persistence.Services
{
    public class UserService : IUserService

    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signinmanager;
        readonly ITokenHandler _token;
        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signinmanager,ITokenHandler token)
        {
            _userManager = userManager;
            _signinmanager = signinmanager;
            _token = token;
        }
        public async Task<CreateUserResponse> CreateUserAsync(CreateUser model)
        {
            
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                NameSurname = model.NameSurname,
                UserName = model.Username,
                Email = model.Email,
            },model.Password);

            if (result.Succeeded)
                return new() { Message = "Kayıt Başarılı",Succeeded = true };
            else
                return new() { Message = "Kayıt Başarısız",Succeeded = false };
        }
        public async Task<LoginUserResponse> LoginUserAsync(LoginUserRequest model)
        {
            AppUser user = await _userManager.FindByEmailAsync(model.UserNameorEmail);

            if(user == null)
            {
                user = await _userManager.FindByNameAsync(model.UserNameorEmail);
            }
            if(user == null)
            {
                throw new Exception("Kullanıcı İsmi Hatalı");
            }

            SignInResult result = await _signinmanager.CheckPasswordSignInAsync(user, model.Password, false);

            if (result.Succeeded)
            {
                Token token = _token.CreateAccessToken(5);
                await UpdateRefreshToken(user.RefreshToken, user.Id,token.Expiration,5);                
                return new() { token = token };
            }
            else
            {
                return new() { Message = "Kullanıcı Adı Yada Şifre Hatalı"};
            }
        }

        public async Task UpdateRefreshToken(string refreshtoken, string id, DateTime accessTokenDate, int refreshTokenLifeTime)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if(user != null)
            {
                user.RefreshToken = refreshtoken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(refreshTokenLifeTime);
                await _userManager.UpdateAsync(user);                
            }
            else
            {
                throw new Exception("Kullanıcı Bulunamadı");
            }
        }
    }
}
