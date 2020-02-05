using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntitiesLib;
using EntitiesLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SampleApp.Filters;

namespace SampleApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [TypeFilter(typeof(SimpleResourceFilter), Arguments = new object[] {1, "blablabla"})]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext db;
        private readonly IMemoryCache _memoryCache;

        public UsersController(AppDbContext context, IMemoryCache memoryCache)
        {
            db = context;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        [Produces("application/json")] //Данные в любом случаем будут отдаваться в формате JSON
        //[Authorize]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return new ObjectResult(await db.Users.ToListAsync());
        }

        // GET api/users/5
        [HttpGet("{id:int}")]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<User>> Get(int id)
        {
            if (!_memoryCache.TryGetValue(id, out User user))
            {
                user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);

                if (user == null)
                    return NotFound();

                _memoryCache.Set(user.Id, user, new MemoryCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
            }

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