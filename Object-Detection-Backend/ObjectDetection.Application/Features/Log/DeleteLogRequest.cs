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
    public class DeleteLogRequest : IRequest<ActionResponse<Domain.Entities.Log>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteLogCommand : IRequestHandler<DeleteLogRequest, ActionResponse<Domain.Entities.Log>>
    {
        readonly ObjectDetectionDbContext _detectionDbContext;
        readonly GenericService<Domain.Entities.Log> _genericService;

        public DeleteLogCommand(ObjectDetectionDbContext detectionDbContext, GenericService<Domain.Entities.Log> genericService)
        {
            _detectionDbContext = detectionDbContext;
            _genericService = genericService;
        }

        public async Task<ActionResponse<Domain.Entities.Log>> Handle(DeleteLogRequest deleteLogRequest, CancellationToken cancellationToken)
        {
            ActionResponse<Domain.Entities.Log> response = new();
            response.IsSuccessful = false;

            Domain.Entities.Log log = await _detectionDbContext.Logs.FirstOrDefaultAsync(d => d.Id == deleteLogRequest.Id);
            if (log != null && log.Status == true)
            {
                log.Status = false;
                _genericService.Update(log);
                response.Data = log;
                response.IsSuccessful = true;
            }
            else
            {
                response.Message = "Log can not find or status already false!";
            }
            return response;
        }
    }
}
