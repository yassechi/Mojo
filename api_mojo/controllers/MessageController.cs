using api_mojo.data;
using api_mojo.data.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_mojo.dtos;

namespace api_mojo.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly AppDbContext db;
        public MessageController(AppDbContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllMessages()
        {
            var msg = await db.Messages.ToListAsync();
            return Ok(msg);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMsgById(int id)
        {
            var msg = await db.Messages.SingleOrDefaultAsync(a => a.Id == id);
            if (msg is null)
            {
                return NotFound($"Le Msg avec l'Id:{id} n'existe pas !");
            }
            return Ok(new { obj = msg });
        }

        [HttpPost]
        public async Task<IActionResult> AddMsg(MessageAddDto messageAddDto)
        {
            Message message = new()
            {
                Contenu = messageAddDto.Contenu,
                DateEnvoi = messageAddDto.DateEnvoi,
                UserId = messageAddDto.UserId,
                DiscussionId = messageAddDto.DiscussionId
            };
            await db.Messages.AddAsync(message);
            db.SaveChanges();
            return Ok(new { msg = "Message Ajouté !", obj = message });
        }

        [HttpPut]
        public async Task<IActionResult> UpadteMessage(MessageUpdateDto messageUpdateDto)
        {
            var message = await db.Messages.SingleOrDefaultAsync(a => a.Id == messageUpdateDto.Id);
            if (message is null)
            {
                return NotFound($"Ce User \"{messageUpdateDto.Id}\" n'existe pas !");
            }
            message.Contenu = messageUpdateDto.Contenu;
            message.DateEnvoi = messageUpdateDto.DateEnvoi;
            db.SaveChanges();
            return Ok(new { msg = "Message Modifié", obj = message });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var msg = await db.Messages.SingleOrDefaultAsync(a => a.Id == id);
            if (msg is null)
            {
                return NotFound($"Le Message avec l'Id:{id} n'exite pas !");
            }
            db.Remove(msg);
            db.SaveChanges();
            return Ok(new { msg = "Le Message est suprimé !" });
        }

    }
}