using Domain.Model.Entities;
using Domain.Model.Interface;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infra.ExternalServices.Api
{
    public class LibreHardwareMonitorApi : ILibreHardwareMonitorApi
    {
        private readonly HttpClient _httpClient;

        public LibreHardwareMonitorApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OperationResult<LHMSensorModel?>> ObtenerSensoresFull()
        {
            try
            {
                string url = "http://192.168.88.3:8085/data.json";
                var response = await _httpClient.GetStringAsync(url);
                var rta = JsonSerializer.Deserialize<LHMSensorModel>(response);
                return OperationResult<LHMSensorModel>.Ok(rta);               


            }
            catch (HttpRequestException ex)
            {
                return OperationResult<LHMSensorModel>.Fail("Error en la petición obtenerSensoresFull " + ex.Message);
            }
        }

    }
}
