using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntitiesLib;
using EntitiesLib.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SampleApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext db;

        public UsersController(AppDbContext context)
        {
            db = context;
        }

        [HttpGet]
        [Produces("application/json")] //Данные в любом случаем будут отдаваться в формате JSON
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return new ObjectResult(await db.Users.ToListAsync());
        }

        // GET api/users/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<User>> Post(User user)
        {
            // обработка частных случаев валидации
            if (user.Age == 99)
                ModelState.AddModelError("Age", "Возраст не должен быть равен 99");

            if (user.Name == "admin")
            {
                ModelState.AddModelError("Name", "Недопустимое имя пользователя - admin");
            }
            // если есть лшибки - возвращаем ошибку 400
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            db.Users.Add(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        // PUT api/users/
        [HttpPut]
        public async Task<ActionResult<User>> Put(User user)
        {
            if (user == null) return BadRequest();
            if (!db.Users.Any(x => x.Id == user.Id)) return NotFound();

            db.Update(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == id);
            if (user == null) return NotFound();
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
    }
}