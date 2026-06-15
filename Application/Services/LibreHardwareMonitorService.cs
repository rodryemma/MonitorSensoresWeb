using Application.DTOs;
using Application.Interfaces;
using Domain.Model.Entities;
using Domain.Model.Interface;
using Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class LibreHardwareMonitorService : ILibreHardwareMonitorService
    {
        ILibreHardwareMonitorApi _libreHardwareMonitorApi;

        public LibreHardwareMonitorService(ILibreHardwareMonitorApi libreHardwareMonitorApi)
        {
            _libreHardwareMonitorApi = libreHardwareMonitorApi;
        }

        public async Task<OperationResult<LHMSensorModel?>> ObtenerSensoresFull()
        {
            return await _libreHardwareMonitorApi.ObtenerSensoresFull();
        }

        public async Task<OperationResult<LHMSensorDTO>> ObtenerSensoresArbolFull()
        {
            var response = await _libreHardwareMonitorApi.ObtenerSensoresFull();
            if (!response.Success) { return OperationResult<LHMSensorDTO>.Fail(response.Message); }

            return OperationResult<LHMSensorDTO>.Ok(Mapear(response.Data));
        }

        public async Task<OperationResult<Dictionary<string, LHMSensorResponseDTO>>> ObtenerEstadosAsync()
        {
            var response = await _libreHardwareMonitorApi.ObtenerSensoresFull();
            if (!response.Success) { return OperationResult<Dictionary<string, LHMSensorResponseDTO>>.Fail(response.Message); }

            var resultado = new Dictionary<string, LHMSensorResponseDTO>();
            MapearADiccionario(response.Data, resultado);
            return OperationResult<Dictionary<string, LHMSensorResponseDTO>>.Ok(resultado);
        }
                

        private LHMSensorDTO Mapear(LHMSensorModel sensor)
        {
            return new LHMSensorDTO
            {
                Text = sensor.Text,
                Hidden = false, // por defecto visible
                Children = sensor.Children.Select(Mapear).ToList(),
                SensorId = sensor.SensorId
            };
        }

        private void MapearADiccionario(LHMSensorModel sensor, Dictionary<string, LHMSensorResponseDTO> resultado)
        {
            if (!string.IsNullOrEmpty(sensor.SensorId))
            {
                resultado[sensor.SensorId] = 
                new LHMSensorResponseDTO()
                    {
                        Hidden = false,
                        Type = sensor.Type,
                        Text = sensor.Text,
                        Min = sensor.Min,
                        Max = sensor.Max,
                        Value = sensor.Value
                    }; // hidden = false por defecto
            }

            foreach (var hijo in sensor.Children)
            {
                MapearADiccionario(hijo, resultado);
            }
        }
               


    }
}
