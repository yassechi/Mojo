using Microsoft.AspNetCore.Mvc;
using api_mojo.data;
using api_mojo.data.models;
using Microsoft.EntityFrameworkCore;
using api_mojo.dtos;


namespace api_mojo.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AmortissementController : ControllerBase
    {
        private readonly AppDbContext db;
        public AmortissementController(AppDbContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAmortissements()
        {
            var amortissement = await db.Amortissements.ToListAsync();
            return Ok(amortissement);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAmortissementById(int id)
        {
            var amortissement = await db.Amortissements.SingleOrDefaultAsync(a => a.Id == id);
            if (amortissement is null)
            {
                return NotFound($"L'amortissement avec l'Id:{id} n'existe pas !");
            }
            return Ok(new { obj = amortissement });
        }

        [HttpPost]
        public async Task<IActionResult> AddAmortissements(AmortissmentAddDto amortDto)
        {
            Amortissement amortissement = new()
            {
                DateDebut = amortDto.DateDebut,
                ValeurInit = amortDto.ValeurInit,
                DureeMois = amortDto.DureeMois,
                ValeurResiduelleFinale = amortDto.ValeurResiduelleFinale,
                VeloId = amortDto.VeloId
            };
            await db.Amortissements.AddAsync(amortissement);
            db.SaveChanges();
            return Ok(new { message = "Amortissement Ajouté !", obj = amortissement });
        }

        [HttpPut]
        public async Task<IActionResult> UpadteAmortissement(AmortissmentUpdateDto amortissementDto)
        {
            var amortissement = await db.Amortissements.SingleOrDefaultAsync(a => a.Id == amortissementDto.Id);
            if (amortissement is null)
            {
                return NotFound($"Cet amortissement \"{amortissementDto.Id}\" n'existe pas !");
            }
            amortissement.DateDebut = amortissementDto.DateDebut;
            amortissement.ValeurInit = amortissementDto.ValeurInit;
            amortissement.DureeMois = amortissementDto.DureeMois;
            amortissement.ValeurResiduelleFinale = amortissementDto.ValeurResiduelleFinale;
            db.SaveChanges();
            return Ok(new { msg = "Amortissement Modifié", obj = amortissement });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmortissement(int id)
        {
            var amortissement = await db.Amortissements.SingleOrDefaultAsync(a => a.Id == id);
            if (amortissement is null)
            {
                return NotFound($"L'amortissement avec l'Id:{id} n'exite pas !");
            }
            db.Remove(amortissement);
            db.SaveChanges();
            return Ok(new { msg = "L'amortissement est suprimé !" });
        }
    }
}