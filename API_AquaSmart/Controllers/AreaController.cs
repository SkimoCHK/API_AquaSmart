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
            return Ok(areas);
        }

        [HttpGet("obtener-areas")]
        public async Task<IActionResult> getAreasv2()
        {
            var areas = await _areaServices.getAsync();
            return Ok(areas);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> getAreaById(string id)
        {

            var area = await _areaServices.GetAreaById(id);
            return Ok(area);

        }

        [HttpGet("obtener-historial{id}")]
        public async Task<IActionResult> getHistorial(string id)
        {
            var area = await _areaServices.GetAreaById(id);
            return Ok(area.HistorialRiego);
        }

        //[HttpGet("obtener-areas")]
        //public async Task<IActionResult> getAreaStatus()
        //{
        //    var areas = await _areaServices.getAsync();
            
        //    foreach(var area in areas)
        //    {

        //    }

        //}



        [HttpPost]
        public async Task<IActionResult> CreateArea([FromBody] AreaDTO areaDTO)
        {
            var sensor = await _sensorservices.GetSensorHumedadById(areaDTO.refSensor);
            var valvulap = await _valvulaServices.GetValvulaById(areaDTO.refValvula);

            var area = new Area()
            {
                Nombre = areaDTO.Nombre,
                Imagen = areaDTO.Imagen,
                IdSensor = areaDTO.refSensor,
                IdValvula = areaDTO.refValvula,
                SensorHumedad = sensor,
                valvula = valvulap
                
            };
            
            await _areaServices.InsertArea(area);
            return Created("Created", true);

        }

        [HttpPut("{ID}")]
        public async Task<IActionResult> UpdateArea(string ID, [FromBody] AreaDTO areaDTO )
        {
            var sensor = await _sensorservices.GetSensorHumedadById(areaDTO.refSensor);
            var valvulap = await _valvulaServices.GetValvulaById(areaDTO.refValvula);

            Area area = new()
            {
                id = ID,
                Nombre = areaDTO.Nombre,
                Imagen = areaDTO.Imagen,
                IdSensor = areaDTO.refSensor,
                IdValvula = areaDTO.refValvula,
                SensorHumedad = sensor,
                valvula = valvulap
                
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
