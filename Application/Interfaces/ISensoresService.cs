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
    public interface ISensoresService
    {
        Task<OperationResult<List<LHMSensorDBModel>>> BuscarSensorFullAsync();
        Task<OperationResult<int>> InsertarSensorAsync(LHMSensorDBModel lHMSensorDBModel);
        Task<OperationResult<int>> InsertarSensoresAsync(List<LHMSensorDBModel> listlHMSensor);
        Task<OperationResult<int>> GestionarEstadosSensores(LHMSensorDTO lHMSensor);
        Task<OperationResult<int>> EliminarFullSensorAsync();
        Task<OperationResult<LHMSensorDTO>> EstadoActualSensores();

    }
}
