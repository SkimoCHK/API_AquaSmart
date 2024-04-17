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


        [HttpPut("cambiar-modo/{id}")]
        public async Task<IActionResult> CambiarModoFuncionamiento(string id, [FromBody] bool modo)
        {
            try
            {
                var area = await _areaServices.GetAreaById(id);
                if (area == null)
                {
                    return NotFound();
                }

                area.Modo = modo;
                await _areaServices.UpdateArea(area);

                return Ok($"El modo de funcionamiento del área {id} se ha actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("obtener-modo/{id}")]
        public async Task<IActionResult> ObtenerModo(string id)
        {
            var area = await _areaServices.GetAreaById(id);
            if (area == null)
            {
                return NotFound(); // o algún otro manejo de error apropiado
            }
            return Ok(area.Modo);
        }



        //AQUI ESTE LO MANDARIA EL WEMOS! cuando se abre la electrovalvula se activa el riego, va cambiar el status a true
        [HttpPut("actualizar-status")]
        public async Task<IActionResult> CambiarStatus(AreaUpdateDTO areaUpdate)
        {
            var area = await _areaServices.GetAreaById(areaUpdate.id);
            area.valvula.Abierta = areaUpdate.Status;
            await _areaServices.UpdateArea(area);
            return Created("Status Actualizado", true);

        }

        // AQUI OBTIENE EL STATUS!!!
        [HttpGet("obtener-status{id}")]
        public async Task<IActionResult>ObtenerStatus(string id)
        {
            var area = await _areaServices.GetAreaById(id);
            return Ok(area.valvula.Abierta);
        }

        //AQUI ESTE ENDPOINT LO VA MANEJAR EL WEMOS, cuando la electrovalvula este abierta, mandara una petcion aqui para registrar el historial
        [HttpPut("historial-riego{id}")]
        public async Task<IActionResult> ActivarRiego(string id)
        {
            try
            {
                var area = await _areaServices.GetAreaById(id);

                area.HistorialRiego.Add(new RiegoEvent
                {
                    Fecha = DateTime.Now,
                });

                await _areaServices.UpdateArea(area);

                return Ok("Riego registrado correctamente.");
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
                Modo = false,
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
