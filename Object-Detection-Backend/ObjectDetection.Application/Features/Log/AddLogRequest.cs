using MediatR;
using ObjectDetection.CommonModels;
using ObjectDetection.Domain;
using ObjectDetection.Domain.Dtos;
using ObjectDetection.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDetection.Application.Features.Log
{
    public class AddLogRequest : IRequest<ActionResponse<Domain.Entities.Log>>
    {
        public string EventType { get; set; }
        public string CapturedImage { get; set; }
        public string Location { get; set; }
    }

    public class AddLogCommand : IRequestHandler<AddLogRequest, ActionResponse<Domain.Entities.Log>>
    {
        readonly ObjectDetectionDbContext _detectionDbContext;
        readonly GenericService<Domain.Entities.Log> _genericService;

        public AddLogCommand(ObjectDetectionDbContext detectionDbContext, GenericService<Domain.Entities.Log> genericService)
        {
            _detectionDbContext = detectionDbContext;
            _genericService = genericService;
        }

        public async Task<ActionResponse<Domain.Entities.Log>> Handle(AddLogRequest addLogRequest, CancellationToken cancellationToken)
        {
            ActionResponse<Domain.Entities.Log> response = new();
            response.IsSuccessful = false;

            Domain.Entities.Log log = new();
            log.Id = Guid.NewGuid();
            log.EventType = addLogRequest.EventType;
            log.CapturedImage = addLogRequest.CapturedImage;
            log.CapturedTime = DateTime.UtcNow;
            log.Location = addLogRequest.Location;
            log.Areas = "Manufacturing Area";
            log.Status = true;
            log.ActionStatus = "Alert";
            log.Confidence = "86%";
            log.Safezone = 1;
            log.IsPublished = false;

            //await _detectionDbContext.Logs.AddAsync(log);
            //await _detectionDbContext.SaveChangesAsync();
            _genericService.Add(log);

            response.Data = log;
            response.IsSuccessful = true;
            return response;

        }
    }
}

