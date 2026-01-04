using data_mojo;
using data_mojo.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_mojo.dtos;

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
        public async Task<IActionResult> AddContrat(ContratAddDto contratAddDto)
        {
            Contrat contrat = new()
            {
                DateDebut = contratAddDto.DateDebut,
                DateFin = contratAddDto.DateFin,
                LoyerMensuelHT = contratAddDto.LoyerMensuelHT,
                StatutContrat = contratAddDto.StatutContrat,
                VeloId = contratAddDto.VeloId,
                BeneficiaireId = contratAddDto.BeneficiaireId,
                UserRhId = contratAddDto.UserRhId
            };
            await db.Contrats.AddAsync(contrat);
            db.SaveChanges();
            return Ok(new { msg = "Contrat Ajouté !", obj = contrat });
        }

        [HttpPut]
        public async Task<IActionResult> UpadteContrat(ContratUpdateDto contratUpdateDto)
        {
            var contrat = await db.Contrats.SingleOrDefaultAsync(a => a.Id == contratUpdateDto.Id);
            if (contrat is null)
            {
                return NotFound($"Ce contrat \"{contratUpdateDto.Id}\" n'existe pas !");
            }
            contrat.DateDebut = contratUpdateDto.DateDebut;
            contrat.DateFin = contratUpdateDto.DateFin;
            contrat.LoyerMensuelHT = contratUpdateDto.LoyerMensuelHT;
            contrat.StatutContrat = contratUpdateDto.StatutContrat;
            db.SaveChanges();
            return Ok(new { msg = "Contrat Modifié", obj = contrat });
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