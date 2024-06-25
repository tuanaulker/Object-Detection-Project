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
    public class DeleteUserRequest : IRequest<ActionResponse<UserOperationDto>>
    {
        public string UserId { get; set; }
    }

    public class DeleteUserCommand : IRequestHandler<DeleteUserRequest, ActionResponse<UserOperationDto>>
    {
        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<AppRole> _roleManager;

        public DeleteUserCommand(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ActionResponse<UserOperationDto>> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            ActionResponse<UserOperationDto> response = new();
            response.IsSuccessful = false;

            AppUser user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user != null)
            {
                user.Status = false;
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles); //TODO SuperAdmin can delete Admin and user, Admin can delete user. User cannot reach this method!
                response.IsSuccessful = true;
                return response;
            }
            else
            {
                response.IsSuccessful = false;
                response.Message = "User cannot found.";
            }
            return response;
        }

    }
}
