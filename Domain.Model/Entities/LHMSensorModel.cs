using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Entities
{
    
    public class LHMSensorModel
    {
        public int id { get; set; }
        public string Text { get; set; }
        public string Min { get; set; }
        public string Value { get; set; }
        public string Max { get; set; }
        public string ImageURL { get; set; }
        public List<LHMSensorModel> Children { get; set; } = new List<LHMSensorModel>();
        public string HardwareId { get; set; }
        public string SensorId { get; set; }
        public string Type { get; set; }
        public string RawMin { get; set; }
        public string RawValue { get; set; }
        public string RawMax { get; set; }
    }
}
