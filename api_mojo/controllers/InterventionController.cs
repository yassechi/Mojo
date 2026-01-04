using api_mojo.data;
using api_mojo.data.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_mojo.dtos;

namespace api_mojo.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterventionController : ControllerBase
    {
        private readonly AppDbContext db;
        public InterventionController(AppDbContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllInterventions()
        {
            var intervention = await db.Interventions.ToListAsync();
            return Ok(intervention);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetInterventionById(int id)
        {
            var intervention = await db.Interventions.SingleOrDefaultAsync(a => a.Id == id);
            if (intervention is null)
            {
                return NotFound($"L'intervention avec l'Id:{id} n'existe pas !");
            }
            return Ok(new { obj = intervention });
        }

        [HttpPost]
        public async Task<IActionResult> AddInterventions(InterventionAddDto interventionAddDto)
        {
            Intervention intervention = new()
            {
                DateIntervention = interventionAddDto.DateIntervention,
                TypeIntervention = interventionAddDto.TypeIntervention,
                Description = interventionAddDto.Description,
                Cout = interventionAddDto.Cout,
                VeloId = interventionAddDto.VeloId
            };
            await db.Interventions.AddAsync(intervention);
            db.SaveChanges();
            return Ok(new { message = "Accesoire Ajouté !", obj = intervention });
        }

        [HttpPut]
        public async Task<IActionResult> UpadteIntervention(InterventionUpdateDto interventionUpdateDto)
        {
            var fdb = await db.Interventions.SingleOrDefaultAsync(a => a.Id == interventionUpdateDto.Id);
            if (fdb is null)
            {
                return NotFound($"Cet Intervention \"{interventionUpdateDto.Id}\" n'existe pas !");
            }
            fdb.DateIntervention = interventionUpdateDto.DateIntervention;
            fdb.TypeIntervention = interventionUpdateDto.TypeIntervention;
            fdb.Description = interventionUpdateDto.Description;
            fdb.Cout = interventionUpdateDto.Cout;
            db.SaveChanges();
            return Ok(new { msg = "Intervention Modifié", obj = fdb });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIntervention(int id)
        {
            var intervention = await db.Interventions.SingleOrDefaultAsync(a => a.Id == id);
            if (intervention is null)
            {
                return NotFound($"L'intervention avec l'Id:{id} n'exite pas !");
            }
            db.Remove(intervention);
            db.SaveChanges();
            return Ok(new { msg = "L'intervention est suprimé !" });
        }

    }
}