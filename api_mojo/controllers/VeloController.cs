using data_mojo;
using data_mojo.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_mojo.dtos;


namespace api_mojo.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeloController : ControllerBase
    {
        private readonly AppDbContext db;
        public VeloController(AppDbContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllVelo()
        {
            var velo = await db.Velos.ToListAsync();
            return Ok(velo);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetVeloById(int id)
        {
            var velo = await db.Velos.SingleOrDefaultAsync(a => a.Id == id);
            if (velo is null)
            {
                return NotFound($"Le velo avec l'Id:{id} n'existe pas !");
            }
            return Ok(new { obj = velo });
        }

        [HttpPost]
        public async Task<IActionResult> AddVelo(VeloAddDto veloAddDto)
        {
            Velo velo = new()
            {
                NumeroSerie = veloAddDto.NumeroSerie,
                Marque = veloAddDto.Marque,
                Modele = veloAddDto.Modele,
                PrixAchat = veloAddDto.PrixAchat,
                Status = veloAddDto.Status
            };
            await db.Velos.AddAsync(velo);
            db.SaveChanges();
            return Ok(new { msg = "Velo Ajouté !", obj = velo });
        }

        [HttpPut]
        public async Task<IActionResult> UpadteVelo(VeloUpdateDto veloUpdateDto)
        {
            var velo = await db.Velos.SingleOrDefaultAsync(a => a.Id == veloUpdateDto.Id);
            if (velo is null)
            {
                return NotFound($"Ce velo \"{veloUpdateDto.Id}\" n'existe pas !");
            }
            velo.NumeroSerie = veloUpdateDto.NumeroSerie;
            velo.Marque = veloUpdateDto.Marque;
            velo.Modele = veloUpdateDto.Modele;
            velo.PrixAchat = veloUpdateDto.PrixAchat;
            velo.Status = veloUpdateDto.Status;
            db.SaveChanges();
            return Ok(new { msg = "Velo Modifié", obj = velo });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVelo(int id)
        {
            var velo = await db.Velos.SingleOrDefaultAsync(a => a.Id == id);
            if (velo is null)
            {
                return NotFound($"Le velo avec l'Id:{id} n'exite pas !");
            }
            db.Remove(velo);
            db.SaveChanges();
            return Ok(new { msg = "Le velo est suprimé !" });
        }

    }
}