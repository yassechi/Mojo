using infrastructure_mojo.repositories;
using Microsoft.AspNetCore.Mvc;
using core_mojo.models;
using core_mojo.Dtos;

namespace api_mojo.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _rep;
        private readonly OrganisationRepository _orgRep;

        public UserController(UserRepository rep, OrganisationRepository orgRep)
        {
            _rep = rep;
            _orgRep = orgRep;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            var users = await _rep.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _rep.GetById2(id);
            if (user is null)
            {
                return NotFound($"Le user avec l'Id:{id} n'existe pas !");
            }
            return Ok(new { obj = user });
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetUserByName(string name)
        {
            var user = await _rep.GetByName(name);
            if (user is null)
            {
                return NotFound($"Le user {name} n'existe pas !");
            }
            return Ok(new { obj = user });
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserAddDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var organisation = await _orgRep.GetById(dto.OrganisationId);
            if (organisation is null)
            {
                return BadRequest($"L'organisation avec l'Id {dto.OrganisationId} n'existe pas.");
            }

            User user = new()
            {
                FirstName = dto.FirstName,
                LasttName = dto.LasttName,
                Email = dto.Email,
                Password = dto.Password,
                Role = dto.Role,
                TailleCm = dto.TailleCm ?? 0,
                IsActif = dto.IsActif,
                OrganisationId = dto.OrganisationId,
                UserName = dto.UserName,
                PhoneNumber = dto.PhoneNumber
            };

            var us = await _rep.Add(user);

            if (us is not null)
            {
                return Ok(new { message = "User Ajoutée !", obj = user });
            }
            return BadRequest($"L'ajout de l'utilisateur {dto.LasttName} a échoué ");
        }

        [HttpPut]
        public async Task<IActionResult> UpadteUser(UserUpdateDto dto)
        {
            var user = await _rep.GetById(dto.Id);

            if (user is null)
            {
                return NotFound($"L'utilisateur avec l'Id {dto.Id} n'existe pas");
            }

            var organisation = await _orgRep.GetById(dto.OrganisationId);
            if (organisation is null)
            {
                return BadRequest($"L'organisation avec l'Id {dto.OrganisationId} n'existe pas.");
            }

            user.FirstName = dto.FirstName;
            user.LasttName = dto.LasttName;
            user.Email = dto.Email;
            user.Password = dto.Password;
            user.Role = dto.Role;
            user.TailleCm = dto.TailleCm;
            user.IsActif = dto.IsActif;
            user.PhoneNumber = dto.PhoneNumber;
            user.OrganisationId = dto.OrganisationId;

            var usrUpdate = await _rep.Upadte(user);
            if (usrUpdate is not null)
            {
                return Ok($"L'utilisateur {user.LasttName} est modifiée avec succès..");
            }
            
            return BadRequest($"La modification de l'utilisateur {dto.LasttName} a échoué !!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (await _rep.Delete(id))
            {
                return Ok(new { msg = "Le user est supprimé !" });
            }
            return NotFound($"Le user avec l'Id:{id} n'existe pas !");
        }
    }
}