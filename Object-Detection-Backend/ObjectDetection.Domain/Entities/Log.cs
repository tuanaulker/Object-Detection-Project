using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDetection.Domain.Entities
{
    public class Log
    {
        public Guid Id { get; set; }
        public string EventType { get; set; }
        public DateTime CapturedTime { get; set; }
        public string CapturedImage { get; set; }
        public string Location { get; set; }
        public string Confidence { get; set; }
        public int Safezone { get; set; }
        public string Areas {  get; set; }
        public Guid? UserId { get; set; }
        public string? ActionTaken { get; set; }
        public DateTime? ActionTime { get; set; }
        public string? Details { get; set; }
        public string ActionStatus { get; set; }
        public bool Status { get; set; } 
        public bool IsPublished { get; set; }
    }
}
