using Azure.Core;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObjectDetection.Application.Features.Token;
using ObjectDetection.Application.Features.User;
using ObjectDetection.CommonModels;
using ObjectDetection.Domain.Dtos;
using ObjectDetection.Domain.Entities;
using ObjectDetection.Infrastructure.Context;

namespace ObjectDetection.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly ObjectDetectionDbContext _objectDetectionDbContext;
        readonly UserManager<AppUser> _userManager;


        public UserController(IMediator mediator, ObjectDetectionDbContext objectDetectionDbContext, UserManager<AppUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
            _objectDetectionDbContext = objectDetectionDbContext;
        }


        [HttpPost] //TODO Admin + SuperAdmin
        public async Task<ActionResponse<UserOperationDto>> AddUser(AddUserRequest addUserRequest)
        {
            return await _mediator.Send(addUserRequest);
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginUserRequest loginUserRequest)
        {
            ActionResponse<TokenDto> token = await _mediator.Send(loginUserRequest);
            return Ok(token);
        }

        [HttpPost]
        public async Task<ActionResponse<UserOperationDto>> UpdateUser(UpdateUserRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost]
        public async Task<ActionResponse<UserOperationDto>> DeleteUser(DeleteUserRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet]
        public async Task<ActionResponse<List<UserOperationDto>>> GetAllUsers()
        {
            ActionResponse<List<UserOperationDto>> response = new();
            response.IsSuccessful = false;

            try
            {
                string userQuery = @"SELECT u.Id, u.Name, u.Surname, u.UserName, u.Email, u.ProfileUrl, u.Status, ur.RoleId AS Role
                    FROM aspnetusers u
                    JOIN  ""AspNetUserRoles"" ur ON u.Id = ur.UserId ORDER BY u.UserName ASC";
                var users = _objectDetectionDbContext.Database.GetDbConnection().QueryAsync<UserOperationDto>(userQuery);
                response.Data = users.Result.ToList();

                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.Message = ex.Message;
            }


            return response;

        }

        [HttpPost]
        public async Task<ActionResponse<UserDto>> GetUserById(GetUserByIdRequest getUserByIdRequest)
        {
            return await _mediator.Send(getUserByIdRequest);
        }
    }
}
