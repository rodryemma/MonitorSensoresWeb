using Domain.Model.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Interface
{
    public interface ILibreHardwareMonitorApi
    {
        Task<OperationResult<LHMSensorModel?>> ObtenerSensoresFull();

    }
}
