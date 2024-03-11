using API_AquaSmart.Models;
using API_AquaSmart.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_AquaSmart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectroValvulaController : ControllerBase
    {
        private readonly ElectroValvulaServices _services;

        public ElectroValvulaController(ElectroValvulaServices services)
        {
            _services = services;
        }

        [HttpPut("actualizar-status")]
        public async Task<IActionResult> CambiarStatus(ValvulaRequest valvulaxd)
        {
            var valvula = await _services.GetValvulaById(valvulaxd.id);
            valvula.Abierta = valvulaxd.Status;
            await _services.UpdateValvula(valvula);
            return Created("Status Actualizado", true);

           
        }

        [HttpGet]
        public async Task<IActionResult> GetValvulas()
        {
            var valvulas = await _services.GetValvulas();
            return Ok(valvulas);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValvulaById(string id)
        {
            var valvula = await _services.GetValvulaById(id);
            return Ok(valvula);
        }

        [HttpPost]
        public async Task<IActionResult> InsertValvula(ElectroValvulaDTO valvulaDTO)
        {
            var electrovalvula = new ElectroValvula()
            {
                Nombre = valvulaDTO.Nombre,
                Abierta = false
            };

            await _services.InsertValvula(electrovalvula);
            return Created("Created", true);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateValvula(ElectroValvulaDTO valvulaDTO, string id)
        {
            
            var electroValvula = new ElectroValvula()
            {
                Id = id,
                Nombre = valvulaDTO.Nombre,
                Abierta = valvulaDTO.Abierta,
            };
            await _services.UpdateValvula(electroValvula);
            return Created("Created", true);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteValvula(string id)
        {
            await _services.DeleteValvula(id);
            return NoContent();
        }


    }
}
