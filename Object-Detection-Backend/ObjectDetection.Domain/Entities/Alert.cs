using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDetection.Domain.Entities
{
    public class Alert
    {
        public int Id { get; set; }
        public Guid RelatedLogId { get; set; }
        public DateTime Time { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}
