using api_mojo.data;
using api_mojo.data.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_mojo.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccessoireController : ControllerBase
    {
        private readonly AppDbContext db;
        public AccessoireController(AppDbContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAccessoires()
        {
            var acc = await db.Accessoires.ToListAsync();
            return Ok(acc);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetAccessoireById(int id)
        {
            var acc = await db.Accessoires.SingleOrDefaultAsync(a => a.Id == id);
            if (acc is null)
            {
                return NotFound($"L'accessoire avec l'Id:{id} n'existe pas !");
            }
            return Ok(new { Accessoire = acc });
        }

        [HttpGet("name")]
        public async Task<IActionResult> GetAccessoireByName(string name)
        {
            var acc = await db.Accessoires.SingleOrDefaultAsync(a => a.Name == name);
            if (acc is null)
            {
                return NotFound($"L'accessoire {name} n'existe pas !");
            }
            return Ok(new { Accessoire = acc });
        }

        [HttpPost]
        public async Task<IActionResult> AddAccesoire(Accessoire acc)
        {
            await db.Accessoires.AddAsync(acc);
            db.SaveChanges();
            return Ok(new { message = "Accesoire Ajouté !", accessoire = acc });
        }

        [HttpPut]
        public async Task<IActionResult> UpadteAccessoire(Accessoire acc)
        {
            var fdb = await db.Accessoires.SingleOrDefaultAsync(a => a.Id == acc.Id);
            if (fdb is null)
            {
                return NotFound($"Cet accessoire \"{acc.Name}\" n'existe pas !");
            }
            fdb.Name = acc.Name;
            fdb.Price = acc.Price;
            fdb.Stock = acc.Stock;
            db.SaveChanges();
            return Ok(new { Message = "Accesoire Modifié", Accessoire = fdb });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAccessoire(int id)
        {
            var acc = await db.Accessoires.SingleOrDefaultAsync(a => a.Id == id);
            if (acc is null)
            {
                return NotFound($"L'accessoire avec l'Id:{id} n'exite pas !");
            }
            db.Remove(acc);
            db.SaveChanges();
            return Ok(new { msg = "L'accessoire est suprimé !" });
        }

    }
}