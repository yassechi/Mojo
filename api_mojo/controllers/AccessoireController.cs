using data_mojo.models;
using data_mojo.dtos;
using Microsoft.AspNetCore.Mvc;
using data_mojo.interfaces;


namespace api_mojo.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccessoireController : ControllerBase
    {
        private readonly IRepository<Accessoire, AccessoireAddDto, AccessoireUpdateDto> rep;

        public AccessoireController(IRepository<Accessoire, AccessoireAddDto, AccessoireUpdateDto> _rep)
        {
            rep = _rep;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAccessoires()
        {
            var accessoires = await rep.GetAll(); 
            return Ok(accessoires);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAccessoireById(int id)
        {
            var accessoire = await rep.GetById(id);
            if (accessoire is null)
            {
                return NotFound($"L'accessoire avec l'Id:{id} n'existe pas !");
            }
            return Ok(new { Accessoire = accessoire });
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetAccessoireByName(string name)
        {
            var accessoire = await rep.GetByName(name);
            if (accessoire is null)
            {
                return NotFound($"L'accessoire {name} n'existe pas !");
            }
            return Ok(new { Accessoire = accessoire });
        }

        [HttpPost]
        public async Task<IActionResult> AddAccesoire(AccessoireAddDto accessoireAddDto)
        {
            var accessoire = await rep.Add(accessoireAddDto);
            return Ok(new { message = "Accesoire Ajouté !", accessoire = accessoire });
        }

        [HttpPut]
        public async Task<IActionResult> UpadteAccessoire(AccessoireUpdateDto accessoireUpdateDto)
        {
            var accessoire = await rep.Upadte(accessoireUpdateDto);
            if (accessoire is null)
            {
                return NotFound($"Cet accessoire n'existe pas !");
            }
            return Ok(new { Message = "Accesoire Modifié", Accessoire = accessoire });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccessoire(int id)
        {
            if (await rep.Delete(id))
            {
                return Ok(new { msg = "L'accessoire est supprimé !" });
            }
            return NotFound($"L'accessoire avec l'Id:{id} n'existe pas !");
        }
    }
}