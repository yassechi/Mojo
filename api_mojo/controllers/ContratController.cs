using Microsoft.AspNetCore.Mvc;
using data_mojo.models;
using data_mojo.dtos;
using data_mojo.interfaces;

namespace api_mojo.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContratController : ControllerBase
    {
        private readonly IRepository<Contrat, ContratAddDto, ContratUpdateDto> _rep;

        public ContratController(IRepository<Contrat, ContratAddDto, ContratUpdateDto> rep)
        {
            _rep = rep;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllContrat()
        {
            var contrats = await _rep.GetAll();
            return Ok(contrats);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetContratById(int id)
        {
            var contrat = await _rep.GetById(id);
            if (contrat is null)
            {
                return NotFound($"Le contrat avec l'Id:{id} n'existe pas !");
            }
            return Ok(new { obj = contrat });
        }

        [HttpPost]
        public async Task<IActionResult> AddContrat(ContratAddDto contratAddDto)
        {
            var contrat = await _rep.Add(contratAddDto);
            return Ok(new { msg = "Contrat Ajouté !", obj = contrat });
        }

        [HttpPut]
        public async Task<IActionResult> UpadteContrat(ContratUpdateDto contratUpdateDto)
        {
            // Note: On utilise 'Upadte' pour correspondre à ton interface
            var contrat = await _rep.Upadte(contratUpdateDto);
            if (contrat is null)
            {
                return NotFound($"Ce contrat avec l'ID \"{contratUpdateDto.Id}\" n'existe pas !");
            }
            return Ok(new { msg = "Contrat Modifié", obj = contrat });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContrat(int id)
        {
            var success = await _rep.Delete(id);
            if (success)
            {
                return Ok(new { msg = "Le contrat est supprimé !" });
            }
            return NotFound($"Le contrat avec l'Id:{id} n'existe pas !");
        }
    }
}