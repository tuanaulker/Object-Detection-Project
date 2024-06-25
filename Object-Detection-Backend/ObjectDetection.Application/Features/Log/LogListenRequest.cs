using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ObjectDetection.CommonModels;
using ObjectDetection.CommonModels.Repositories;
using ObjectDetection.Domain.Dtos;
using ObjectDetection.Domain.Entities;
using ObjectDetection.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDetection.Application.Features.Log
{
    public class LogListenRequest :  IRequest<ActionResponse<List<AlertDto>>>
    {
    }

    public class LogListenCommand : IRequestHandler<LogListenRequest, ActionResponse<List<AlertDto>>>
    {
        readonly ObjectDetectionDbContext _objectDetectionDbContext;
        readonly IUserInfoRepository _userInfoRepository;

        public LogListenCommand(ObjectDetectionDbContext objectDetectionDbContext, IUserInfoRepository userInfoRepository)
        {
            _objectDetectionDbContext = objectDetectionDbContext;
            _userInfoRepository = userInfoRepository;
        }

        public async Task<ActionResponse<List<AlertDto>>> Handle(LogListenRequest listenRequest, CancellationToken cancellationToken)
        {
            ActionResponse<List<AlertDto>> response = new();
            response.IsSuccessful = false;

            string logQuery = @"SELECT l.Id, l.EventType, l.CapturedTime, l.Location, l.Confidence, l.Safezone, l.ActionStatus 
                                FROM logs l 
                                WHERE l.Status = true AND l.ActionStatus = 'Alert'
                                ORDER BY l.CapturedTime DESC";

            try
            {
                var alerts = await _objectDetectionDbContext.Database.GetDbConnection().QueryAsync<AlertDto>(logQuery);
                if (alerts != null)
                {
                    var userId = _userInfoRepository.User.Id;
                    var currentTime = DateTime.UtcNow;

                    //foreach (var alert in alerts)
                    //{
                    //    var logPublished = new LogPublished
                    //    {
                    //        LogId = alert.Id,
                    //        UserId = userId,
                    //        IsShow = false,
                    //        CreatedTime = currentTime,
                    //        ShownTime = DateTime.MinValue 
                    //    };

                    //    _objectDetectionDbContext.PublishedLogs.Add(logPublished);
                    //    //alert.IsPublished = true;
                    //    //_objectDetectionDbContext.Logs.Update(alert);
                    //}
                }

                //await _objectDetectionDbContext.SaveChangesAsync();
                response.IsSuccessful = true;
                response.Data = alerts.ToList();

            }
            catch (Exception ex)
            {

            }
            
            return response;
        }
    }
}
