using Microsoft.AspNetCore.Mvc;
using core_mojo.models;
using core_mojo.interfaces;
using core_mojo.interfaces;
using core_mojo.Dtos;

namespace api_mojo.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User, UserAddDto, UserUpdateDto> _rep;

        public UserController(IRepository<User, UserAddDto, UserUpdateDto> rep)
        {
            _rep = rep;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            var users = await _rep.GetAll();
            return Ok(users);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _rep.GetById(id);
            if (user is null)
            {
                return NotFound($"Le user avec l'Id:{id} n'existe pas !");
            }
            return Ok(new { obj = user });
        }
        
        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetUserById(string id)
        // {
        //     var user = await _rep.GetById2(id);
        //     if (user is null)
        //     {
        //         return NotFound($"Le user avec l'Id:{id} n'existe pas !");
        //     }
        //     return Ok(new { obj = user });
        // }


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
        public async Task<IActionResult> AddUser(UserAddDto userAddDto)
        {
            var user = await _rep.Add(userAddDto);
            return Ok(new { message = "User Ajouté !", obj = user });
        }

        [HttpPut]
        public async Task<IActionResult> UpadteUser(UserUpdateDto userUpdateDto)
        {
            var user = await _rep.Upadte(userUpdateDto);
            if (user is null)
            {
                return NotFound($"Ce User \"{userUpdateDto.LasttName}\" n'existe pas !");
            }
            return Ok(new { msg = "User Modifié", obj = user });
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