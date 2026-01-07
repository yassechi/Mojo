using Microsoft.AspNetCore.Mvc;
using core_mojo.models;
using infrastructure_mojo.Interfaces;
using core_mojo.Dtos;
using infrastructure_mojo.repositories;

namespace api_mojo.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscussionController : ControllerBase
    {
        private readonly DiscussionRepository _rep;
        private readonly UserRepository _userRep;

        public DiscussionController(DiscussionRepository rep, UserRepository userRep)
        {
            _rep = rep; 
            _userRep = userRep;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllDiscussions()
        {
            var discussions = await _rep.GetAll();
            return Ok(discussions);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDiscussionById(int id)
        {
            var discussion = await _rep.GetById(id);
            if (discussion is null)
            {
                return NotFound($"La discussion avec l'Id:{id} n'existe pas !");
            }
            return Ok(new { obj = discussion });
        }

        [HttpPost]
        public async Task<IActionResult> AddDiscussion(DiscussionAddDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var client = await _userRep.GetById2(dto.ClientId);
            if (client is null)
            {
                return BadRequest($"L'utilisateur (Client) avec l'Id {dto.ClientId} n'existe pas.");
            }

            var mojo = await _userRep.GetById2(dto.MojoId);
            if (mojo is null)
            {
                return BadRequest($"L'utilisateur (Mojo) avec l'Id {dto.MojoId} n'existe pas.");
            }

            Discussion discussion = new()
            {
                Objet = dto.Objet,
                Status = dto.Status,
                DateCreation = dto.DateCreation,
                ClientId = dto.ClientId,
                MojoId = dto.MojoId
            };

            var disc = await _rep.Add(discussion);

            if (disc is not null)
            {
                return Ok(new { message = "Discussion Ajoutée !", obj = disc });
            }
            return BadRequest($"L'ajout de la discussion {dto.Objet} a échoué.");
        }

        [HttpPut]
        public async Task<IActionResult> UpadteDiscussion(DiscussionUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var discussion = await _rep.GetById(dto.Id);

            if (discussion is null)
            {
                return NotFound($"La discussion avec l'Id {dto.Id} n'existe pas.");
            }

            discussion.Objet = dto.Objet;
            discussion.Status = dto.Status;
            discussion.DateCreation = dto.DateCreation;

            var discUpdate = await _rep.Upadte(discussion);
            if (discUpdate is not null)
            {
                return Ok(new { message = $"La discussion {discussion.Objet} est modifiée.", obj = discUpdate });
            }
            
            return BadRequest($"La modification de la discussion {dto.Objet} a échoué.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteDiscussion(int id)
        {
            if (await _rep.Delete(id))
            {
                return Ok(new { msg = "La discussion est supprimée !" });
            }
            return NotFound($"La discussion avec l'Id:{id} n'existe pas !");
        }
    }
}