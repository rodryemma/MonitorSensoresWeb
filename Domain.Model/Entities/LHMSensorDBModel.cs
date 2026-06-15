using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Entities
{
    public class LHMSensorDBModel
    {
        public string SensorId { get; set; }
        public string Text { get; set; }
        public bool Hidden { get; set; } 
    }
}
