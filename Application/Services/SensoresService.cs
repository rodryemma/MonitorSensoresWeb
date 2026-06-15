using Application.DTOs;
using Application.Interfaces;
using Domain.Model.Entities;
using Domain.Model.Interface;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SensoresService : ISensoresService
    {
        ISensoresRepository _sensoresRepository;
        ILibreHardwareMonitorService _libreHardwareMonitorService;

        public SensoresService(ISensoresRepository sensoresRepository, ILibreHardwareMonitorService libreHardwareMonitorService)
        {
            _sensoresRepository = sensoresRepository;
            _libreHardwareMonitorService = libreHardwareMonitorService;
        }

        public async Task<OperationResult<List<LHMSensorDBModel>>> BuscarSensorFullAsync()
        {
            return await _sensoresRepository.BuscarSensorFullAsync();
        }

        public async Task<OperationResult<int>> InsertarSensorAsync(LHMSensorDBModel lHMSensorDBModel)
        {
            return await _sensoresRepository.InsertarSensorAsync(lHMSensorDBModel);
        }

        public async Task<OperationResult<int>> InsertarSensoresAsync(List<LHMSensorDBModel> listlHMSensor)
        {
            return await _sensoresRepository.InsertarSensoresAsync(listlHMSensor);
        }

        public async Task<OperationResult<int>> EliminarFullSensorAsync()
        {
            return await _sensoresRepository.EliminarFullSensorAsync();
        }

        public async Task<OperationResult<LHMSensorDTO>> EstadoActualSensores()
        {
            var sensoresArbol = await _libreHardwareMonitorService.ObtenerSensoresArbolFull();
            if (!sensoresArbol.Success) { return OperationResult<LHMSensorDTO>.Fail(sensoresArbol.Message); }

            var sensoresEstados = await _sensoresRepository.BuscarSensorFullAsync();
            if (!sensoresEstados.Success) { return OperationResult<LHMSensorDTO>.Fail(sensoresEstados.Message); }

            var hiddenDict = sensoresEstados.Data.ToDictionary(s => s.SensorId, s => s.Hidden);

            ActualizarHidden(sensoresArbol.Data, hiddenDict);

            return OperationResult<LHMSensorDTO>.Ok(sensoresArbol.Data);
        }

        void ActualizarHidden(LHMSensorDTO node, Dictionary<string, bool> hiddenDict)
        {
            if (node.SensorId != null && hiddenDict.TryGetValue(node.SensorId, out bool hidden))
            {
                node.Hidden = hidden;
            }

            if (node.Children?.Count > 0)
            {
                foreach (var child in node.Children)
                {
                    ActualizarHidden(child, hiddenDict);
                }
            }
        }


        public async Task<OperationResult<int>> GestionarEstadosSensores(LHMSensorDTO lHMSensor)
        {
            var resultado = new Dictionary<string, LHMSensorDBModel>();
            MapearDtoADiccionario(lHMSensor, resultado);
            var responseDel = await EliminarFullSensorAsync();
            if (!responseDel.Success) { return OperationResult<int>.Fail(responseDel.Message); }

            List<LHMSensorDBModel> listaSensores = new();
            listaSensores.AddRange(resultado.Values);

            var responseIns = await _sensoresRepository.InsertarSensoresAsync(listaSensores);
            if (!responseIns.Success) { return OperationResult<int>.Fail(responseIns.Message); }

            return OperationResult<int>.Ok(0);
        }

        private void MapearDtoADiccionario(LHMSensorDTO sensor, Dictionary<string, LHMSensorDBModel> resultado)
        {
            if (!string.IsNullOrEmpty(sensor.SensorId))
            {
                resultado[sensor.SensorId] =
                new LHMSensorDBModel()
                {
                    Hidden = sensor.Hidden,
                    SensorId = sensor.SensorId,
                    Text = sensor.Text

                };
            }

            foreach (var hijo in sensor.Children)
            {
                MapearDtoADiccionario(hijo, resultado);
            }
        }
    }
}
