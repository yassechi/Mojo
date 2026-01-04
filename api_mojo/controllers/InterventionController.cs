using Microsoft.AspNetCore.Mvc;
using data_mojo.models;
using data_mojo.dtos;
using data_mojo.interfaces;

namespace api_mojo.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterventionController : ControllerBase
    {
        private readonly IRepository<Intervention, InterventionAddDto, InterventionUpdateDto> _rep;

        public InterventionController(IRepository<Intervention, InterventionAddDto, InterventionUpdateDto> rep)
        {
            _rep = rep;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllInterventions()
        {
            var interventions = await _rep.GetAll();
            return Ok(interventions);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetInterventionById(int id)
        {
            var intervention = await _rep.GetById(id);
            if (intervention is null)
            {
                return NotFound($"L'intervention avec l'Id:{id} n'existe pas !");
            }
            return Ok(new { obj = intervention });
        }

        [HttpPost]
        public async Task<IActionResult> AddIntervention(InterventionAddDto dto)
        {
            var intervention = await _rep.Add(dto);
            return Ok(new { message = "Intervention Ajoutée !", obj = intervention });
        }

        [HttpPut]
        public async Task<IActionResult> UpadteIntervention(InterventionUpdateDto dto)
        {
            var intervention = await _rep.Upadte(dto);
            if (intervention is null)
            {
                return NotFound($"L'intervention avec l'Id \"{dto.Id}\" n'existe pas !");
            }
            return Ok(new { msg = "Intervention Modifiée", obj = intervention });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIntervention(int id)
        {
            if (await _rep.Delete(id))
            {
                return Ok(new { msg = "L'intervention est supprimée !" });
            }
            return NotFound($"L'intervention avec l'Id:{id} n'existe pas !");
        }
    }
}