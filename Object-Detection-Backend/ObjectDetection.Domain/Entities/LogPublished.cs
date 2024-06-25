using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDetection.Domain.Entities
{
    public class LogPublished
    {
        public int Id { get; set; }
        public Guid LogId { get; set; }
        public Guid UserId { get; set; }
        public bool IsShow { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ShownTime { get; set; }


    }
}
