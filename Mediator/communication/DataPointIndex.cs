using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator.communication
{
    class DataPointIndex
    {
        public DataPointIndex(DateTime startTime, DateTime endTime, double value, int from)
        {
            StartTime = startTime;
            EndTime = endTime;
            Value = value;
            From = from;
        }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Value { get; set; }
        public int From { get; set; }
    }
}
