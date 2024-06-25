using AutoMapper;
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
    public class GetUserByIdRequest : IRequest<ActionResponse<UserDto>>
    {
        public Guid Id { get; set; }
    }


    public class GetUserByIdCommand : IRequestHandler<GetUserByIdRequest, ActionResponse<UserDto>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public GetUserByIdCommand(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ActionResponse<UserDto>> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
            ActionResponse<UserDto> response = new();
            AppUser user = await _userManager.FindByIdAsync(request.Id.ToString());

            response.IsSuccessful = false;
            if (user != null)
            {
                response.Data = _mapper.Map<UserDto>(user);
                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles != null)
                {
                    response.Data.Role = userRoles[0];
                }
                else
                {
                    response.Data.Role = "Role cannot found.";
                }
                response.IsSuccessful = true;
            }
            else
                response.Message = "User Not Found";

            return response;
        }
    }


}
