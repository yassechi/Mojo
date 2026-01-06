using Microsoft.AspNetCore.Mvc;
using core_mojo.models;
using core_mojo.interfaces;
using core_mojo.interfaces;
using core_mojo.Dtos;

namespace api_mojo.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AmortissementController : ControllerBase
    {
        private readonly IRepository<Amortissement, AmortissmentAddDto, AmortissmentUpdateDto> _rep;

        public AmortissementController(IRepository<Amortissement, AmortissmentAddDto, AmortissmentUpdateDto> rep)
        {
            _rep = rep;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAmortissements()
        {
            var amortissements = await _rep.GetAll();
            return Ok(amortissements);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAmortissementById(int id)
        {
            var amortissement = await _rep.GetById(id);
            if (amortissement is null)
            {
                return NotFound($"L'amortissement avec l'Id:{id} n'existe pas !");
            }
            return Ok(new { obj = amortissement });
        }

        [HttpPost]
        public async Task<IActionResult> AddAmortissements(AmortissmentAddDto amortDto)
        {
            var amortissement = await _rep.Add(amortDto);
            return Ok(new { message = "Amortissement Ajouté !", obj = amortissement });
        }

        [HttpPut]
        public async Task<IActionResult> UpadteAmortissement(AmortissmentUpdateDto amortissementDto)
        {
            var amortissement = await _rep.Upadte(amortissementDto);
            if (amortissement is null)
            {
                return NotFound($"Cet amortissement \"{amortissementDto.Id}\" n'existe pas !");
            }
            return Ok(new { msg = "Amortissement Modifié", obj = amortissement });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmortissement(int id)
        {
            bool deleted = await _rep.Delete(id);
            if (!deleted)
            {
                return NotFound($"L'amortissement avec l'Id:{id} n'exite pas !");
            }
            return Ok(new { msg = "L'amortissement est suprimé !" });
        }
    }
}