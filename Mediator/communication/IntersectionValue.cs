using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator.communication
{
    class IntersectionValue
    {
        public IntersectionValue(double value, DateTime start, DateTime end)
        {
            Value = value;
            StartTime = start;
            EndTime = end;
        }

        public double Value { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
     
    }
}
