using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class LHMSensorDTO
    {
        public string Text { get; set; }
        public bool Hidden { get; set; } // false = visible
        public string SensorId { get; set; }
        public List<LHMSensorDTO> Children { get; set; } = new List<LHMSensorDTO>();
    }

}
