using Dapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObjectDetection.Application.Features.Log;
using ObjectDetection.Application.Features.User;
using ObjectDetection.CommonModels;
using ObjectDetection.Domain.Dtos;
using ObjectDetection.Domain.Entities;
using ObjectDetection.Infrastructure.Context;
using System.Diagnostics;

namespace ObjectDetection.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class LogController : ControllerBase
    {

        readonly IMediator _mediator;
        readonly ObjectDetectionDbContext _objectDetectionDbContext;

        public LogController(IMediator mediator, ObjectDetectionDbContext objectDetectionDbContext)
        {
            _mediator = mediator;
            _objectDetectionDbContext = objectDetectionDbContext;
        }

        [HttpPost] 
        public async Task<ActionResponse<Log>> AddLog(AddLogRequest addLogRequest)
        {
            return await _mediator.Send(addLogRequest);
        }


        [HttpPost]
        public async Task<ActionResponse<Log>> UpdateLog(UpdateLogRequest updateLogRequest)
        {
            return await _mediator.Send(updateLogRequest);
        }

        [HttpPost]
        public async Task<ActionResponse<Log>> DeleteLog(DeleteLogRequest deleteLogRequest)
        {
            return await _mediator.Send(deleteLogRequest);
        }

        [HttpGet("{id}")]
        public async Task<ActionResponse<Log>> GetLogById(Guid id)
        {
            ActionResponse<Log> response = new();
            response.IsSuccessful = false;

            var log = await _objectDetectionDbContext.Logs.FirstOrDefaultAsync(d => d.Id == id);

            if (log == null)
            {
                response.Message = "Log Object can not found!";
            }
            else
            {
                response.Data = log;
                response.IsSuccessful = true;
            }

            return response; 
        }



        [HttpGet]
        public async Task<ActionResponse<List<LogDto>>> GetAllLogs()
        {
            ActionResponse<List<LogDto>> response = new();
            response.IsSuccessful = false;

            try
            {
                string logQuery = @"SELECT l.Id, l.EventType, l.CapturedTime, l.CapturedImage, l.Location, l.confidence, l.safezone, l.areas, l.Status, l.ActionStatus FROM logs l WHERE l.Status = True ORDER BY l.CapturedTime DESC";
                var logs = _objectDetectionDbContext.Database.GetDbConnection().QueryAsync<LogDto>(logQuery);
                response.Data = logs.Result.ToList();

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
        public async Task<ActionResponse<List<AlertDto>>> LogListen()
        {
            return await _mediator.Send(new LogListenRequest());
        }

    }
}
