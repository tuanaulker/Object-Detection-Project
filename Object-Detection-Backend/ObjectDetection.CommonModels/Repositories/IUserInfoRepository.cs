using ObjectDetection.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDetection.CommonModels.Repositories
{
    public interface IUserInfoRepository
    {
        public UserInfo User { get; }
    }
}
