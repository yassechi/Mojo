using Microsoft.AspNetCore.Mvc;
using api_mojo.data;
using api_mojo.data.models;
using Microsoft.EntityFrameworkCore;


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
        public async Task<IActionResult> AddAmortissements(Amortissement amortissement)
        {
            await db.Amortissements.AddAsync(amortissement);
            db.SaveChanges();
            return Ok(new { message = "Amortissement Ajouté !", obj = amortissement });
        }

        [HttpPut]
        public async Task<IActionResult> UpadteAmortissement(Amortissement amortissement)
        {
            var fdb = await db.Amortissements.SingleOrDefaultAsync(a => a.Id == amortissement.Id);
            if (fdb is null)
            {
                return NotFound($"Cet amortissement \"{amortissement.Id}\" n'existe pas !");
            }
            fdb.DateDebut = amortissement.DateDebut;
            fdb.ValeurInit = amortissement.ValeurInit;
            fdb.DureeMois = amortissement.DureeMois;
            fdb.ValeurResiduelleFinale = amortissement.ValeurResiduelleFinale;
            db.SaveChanges();
            return Ok(new { msg = "Amortissement Modifié", obj = fdb });
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