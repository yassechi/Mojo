using data_mojo;
using data_mojo.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_mojo.dtos;

namespace api_mojo.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscussionController : ControllerBase
    {
        private readonly AppDbContext db;
        public DiscussionController(AppDbContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllDiscussions()
        {
            var discussion = await db.Discussions.ToListAsync();
            return Ok(discussion);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetdiscussionById(int id)
        {
            var discussion = await db.Discussions.SingleOrDefaultAsync(a => a.Id == id);
            if (discussion is null)
            {
                return NotFound($"La discussion avec l'Id:{id} n'existe pas !");
            }
            return Ok(new { obj = discussion });
        }

        [HttpPost]
        public async Task<IActionResult> AddDiscussion(DiscussionAddDto discussionAddDto)
        {
            Discussion discussion = new()
            {
                Objet = discussionAddDto.Objet,
                Status = discussionAddDto.Status,
                DateCreation = discussionAddDto.DateCreation,
                UserId = discussionAddDto.UserId
            };
            await db.Discussions.AddAsync(discussion);
            db.SaveChanges();
            return Ok(new { msg = "Discussion Ajouté !", obj = discussion });
        }

        [HttpPut]
        public async Task<IActionResult> UpadteDiscussion(DiscussionUpdateDto discussionUpdateDto)
        {
            var fdb = await db.Discussions.SingleOrDefaultAsync(a => a.Id == discussionUpdateDto.Id);
            if (fdb is null)
            {
                return NotFound($"Ce User \"{discussionUpdateDto.Id}\" n'existe pas !");
            }
            fdb.Objet = discussionUpdateDto.Objet;
            fdb.Status = discussionUpdateDto.Status;
            fdb.DateCreation = discussionUpdateDto.DateCreation;
            db.SaveChanges();
            return Ok(new { msg = "Discussion Modifié", obj = fdb });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscussion(int id)
        {
            var discussion = await db.Discussions.SingleOrDefaultAsync(a => a.Id == id);
            if (discussion is null)
            {
                return NotFound($"La discussion avec l'Id:{id} n'exite pas !");
            }
            db.Remove(discussion);
            db.SaveChanges();
            return Ok(new { msg = "La discussion est suprimé !" });
        }

    }
}