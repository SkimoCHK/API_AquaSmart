using API_AquaSmart.Models;
using API_AquaSmart.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace API_AquaSmart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioRiegoController : ControllerBase
    {
        private readonly HorarioRiegoServices _services;

        public HorarioRiegoController(HorarioRiegoServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetHorarios()
        {
            var areas = await _services.GetHorarios();
            return Ok(areas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHorarioById(string id)
        {
            return Ok(await _services.GetHorarioById(id));
        }

        [HttpPost]
        public async Task<IActionResult> InsertHorario([FromBody] HorarioRiego horario)
        {
            if(horario == null)
            {
                Console.WriteLine("No puede ser nulo el modelo");
                return BadRequest();
            }
            else if(horario.HoraInicio == DateTime.MinValue && horario.HoraFin == DateTime.MinValue)
            {
                Console.WriteLine("Debes de rellenar todos los campos");
                ModelState.AddModelError("Modelo", "Los campos no deben estar vacios");
                return BadRequest();
            }
            else
            {
                await  _services.InsertHorario(horario);
                return Created("Created", true);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHorario([FromBody] HorarioRiego horario, string id)
        {
            if (horario == null)
            {
                Console.WriteLine("No puede ser nulo el modelo");
                return BadRequest();
            }
            else if (horario.HoraInicio == DateTime.MinValue && horario.HoraFin == DateTime.MinValue)
            {
                Console.WriteLine("Debes de rellenar todos los campos");
                ModelState.AddModelError("Modelo", "Los campos no deben estar vacios");
                return BadRequest();
            }
            else
            {
                horario.Id = id;
                await _services.UpdateHorario(horario);
                return Created("Created", true);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHorario(string id)
        {
            await _services.DeleteHorario(id);
            return NoContent();
        }

    }
}
