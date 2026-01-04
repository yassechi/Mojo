using Microsoft.AspNetCore.Mvc;
using data_mojo.models;
using data_mojo.dtos;
using data_mojo.interfaces;

namespace api_mojo.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscussionController : ControllerBase
    {
        private readonly IRepository<Discussion, DiscussionAddDto, DiscussionUpdateDto> _rep;

        public DiscussionController(IRepository<Discussion, DiscussionAddDto, DiscussionUpdateDto> rep)
        {
            _rep = rep;
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
        public async Task<IActionResult> AddDiscussion(DiscussionAddDto discussionAddDto)
        {
            var discussion = await _rep.Add(discussionAddDto);
            return Ok(new { msg = "Discussion Ajoutée !", obj = discussion });
        }

        [HttpPut]
        public async Task<IActionResult> UpadteDiscussion(DiscussionUpdateDto discussionUpdateDto)
        {
            var discussion = await _rep.Upadte(discussionUpdateDto);
            if (discussion is null)
            {
                return NotFound($"La discussion avec l'ID \"{discussionUpdateDto.Id}\" n'existe pas !");
            }
            return Ok(new { msg = "Discussion Modifiée", obj = discussion });
        }

        [HttpDelete("{id}")]
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