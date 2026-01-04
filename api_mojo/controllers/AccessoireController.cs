using data_mojo;
using data_mojo.models;
using api_mojo.dtos;
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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAccessoireById(int id)
        {
            var acc = await db.Accessoires.SingleOrDefaultAsync(a => a.Id == id);
            if (acc is null)
            {
                return NotFound($"L'accessoire avec l'Id:{id} n'existe pas !");
            }
            return Ok(new { Accessoire = acc });
        }

        [HttpGet("name/{name}")]
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
        public async Task<IActionResult> AddAccesoire(AccessoireAddDto accessoireAddDto)
        {
            Accessoire accessoire = new()
            {
                Name = accessoireAddDto.Name,
                Price = accessoireAddDto.Price,
                Stock = accessoireAddDto.Stock
            };

            await db.Accessoires.AddAsync(accessoire);
            db.SaveChanges();
            return Ok(new { message = "Accesoire Ajouté !", accessoire = accessoireAddDto });
        }

        [HttpPut]
        public async Task<IActionResult> UpadteAccessoire(AccessoireUpdateDto accessoireUpdateDto)
        {
            var accessoire = await db.Accessoires.SingleOrDefaultAsync(a => a.Id == accessoireUpdateDto.Id);
            if (accessoire is null)
            {
                return NotFound($"Cet accessoire \"{accessoireUpdateDto.Name}\" n'existe pas !");
            }
            accessoire.Name = accessoireUpdateDto.Name;
            accessoire.Price = accessoireUpdateDto.Price;
            accessoire.Stock = accessoireUpdateDto.Stock;
            db.SaveChanges();
            return Ok(new { Message = "Accesoire Modifié", Accessoire = accessoire });
        }

        [HttpDelete("{id}")]
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