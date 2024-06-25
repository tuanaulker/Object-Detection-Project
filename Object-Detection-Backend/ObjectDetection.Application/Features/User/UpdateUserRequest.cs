using MediatR;
using Microsoft.AspNetCore.Identity;
using ObjectDetection.CommonModels;
using ObjectDetection.Domain.Dtos;
using ObjectDetection.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDetection.Application.Features.User
{
    public class UpdateUserRequest : IRequest<ActionResponse<UserOperationDto>>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string ProfileUrl { get; set; }
        public string Role { get; set; }
    }

    public class UpdateUserCommand : IRequestHandler<UpdateUserRequest, ActionResponse<UserOperationDto>>
    {
        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<AppRole> _roleManager;

        public UpdateUserCommand(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ActionResponse<UserOperationDto>> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            ActionResponse<UserOperationDto> response = new();
            response.IsSuccessful = false;

            AppUser user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user != null)
            {
                user.Name = request.Name;
                user.Surname = request.Surname;
                user.UserName = request.Username;
                user.Email = request.Email;
                user.ProfileUrl = request.ProfileUrl;

                if (await _roleManager.RoleExistsAsync(request.Role))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    if (userRoles[0] != request.Role)
                    {
                        await _userManager.RemoveFromRolesAsync(user, userRoles);
                        await _userManager.AddToRoleAsync(user, request.Role);
                    }

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        response.IsSuccessful = true;
                    }
                    else
                    {
                        response.IsSuccessful = false;
                        response.Message = "User cannot updated.";
                    }
                }
                return response;
            }
            else
            {
                response.IsSuccessful = false;
                response.Message = "User does not exist.";
            }
            return response;
        }

    }

}