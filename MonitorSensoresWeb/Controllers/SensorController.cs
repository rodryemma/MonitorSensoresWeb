using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MonitorSensoresWeb.Controllers
{
    public class SensorController : Controller
    {
        private readonly ILibreHardwareMonitorService _libreHardwareMonitorService;
        private readonly ISensoresService _sensoresService;

        public SensorController(ILibreHardwareMonitorService libreHardwareMonitorApi, ISensoresService sensoresService)
        {
            _libreHardwareMonitorService = libreHardwareMonitorApi;
            _sensoresService = sensoresService;
        }

        [HttpGet("sensor/check")]
        public ActionResult SensorCheck()
        {
            return View();
        }

        [HttpGet("sensor/json")]
        public async Task<IActionResult> GetSensores()
        {
            var sensores = await _libreHardwareMonitorService.ObtenerSensoresFull();
            if (!sensores.Success) { return BadRequest(sensores.Message); }
            
            return Json(sensores.Data);

        }

        [HttpGet("sensor/arbol/json")]
        public async Task<IActionResult> GetArbolSensores()
        {
            //var sensoresArbol = await _libreHardwareMonitorService.ObtenerSensoresArbolFull();
            //if (!sensoresArbol.Success) { return BadRequest(sensoresArbol.Message); }

            //var sensoresEstados = await _sensoresService.BuscarSensorFullAsync();
            //if (!sensoresEstados.Success) { return BadRequest(sensoresEstados.Message); }

            var sensoresEstados = await _sensoresService.EstadoActualSensores();
            if (!sensoresEstados.Success) { return BadRequest(sensoresEstados.Message); }

            return Json(sensoresEstados.Data);

        }

        [HttpGet("sensor/estados/json")]
        public async Task<IActionResult> GetEstadoSensores()
        {
            var sensores = await _libreHardwareMonitorService.ObtenerEstadosAsync();
            if (!sensores.Success) { return BadRequest(sensores.Message); }

            return Json(sensores.Data);

        }

        [HttpPost("sensor/actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] LHMSensorDTO data)
        {
            var response = data;
            var sensores = await _sensoresService.GestionarEstadosSensores(data);
            if (!sensores.Success) { return BadRequest(sensores.Message); }

            return Ok();
        }
    }
}
