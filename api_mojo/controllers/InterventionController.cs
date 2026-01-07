using infrastructure_mojo.repositories;
using Microsoft.AspNetCore.Mvc;
using core_mojo.models;
using core_mojo.Dtos;

namespace api_mojo.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterventionController : ControllerBase
    {
        private readonly InterventionRepository _rep;
        private readonly VeloRepository _veloRep;

        public InterventionController(InterventionRepository rep, VeloRepository veloRep)
        {
            _rep = rep;
            _veloRep = veloRep;
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
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var velo = await _veloRep.GetById(dto.VeloId);
            if (velo is null)
            {
                return BadRequest($"Le vélo avec l'Id {dto.VeloId} n'existe pas.");
            }

            Intervention intervention = new()
            {
                DateIntervention = dto.DateIntervention,
                TypeIntervention = dto.TypeIntervention,
                Description = dto.Description,
                Cout = dto.Cout,
                VeloId = dto.VeloId
            };

            var inter = await _rep.Add(intervention);
            if (inter is not null)
            {
                return Ok(new { message = "Intervention Ajoutée !", obj = inter });
            }
            return BadRequest($"L'ajout de l'intervention {dto.TypeIntervention} a échoué.");
        }

        [HttpPut]
        public async Task<IActionResult> UpadteIntervention(Intervention dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var intervention = await _rep.GetById(dto.Id);
            if (intervention is null)
            {
                return NotFound($"L'intervention avec l'Id {dto.Id} n'existe pas.");
            }

            var velo = await _veloRep.GetById(dto.VeloId);
            if (velo is null)
            {
                return BadRequest($"Le vélo avec l'Id {dto.VeloId} n'existe pas.");
            }

            intervention.DateIntervention = dto.DateIntervention;
            intervention.TypeIntervention = dto.TypeIntervention;
            intervention.Description = dto.Description;
            intervention.Cout = dto.Cout;
            intervention.VeloId = dto.VeloId;

            var intervUpdate = await _rep.Upadte(intervention);
            if (intervUpdate is not null)
            {
                return Ok(new { message = "Intervention modifiée avec succès.", obj = intervUpdate });
            }
            
            return BadRequest($"La modification de l'intervention a échoué.");
        }

        [HttpDelete("{id:int}")]
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