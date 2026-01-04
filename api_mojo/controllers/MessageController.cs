using Microsoft.AspNetCore.Mvc;
using data_mojo.models;
using data_mojo.dtos;
using data_mojo.interfaces;

namespace api_mojo.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IRepository<Message, MessageAddDto, MessageUpdateDto> _rep;

        public MessageController(IRepository<Message, MessageAddDto, MessageUpdateDto> rep)
        {
            _rep = rep;
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
        public async Task<IActionResult> AddMsg(MessageAddDto messageAddDto)
        {
            var message = await _rep.Add(messageAddDto);
            return Ok(new { msg = "Message Ajouté !", obj = message });
        }

        [HttpPut]
        public async Task<IActionResult> UpadteMessage(MessageUpdateDto messageUpdateDto)
        {
            var message = await _rep.Upadte(messageUpdateDto);
            if (message is null)
            {
                return NotFound($"Le message avec l'Id \"{messageUpdateDto.Id}\" n'existe pas !");
            }
            return Ok(new { msg = "Message Modifié", obj = message });
        }

        [HttpDelete("{id}")]
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