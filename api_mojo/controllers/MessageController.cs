using api_mojo.data;
using api_mojo.data.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> AddMsg(Message msg)
        {
            await db.Messages.AddAsync(msg);
            db.SaveChanges();
            return Ok(new { msg = "Message Ajouté !", obj = msg });
        }

        [HttpPut]
        public async Task<IActionResult> UpadteMessage(Message msg)
        {
            var fdb = await db.Messages.SingleOrDefaultAsync(a => a.Id == msg.Id);
            if (fdb is null)
            {
                return NotFound($"Ce User \"{msg.Id}\" n'existe pas !");
            }
            fdb.Contenu = msg.Contenu;
            fdb.DateEnvoi = msg.DateEnvoi;
            db.SaveChanges();
            return Ok(new { msg = "Message Modifié", obj = fdb });
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