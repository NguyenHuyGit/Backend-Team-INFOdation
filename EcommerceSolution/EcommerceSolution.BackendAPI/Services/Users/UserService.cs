using EcommerceSolution.BackendAPI.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using EcommerceSolution.BackendAPI.Data.Entities;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System;
using EcommerceSolution.BackendAPI.ViewModels.Users;

namespace EcommerceSolution.BackendAPI.Services.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;

        public UserService(UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        public async Task<ApiResult<string>> Login(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if(user == null) 
                return new ApiErrorResult<string>("Tài khoản không tồn tại");
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, true);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<string>("Đăng nhập không đúng");
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));

        }
    }
}
