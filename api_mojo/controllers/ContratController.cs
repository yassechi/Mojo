using api_mojo.data;
using api_mojo.data.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_mojo.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContratController : ControllerBase
    {
        private readonly AppDbContext db;
        public ContratController(AppDbContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllContrat()
        {
            var contrat = await db.Contrats.ToListAsync();
            return Ok(contrat);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetContratById(int id)
        {
            var contrat = await db.Contrats.SingleOrDefaultAsync(a => a.Id == id);
            if (contrat is null)
            {
                return NotFound($"Le contrat avec l'Id:{id} n'existe pas !");
            }
            return Ok(new { obj = contrat });
        }

        [HttpPost]
        public async Task<IActionResult> AddContrat(Contrat contrat)
        {
            await db.Contrats.AddAsync(contrat);
            db.SaveChanges();
            return Ok(new { msg = "Contrat Ajouté !", obj = contrat });
        }

        [HttpPut]
        public async Task<IActionResult> UpadteContrat(Contrat contrat)
        {
            var fdb = await db.Contrats.SingleOrDefaultAsync(a => a.Id == contrat.Id);
            if (fdb is null)
            {
                return NotFound($"Ce contrat \"{contrat.Id}\" n'existe pas !");
            }
            fdb.DateDebut = contrat.DateDebut;
            fdb.DateFin = contrat.DateFin;
            fdb.LoyerMensuelHT = contrat.LoyerMensuelHT;
            fdb.StatutContrat = contrat.StatutContrat;
            db.SaveChanges();
            return Ok(new { msg = "Contrat Modifié", obj = fdb });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContrat(int id)
        {
            var contrat = await db.Contrats.SingleOrDefaultAsync(a => a.Id == id);
            if (contrat is null)
            {
                return NotFound($"Le contrat avec l'Id:{id} n'exite pas !");
            }
            db.Remove(contrat);
            db.SaveChanges();
            return Ok(new { msg = "Le contrat est suprimé !" });
        }
    }
}