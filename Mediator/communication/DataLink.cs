using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator.communication
{
    class DataLink
    {
        public DataLink(DateTime realTime, DateTime otherTime, bool isOpen, double value, long id, int from)
        {
            RealTime = realTime;
            OtherTime = otherTime;
            IsOpen = isOpen;
            Value = value;
            Id = id;
            From = from;
        }

        public DateTime RealTime { get; set; }
        public DateTime OtherTime { get; set; }
        public bool IsOpen { get; set; }
        public double Value { get; set; }
        public long Id { get; set; }
        public long From { get; set; }

    }
}
