using API_AquaSmart.Models;
using API_AquaSmart.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_AquaSmart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorHumedadController : ControllerBase
    {
        private readonly ILogger<SensorHumedadController> _logger;
        private readonly SensorHumedadServices _sensorHumedadServices;

        public SensorHumedadController(ILogger<SensorHumedadController> logger, SensorHumedadServices sensorHumedadServices)
        {
            _logger = logger;
            _sensorHumedadServices = sensorHumedadServices;
        }

        [HttpPost("actualizar-humedad")]
        public async Task<IActionResult> ActualizarHumedad([FromBody] DatosHumedadDTO datos)
        {
            if (datos == null)
                return BadRequest();

            var sensorHumedad = await _sensorHumedadServices.GetSensorHumedadById(datos.IdSensor);

            if (sensorHumedad == null)
                return NotFound();


            sensorHumedad.ValorActualHumedad = datos.ValorActualHumedad;

            await _sensorHumedadServices.UpdateSensorHumedad(sensorHumedad);

            return Ok();
        }
    


        [HttpGet]
        public async Task<IActionResult> GetSensoresHumedad()
        {
            var sensoresHumedad = await _sensorHumedadServices.GetSensoresHumedadAsync();
            return Ok(sensoresHumedad);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSensorHumedadById(string id)
        {
            return Ok(await _sensorHumedadServices.GetSensorHumedadById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateSensorHumedad([FromBody] SensorHumedad sensorHumedad)
        {
            if (sensorHumedad == null)
                return BadRequest();

            await _sensorHumedadServices.InsertSensorHumedad(sensorHumedad);
            return Created("Created", true);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSensorHumedad([FromBody] SensorHumedad sensorHumedad, string id)
        {
            if (sensorHumedad == null)
                return BadRequest();

            sensorHumedad.Id = id;

            await _sensorHumedadServices.UpdateSensorHumedad(sensorHumedad);
            return Created("Created", true);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSensorHumedad(string id)
        {
            await _sensorHumedadServices.DeleteSensorHumedad(id);

            return NoContent();
        }
    }
}
