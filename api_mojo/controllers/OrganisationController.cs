using infrastructure_mojo.repositories;
using Microsoft.AspNetCore.Mvc;
using core_mojo.models;
using core_mojo.Dtos;

namespace api_mojo.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganisationController : ControllerBase
    {
        private readonly OrganisationRepository _rep;

        public OrganisationController(OrganisationRepository rep)
        {
            _rep = rep; 
        }

        [HttpGet]
        public async Task<ActionResult> GetAllOrganisations()
        {
            var organisations = await _rep.GetAll();
            return Ok(organisations);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrganisationById(int id)
        {
            var organisation = await _rep.GetById(id);
            if (organisation is null)
            {
                return NotFound($"L'organisation avec l'Id:{id} n'existe pas !");
            }
            return Ok(new { obj = organisation });
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetOrganisationByName(string name)
        {
            var organisation = await _rep.GetByName(name);
            if (organisation is null)
            {
                return NotFound($"L'organisation {name} n'existe pas !");
            }
            return Ok(new { obj = organisation });
        }

        [HttpPost]
        public async Task<IActionResult> AddOrganisation(OrganisationAddDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Organisation organisation = new()
            {
                Name = dto.Name,
                Code = dto.Code,
                Address = dto.Address,
                ContactEmail = dto.ContactEmail,
                IsActif = dto.IsActif
            };

            var org = await _rep.Add(organisation);
            if (org is not null)
            {
                return Ok(new { message = "Organisation Ajoutée !", obj = org });
            }
            return BadRequest($"L'ajout de l'organisation {dto.Name} a échoué.");
        }

        [HttpPut]
        public async Task<IActionResult> UpadteOrganisation(OrganisationUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var organisation = await _rep.GetById(dto.Id);
            if (organisation is null)
            {
                return NotFound($"L'organisation avec l'Id {dto.Id} n'existe pas.");
            }

            organisation.Name = dto.Name;
            organisation.Code = dto.Code;
            organisation.Address = dto.Address;
            organisation.ContactEmail = dto.ContactEmail;
            organisation.IsActif = dto.IsActif;

            var orgUpdate = await _rep.Upadte(organisation);
            if (orgUpdate is not null)
            {
                return Ok(new { message = $"L'organisation {organisation.Name} a été modifiée.", obj = orgUpdate });
            }
            
            return BadRequest($"La modification de l'organisation {dto.Name} a échoué.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrganisation(int id)
        {
            if (await _rep.Delete(id))
            {
                return Ok(new { msg = "L'organisation est supprimée !" });
            }
            return NotFound($"L'organisation avec l'Id:{id} n'existe pas !");
        }
    }
}