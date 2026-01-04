using Microsoft.AspNetCore.Mvc;
using data_mojo.models;
using data_mojo.dtos;
using data_mojo.interfaces;

namespace api_mojo.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganisationController : ControllerBase
    {
        private readonly IRepository<Organisation, OrganisationAddDto, OrganisationUpdateDto> _rep;

        public OrganisationController(IRepository<Organisation, OrganisationAddDto, OrganisationUpdateDto> rep)
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
            var organisation = await _rep.Add(dto);
            return Ok(new { msg = "Organisation Ajoutée !", obj = organisation });
        }

        [HttpPut]
        public async Task<IActionResult> UpadteOrganisation(OrganisationUpdateDto dto)
        {
            var organisation = await _rep.Upadte(dto);
            if (organisation is null)
            {
                return NotFound($"L'organisation \"{dto.Name}\" n'existe pas !");
            }
            return Ok(new { msg = "Organisation Modifiée", obj = organisation });
        }

        [HttpDelete("{id}")]
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