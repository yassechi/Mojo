using api_mojo.data;
using api_mojo.data.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_mojo.dtos;

namespace api_mojo.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext db;
        public UserController(AppDbContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            var user = await db.Users.ToListAsync();
            return Ok(user);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await db.Users.SingleOrDefaultAsync(a => a.Id == id);
            if (user is null)
            {
                return NotFound($"Le user avec l'Id:{id} n'existe pas !");
            }
            return Ok(new { obj = user });
        }
        
        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetUserByName(string name)
        {
            var user = await db.Users.SingleOrDefaultAsync(a => a.LasttName == name);
            if (user is null)
            {
                return NotFound($"Le user {name} n'existe pas !");
            }
            return Ok(new { obj = user });
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserAddDto userAddDto)
        {
            User user = new()
            {
                FirstName = userAddDto.FirstName,
                LasttName = userAddDto.LasttName,
                Email = userAddDto.Email,
                Hpassword = userAddDto.Hpassword,
                Role = userAddDto.Role,
                TailleCm = userAddDto.TailleCm,
                IsActif = userAddDto.IsActif,
                OrganisationId = userAddDto.OrganisationId
            };
            await db.Users.AddAsync(user);
            db.SaveChanges();
            return Ok(new { message = "User Ajouté !", obj = user });
        }

        [HttpPut]
        public async Task<IActionResult> UpadteUser(UserUpdateDto userUpdateDto)
        {
            var user = await db.Users.SingleOrDefaultAsync(a => a.Id == userUpdateDto.Id);
            if (user is null)
            {
                return NotFound($"Ce User \"{userUpdateDto.LasttName}\" n'existe pas !");
            }
            user.FirstName = userUpdateDto.FirstName;
            user.LasttName = userUpdateDto.LasttName;
            user.Email = userUpdateDto.Email;
            user.Hpassword = userUpdateDto.Hpassword;
            user.Role = userUpdateDto.Role;
            user.TailleCm = userUpdateDto.TailleCm;
            user.IsActif = userUpdateDto.IsActif;
            db.SaveChanges();
            return Ok(new { msg = "User Modifié", obj = user });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await db.Users.SingleOrDefaultAsync(a => a.Id == id);
            if (user is null)
            {
                return NotFound($"Le user avec l'Id:{id} n'exite pas !");
            }
            db.Remove(user);
            db.SaveChanges();
            return Ok(new { msg = "Le user est suprimé !" });
        }

    }
}