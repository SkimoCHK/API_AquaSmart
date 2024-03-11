using API_AquaSmart.Services;
using Microsoft.AspNetCore.Http;
using API_AquaSmart.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_AquaSmart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {

        private readonly ILogger<AreaController> _logger;
        private readonly AreaServices _areaServices;
        private readonly SensorHumedadServices _sensorservices;
        private readonly ElectroValvulaServices _valvulaServices;




        public AreaController(ILogger<AreaController> logger, AreaServices areaServices, ElectroValvulaServices valvulaServ, SensorHumedadServices sensorServices)
        {
            _logger = logger;
            _areaServices = areaServices;
            _sensorservices = sensorServices;
            _valvulaServices = valvulaServ;
        }

        //[HttpPut("actualizar-status")]
        //public async Task<IActionResult> ActualizarStatus(bool status, string id)
        //{

        //    var area = await _areaServices.GetAreaById(id);
        //    area.valvula.Abierta = status;

        //    await _areaServices.UpdateArea(area);

        //    return Ok("Status Modificado");


        //}

        [HttpPut("historial-riego")]
        public async Task<IActionResult> ActivarRiego([FromBody] ActivarRiegoRequest request)
        {
            try
            {

                var area = await _areaServices.GetAreaById(request.AreaId);


                area.HistorialRiego.Add(new RiegoEvent
                {
                    Fecha = DateTime.Now,
                    Duracion = request.Duracion
                });


                await _areaServices.UpdateArea(area);

                return Ok("Riego activado y registrado correctamente.");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> getAreas()
        {
            var areas = await _areaServices.getAsync();

            foreach(var area in areas)
            {
                var sensor = await  _sensorservices.GetSensorHumedadById(area.IdSensor);
                var valvula = await _valvulaServices.GetValvulaById(area.IdValvula);

                area.SensorHumedad = sensor;
                area.valvula = valvula;
            }

            return Ok(areas);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getAreaById(string id)
        {
            
            var area = await _areaServices.GetAreaById(id);
            var sensor = await _sensorservices.GetSensorHumedadById(area.IdSensor);
            var valvula = await _valvulaServices.GetValvulaById(area.IdValvula);
            area.SensorHumedad = sensor;
            area.valvula = valvula;
            return Ok(area);

        }

        [HttpPost]
        public async Task<IActionResult> CreateArea([FromBody] AreaDTO areaDTO)
        {

            var area = new Area()
            {
                Nombre = areaDTO.Nombre,
                IdSensor = areaDTO.refSensor,
                IdValvula = areaDTO.refValvula
            };
            await _areaServices.InsertArea(area);
            return Created("Created", true);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDriver([FromBody] AreaDTO areaDTO, string ID)
        {

            Area area = new()
            {
                id = ID,
                Nombre = areaDTO.Nombre,
                IdSensor = areaDTO.refSensor,
                IdValvula = areaDTO.refValvula
            };

            await _areaServices.UpdateArea(area);
            return Created("Created", true);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriver(string id)
        {
            await _areaServices.DeleteArea(id);

            return NoContent();
        }


    }
}
