using api_mojo.data;
using api_mojo.data.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var user = await db.Accessoires.ToListAsync();
            return Ok(user);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await db.Users.SingleOrDefaultAsync(a => a.Id == id);
            if (user is null)
            {
                return NotFound($"Le user avec l'Id:{id} n'existe pas !");
            }
            return Ok(new { obj = user });
        }

        [HttpGet("name")]
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
        public async Task<IActionResult> AddUser(User user)
        {
            await db.Users.AddAsync(user);
            db.SaveChanges();
            return Ok(new { message = "User Ajouté !", obj = user });
        }

        [HttpPut]
        public async Task<IActionResult> UpadteUser(User user)
        {
            var fdb = await db.Users.SingleOrDefaultAsync(a => a.Id == user.Id);
            if (fdb is null)
            {
                return NotFound($"Ce User \"{user.LasttName}\" n'existe pas !");
            }
            fdb.FirstName = user.FirstName;
            fdb.LasttName = user.LasttName;
            fdb.Email = user.Email;
            fdb.Hpassword = user.Hpassword;
            fdb.Role = user.Role;
            fdb.TailleCm = user.TailleCm;
            fdb.IsActif = user.IsActif;
            db.SaveChanges();
            return Ok(new { msg = "User Modifié", obj = fdb });
        }

        [HttpDelete]
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