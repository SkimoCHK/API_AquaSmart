﻿using API_AquaSmart.Services;
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

        
        [HttpPost("activar-riego")]
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


        public AreaController(ILogger<AreaController> logger, AreaServices areaServices)
        {
            _logger = logger;
            _areaServices = areaServices;
        }
        [HttpGet]
        public async Task<IActionResult> getAreas()
        {
            var areas = await _areaServices.getAsync();
            return Ok(areas);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getAreaById(string id)
        {
            return Ok(await _areaServices.GetAreaById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateArea([FromBody] Area area)
        {
            if (area == null)
                return BadRequest();
            if (area.Nombre == string.Empty)
                ModelState.AddModelError("Nombre", "El driver no debe estar vacio");

            await _areaServices.InsertArea(area);
            return Created("Created", true);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDriver([FromBody] Area area, string id)
        {
            if (area == null)
                return BadRequest();
            if (area.Nombre == string.Empty)
                ModelState.AddModelError("Name", "El área no debe estar vacio");

            area.id = id;

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
