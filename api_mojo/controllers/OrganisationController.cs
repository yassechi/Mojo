using api_mojo.data;
using api_mojo.data.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_mojo.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganisationController : ControllerBase
    {
        private readonly AppDbContext db;
        public OrganisationController(AppDbContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllOrganisations()
        {
            var organisation = await db.Organisations.ToListAsync();
            return Ok(organisation);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrganisationById(int id)
        {
            var organisation = await db.Organisations.SingleOrDefaultAsync(a => a.Id == id);
            if (organisation is null)
            {
                return NotFound($"L'organisation avec l'Id:{id} n'existe pas !");
            }
            return Ok(new { obj = organisation });
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetOrganisationByName(string name)
        {
            var organisation = await db.Organisations.SingleOrDefaultAsync(a => a.Name == name);
            if (organisation is null)
            {
                return NotFound($"L'organisation {name} n'existe pas !");
            }
            return Ok(new { obj = organisation });
        }

        [HttpPost]
        public async Task<IActionResult> AddOrganisation(Organisation organisation)
        {
            await db.Organisations.AddAsync(organisation);
            db.SaveChanges();
            return Ok(new { msg = "Organisation Ajouté !", obj = organisation });
        }

        [HttpPut]
        public async Task<IActionResult> UpadteOrganisation(Organisation organisation)
        {
            var fdb = await db.Organisations.SingleOrDefaultAsync(a => a.Id == organisation.Id);
            if (fdb is null)
            {
                return NotFound($"Cet organisation \"{organisation.Name}\" n'existe pas !");
            }
            fdb.Name = organisation.Name;
            fdb.Code = organisation.Code;
            fdb.Address = organisation.Address;
            fdb.ContactEmail = organisation.ContactEmail;
            fdb.IsActif = organisation.IsActif;
            db.SaveChanges();
            return Ok(new { msg = "Organisation Modifié", obj = organisation });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganisation(int id)
        {
            var organisation = await db.Organisations.SingleOrDefaultAsync(a => a.Id == id);
            if (organisation is null)
            {
                return NotFound($"L'organisation avec l'Id:{id} n'exite pas !");
            }
            db.Remove(organisation);
            db.SaveChanges();
            return Ok(new { msg = "L'organisation est suprimé !" });
        }
        
    }
}