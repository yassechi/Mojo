using Microsoft.AspNetCore.Mvc;
using core_mojo.models;
using infrastructure_mojo.repositories;
using core_mojo.Dtos;

namespace api_mojo.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeloController : ControllerBase
    {
        private readonly VeloRepository _rep;

        public VeloController(VeloRepository rep)
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
        public async Task<IActionResult> AddVelo(VeloAddDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Velo velo = new Velo
            {
                NumeroSerie = dto.NumeroSerie,
                Marque = dto.Marque,
                Modele = dto.Modele,
                PrixAchat = dto.PrixAchat,
                Status = dto.Status
            };

            var result = await _rep.Add(velo);
            
            if (result != null)
            {
                return Ok(new { msg = "Velo Ajouté !", obj = result });
            }
            return BadRequest("L'ajout du vélo a échoué.");
        }

        [HttpPut]
        public async Task<IActionResult> UpadteVelo(VeloUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var veloToUpdate = await _rep.GetById(dto.Id);
            if (veloToUpdate is null)
            {
                return NotFound($"Ce velo avec l'Id \"{dto.Id}\" n'existe pas !");
            }

            veloToUpdate.NumeroSerie = dto.NumeroSerie;
            veloToUpdate.Marque = dto.Marque;
            veloToUpdate.Modele = dto.Modele;
            veloToUpdate.PrixAchat = dto.PrixAchat;
            veloToUpdate.Status = dto.Status;

            var result = await _rep.Upadte(veloToUpdate);
            
            if (result != null)
            {
                return Ok(new { msg = "Velo Modifié", obj = result });
            }
            return BadRequest("La modification a échoué.");
        }

        [HttpDelete("{id:int}")]
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