using api_mojo.data;
using api_mojo.data.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> AddVelo(Velo velo)
        {
            await db.Velos.AddAsync(velo);
            db.SaveChanges();
            return Ok(new { msg = "Velo Ajouté !", obj = velo });
        }

        [HttpPut]
        public async Task<IActionResult> UpadteVelo(Velo velo)
        {
            var fdb = await db.Velos.SingleOrDefaultAsync(a => a.Id == velo.Id);
            if (fdb is null)
            {
                return NotFound($"Ce velo \"{velo.Id}\" n'existe pas !");
            }
            fdb.NumeroSerie = velo.NumeroSerie;
            fdb.Marque = velo.Marque;
            fdb.Modele = velo.Modele;
            fdb.PrixAchat = velo.PrixAchat;
            fdb.Status = velo.Status;
            db.SaveChanges();
            return Ok(new { msg = "Velo Modifié", obj = fdb });
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