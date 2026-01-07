using Microsoft.AspNetCore.Mvc;
using core_mojo.models;
using core_mojo.Dtos;
using infrastructure_mojo.repositories;

namespace api_mojo.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContratController : ControllerBase
    {
        private readonly ContratRepository _rep;
        private readonly VeloRepository _veloRep;
        private readonly UserRepository _userRep;

        public ContratController(ContratRepository rep, VeloRepository veloRep, UserRepository userRep)
        {
            _rep = rep;
            _veloRep = veloRep;
            _userRep = userRep;
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
        public async Task<IActionResult> AddContrat(ContratAddDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var velo = await _veloRep.GetById(dto.VeloId);
            if (velo is null) return BadRequest("Le vélo spécifié n'existe pas.");

            var beneficiaire = await _userRep.GetById2(dto.BeneficiaireId.ToString());
            var userRh = await _userRep.GetById2(dto.UserRhId.ToString());
            
            if (beneficiaire is null || userRh is null) 
                return BadRequest("L'un des utilisateurs (Bénéficiaire ou RH) n'existe pas.");

            Contrat contrat = new Contrat
            {
                DateDebut = dto.DateDebut,
                DateFin = dto.DateFin,
                LoyerMensuelHT = dto.LoyerMensuelHT,
                StatutContrat = dto.StatutContrat,
                VeloId = dto.VeloId,
                BeneficiaireId = dto.BeneficiaireId.ToString(),
                UserRhId = dto.UserRhId.ToString()
            };

            var result = await _rep.Add(contrat);
            return Ok(new { msg = "Contrat Ajouté !", obj = result });
        }

        [HttpPut]
        public async Task<IActionResult> UpadteContrat(ContratUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var contratToUpdate = await _rep.GetById(dto.Id);
            if (contratToUpdate is null)
            {
                return NotFound($"Ce contrat avec l'ID \"{dto.Id}\" n'existe pas !");
            }

            var velo = await _veloRep.GetById(dto.VeloId);
            if (velo is null) return BadRequest("Le vélo spécifié n'existe pas.");

            contratToUpdate.DateDebut = dto.DateDebut;
            contratToUpdate.DateFin = dto.DateFin;
            contratToUpdate.LoyerMensuelHT = dto.LoyerMensuelHT;
            contratToUpdate.StatutContrat = dto.StatutContrat;
            contratToUpdate.VeloId = dto.VeloId;
            contratToUpdate.BeneficiaireId = dto.BeneficiaireId.ToString();
            contratToUpdate.UserRhId = dto.UserRhId.ToString();

            var result = await _rep.Upadte(contratToUpdate);
            return Ok(new { msg = "Contrat Modifié", obj = result });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteContrat(int id)
        {
            if (await _rep.Delete(id))
            {
                return Ok(new { msg = "Le contrat est supprimé !" });
            }
            return NotFound($"Le contrat avec l'Id:{id} n'existe pas !");
        }
    }
}