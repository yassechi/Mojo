using Microsoft.AspNetCore.Mvc;
using core_mojo.models;
using core_mojo.interfaces;
using core_mojo.interfaces;
using core_mojo.Dtos;

namespace api_mojo.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeloController : ControllerBase
    {
        private readonly IRepository<Velo, VeloAddDto, VeloUpdateDto> _rep;

        public VeloController(IRepository<Velo, VeloAddDto, VeloUpdateDto> rep)
        {
            _rep = rep;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllVelo()
        {
            var velos = await _rep.GetAll();
            return Ok(velos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetVeloById(int id)
        {
            var velo = await _rep.GetById(id);
            if (velo is null)
            {
                return NotFound($"Le velo avec l'Id:{id} n'existe pas !");
            }
            return Ok(new { obj = velo });
        }

        [HttpPost]
        public async Task<IActionResult> AddVelo(VeloAddDto veloAddDto)
        {
            var velo = await _rep.Add(veloAddDto);
            return Ok(new { msg = "Velo Ajouté !", obj = velo });
        }

        [HttpPut]
        public async Task<IActionResult> UpadteVelo(VeloUpdateDto veloUpdateDto)
        {
            var velo = await _rep.Upadte(veloUpdateDto);
            if (velo is null)
            {
                return NotFound($"Ce velo avec l'Id \"{veloUpdateDto.Id}\" n'existe pas !");
            }
            return Ok(new { msg = "Velo Modifié", obj = velo });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVelo(int id)
        {
            if (await _rep.Delete(id))
            {
                return Ok(new { msg = "Le velo est supprimé !" });
            }
            return NotFound($"Le velo avec l'Id:{id} n'existe pas !");
        }
    }
}