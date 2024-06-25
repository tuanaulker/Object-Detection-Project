using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ObjectDetection.CommonModels;
using ObjectDetection.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDetection.Application.Features.Token
{
    public class TokenRequest : IRequest<ActionResponse<TokenDto>>
    {
        public AppUser User { get; set; }
    }

    public class TokenCommand : IRequestHandler<TokenRequest, ActionResponse<TokenDto>>
    {
        readonly IConfiguration _config;
        private readonly UserManager<AppUser> _userManager;

        public TokenCommand(IConfiguration config, UserManager<AppUser> userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        public async Task<ActionResponse<TokenDto>> Handle(TokenRequest tokenRequest, CancellationToken cancellationToken)
        {
            ActionResponse<TokenDto> response = new();
            TokenDto token = new();
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_config["Token:SecurityKey"]));
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(tokenRequest.User);

            var claims = new List<Claim>
            {
                new Claim("UserId", tokenRequest.User.Id),
                new Claim("Username", tokenRequest.User.UserName),
                new Claim("UserName", tokenRequest.User.Name),
                new Claim("UserSurname", tokenRequest.User.Surname),
                new Claim("Email", tokenRequest.User.Email),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var date = DateTime.UtcNow;
            token.Expiration = date.AddDays(6); // TODO


            JwtSecurityToken tokenSecurityToken = new(
                audience: _config["Token:Audience"],
                issuer: _config["Token:Issuer"],
                expires: token.Expiration,
                notBefore: date,
                signingCredentials: signingCredentials,
                claims: claims);

            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(tokenSecurityToken);

            response.Data = token;
            response.IsSuccessful = true;

            return response;

        }
    }
}
