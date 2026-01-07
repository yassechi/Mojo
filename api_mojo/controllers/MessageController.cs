using infrastructure_mojo.repositories;
using Microsoft.AspNetCore.Mvc;
using core_mojo.models;
using core_mojo.Dtos;

namespace api_mojo.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly MessageRepository _rep;
        private readonly DiscussionRepository _discussionRep;

        public MessageController(MessageRepository rep, DiscussionRepository discussionRep)
        {
            _rep = rep; 
            _discussionRep = discussionRep;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllMessages()
        {
            var messages = await _rep.GetAll();
            return Ok(messages);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMsgById(int id)
        {
            var msg = await _rep.GetById(id);
            if (msg is null)
            {
                return NotFound($"Le Message avec l'Id:{id} n'existe pas !");
            }
            return Ok(new { obj = msg });
        }

        [HttpPost]
        public async Task<IActionResult> AddMsg(MessageAddDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var discussion = await _discussionRep.GetById(dto.DiscussionId);
            if (discussion is null)
            {
                return BadRequest($"La discussion avec l'Id {dto.DiscussionId} n'existe pas.");
            }

            Message message = new()
            {
                Contenu = dto.Contenu,
                DateEnvoi = dto.DateEnvoi,
                DiscussionId = dto.DiscussionId
            };

            var msg = await _rep.Add(message);

            if (msg is not null)
            {
                return Ok(new { message = "Message Ajouté !", obj = msg });
            }
            return BadRequest($"L'ajout du message du {dto.DateEnvoi} a échoué.");
        }

        [HttpPut]
        public async Task<IActionResult> UpadteMessage(MessageUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var message = await _rep.GetById(dto.Id);

            if (message is null)
            {
                return NotFound($"Le message avec l'Id {dto.Id} n'existe pas.");
            }

            message.Contenu = dto.Contenu;
            message.DateEnvoi = dto.DateEnvoi;

            var msgUpdate = await _rep.Upadte(message);
            if (msgUpdate is not null)
            {
                return Ok(new { message = "Message modifié avec succès.", obj = msgUpdate });
            }
            
            return BadRequest($"La modification du message a échoué.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            if (await _rep.Delete(id))
            {
                return Ok(new { msg = "Le Message est supprimé !" });
            }
            return NotFound($"Le Message avec l'Id:{id} n'existe pas !");
        }
    }
}