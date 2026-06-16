using Domain.Model.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Interface
{
    public interface ISensoresRepository
    {
        Task<OperationResult<List<LHMSensorDBModel>>> BuscarSensorFullAsync();
        Task<OperationResult<List<LHMSensorDBModel>>> BuscarSensorHiddenAsync(bool hidden);
        Task<OperationResult<int>> InsertarSensorAsync(LHMSensorDBModel lHMSensorDBModel);
        Task<OperationResult<int>> InsertarSensoresAsync(List<LHMSensorDBModel> listlHMSensor);
        Task<OperationResult<int>> EliminarFullSensorAsync();
    }
}
