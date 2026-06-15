using Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class LHMSensorResponseDTO
    {
        public string Type { get; set; }
        public string Text { get; set; }
        public string Min { get; set; }
        public string Value { get; set; }
        public string Max { get; set; }
        public bool Hidden { get; set; }

    }
}
