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
    public class AddUserRequest : IRequest<ActionResponse<UserOperationDto>>
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string ProfileUrl { get; set; }
    }


    public class AddUserCommand : IRequestHandler<AddUserRequest, ActionResponse<UserOperationDto>>
    {

        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<AppRole> _roleManager;


        public AddUserCommand(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ActionResponse<UserOperationDto>> Handle(AddUserRequest addUserRequest, CancellationToken cancellationToken)
        {
            ActionResponse<UserOperationDto> response = new();
            UserOperationDto userDto = new();
            response.IsSuccessful = false;

            var roleExists = await _roleManager.RoleExistsAsync(addUserRequest.Role);

            if (roleExists)
            {
                var user = new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = addUserRequest.UserName,
                    Name = addUserRequest.Name,
                    Surname = addUserRequest.Surname,
                    Email = addUserRequest.Email,
                    ProfileUrl = addUserRequest.ProfileUrl,
                    Status = true
                };
                //var password = GeneratePassword(3, 3, 3); //TODO WITH MAIL
                //IdentityResult result = await _userManager.CreateAsync(user, password);

                IdentityResult result = await _userManager.CreateAsync(user, addUserRequest.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, addUserRequest.Role);
                    userDto.Username = addUserRequest.UserName;
                    userDto.Id = user.Id;
                    userDto.Surname = user.Surname;
                    userDto.Name = addUserRequest.Name;
                    userDto.Role = addUserRequest.Role;
                    userDto.Email = addUserRequest.Email;
                    userDto.ProfileUrl = addUserRequest.ProfileUrl;
                    //userDto.Password = password;
                    response.Data = userDto;
                    response.IsSuccessful = true;

                }
                else
                {
                    foreach (var error in result.Errors)
                        response.Message += $"{error.Code} - {error.Description}\n";

                }
            }
            else
            {
                response.Message = "Role doesn't exist";
            }

            return response;
        }

        public static string GeneratePassword(int lowercase, int uppercase, int numerics)
        {
            string lowers = "abcdefghijklmnopqrstuvwxyz";
            string uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string number = "0123456789";

            Random random = new Random();

            string generated = "!";
            for (int i = 1; i <= lowercase; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    lowers[random.Next(lowers.Length - 1)].ToString()
                );

            for (int i = 1; i <= uppercase; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    uppers[random.Next(uppers.Length - 1)].ToString()
                );

            for (int i = 1; i <= numerics; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    number[random.Next(number.Length - 1)].ToString()
                );

            return generated.Replace("!", string.Empty);

        }

    }
}


