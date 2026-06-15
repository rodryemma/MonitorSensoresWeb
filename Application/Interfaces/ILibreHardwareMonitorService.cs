using Application.DTOs;
using Domain.Model.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ILibreHardwareMonitorService
    {
        Task<OperationResult<LHMSensorModel?>> ObtenerSensoresFull();
        Task<OperationResult<LHMSensorDTO>> ObtenerSensoresArbolFull();
        Task<OperationResult<Dictionary<string, LHMSensorResponseDTO>>> ObtenerEstadosAsync();

    }
}
