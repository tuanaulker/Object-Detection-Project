using MediatR;
using Microsoft.AspNetCore.Identity;
using ObjectDetection.Application.Features.Token;
using ObjectDetection.CommonModels;
using ObjectDetection.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDetection.Application.Features.User
{
    
    public class LoginUserRequest : IRequest<ActionResponse<TokenDto>>
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }


    public class LoginUserCommand : IRequestHandler<LoginUserRequest, ActionResponse<TokenDto>>
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly IMediator _mediator;


        public LoginUserCommand(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMediator mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mediator = mediator;

        }

        public async Task<ActionResponse<TokenDto>> Handle(LoginUserRequest loginRequest, CancellationToken cancellationToken)
        {
            ActionResponse<TokenDto> response = new();
            response.IsSuccessful = false;

            var user = await _userManager.FindByNameAsync(loginRequest.UserNameOrEmail)
                ?? await _userManager.FindByEmailAsync(loginRequest.UserNameOrEmail);

            if (user == null)
            {
                response.Message = "Username or email incorrect.";
                return response;
            }

            if (!user.Status)
            {
                response.Message = "Your account is inactive. Please contact with your project leader.";
                return response;
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, false);

            if (result.Succeeded)
            {
                return await _mediator.Send(new TokenRequest { User = user });
            }
            else
            {
                response.Message = "password incorrect.";
                response.IsSuccessful = false;

            }
            return response;

        }
    }
}
