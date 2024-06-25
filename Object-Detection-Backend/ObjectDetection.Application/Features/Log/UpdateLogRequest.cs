using MediatR;
using Microsoft.EntityFrameworkCore;
using ObjectDetection.CommonModels;
using ObjectDetection.CommonModels.Repositories;
using ObjectDetection.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDetection.Application.Features.Log
{
    public class UpdateLogRequest : IRequest<ActionResponse<Domain.Entities.Log>>
    {
        public Guid Id { get; set; }
        public string? ActionTaken { get; set; }
        public string? Details { get; set; }
        public string ActionStatus { get; set; }
    }

    public class UpdateLogCommand : IRequestHandler<UpdateLogRequest, ActionResponse<Domain.Entities.Log>>
    {
        readonly ObjectDetectionDbContext _detectionDbContext;
        readonly GenericService<Domain.Entities.Log> _genericService;
        readonly IUserInfoRepository _userInfoRepository;

        public UpdateLogCommand(ObjectDetectionDbContext detectionDbContext, GenericService<Domain.Entities.Log> genericService, IUserInfoRepository userInfoRepository)
        {
            _detectionDbContext = detectionDbContext;
            _genericService = genericService;
            _userInfoRepository = userInfoRepository;
        }

        public async Task<ActionResponse<Domain.Entities.Log>> Handle(UpdateLogRequest updateLogRequest, CancellationToken cancellationToken)
        {
            ActionResponse<Domain.Entities.Log> response = new();
            response.IsSuccessful = false;

            Domain.Entities.Log log = await _detectionDbContext.Logs.FirstOrDefaultAsync(d => d.Id == updateLogRequest.Id);
            if (log != null && log.Status == true)
            {
                //log.UserId = _userInfoRepository.User.Id;  OPEN AFTER USER DONE
                log.UserId = Guid.Parse("94c328af-952d-42a5-ae86-4f0fe6d84d74"); // default super admin for now
                log.ActionTaken = updateLogRequest.ActionTaken;
                log.ActionTime = DateTime.UtcNow;
                log.Details = updateLogRequest.Details;
                log.ActionStatus = updateLogRequest.ActionStatus;
             

                //await _detectionDbContext.SaveChangesAsync();
                _genericService.Update(log);

                response.Data = log;
                response.IsSuccessful = true;
            }
            else
            {
                response.Message = "Log can not find or status false!";
            }
            return response;
        }
    }
}
