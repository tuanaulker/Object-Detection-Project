﻿using ObjectDetection.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDetection.CommonModels.Repositories
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserInfoRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public UserInfo User
        {
            get
            {
                var user = new UserInfo()
                {
                    Id = new Guid(_httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value ?? ""),
                    Username = _httpContextAccessor.HttpContext.User.FindFirst("Username")?.Value ?? "",
                    Role = _httpContextAccessor.HttpContext.User.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value ?? "",
                };
                return user;
            }
        }
    }
}
