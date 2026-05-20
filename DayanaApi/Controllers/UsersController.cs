using Microsoft.AspNetCore.Mvc;

namespace DayanaApi.Controllers
{
    // Esto define los campos del usuario: id, name, email
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // Datos iniciales de prueba para que no aparezca vacío
        private static List<User> _users = new List<User>
        {
            new User { Id = 1, Name = "Alice", Email = "alice@mail.com" },
            new User { Id = 2, Name = "Juan", Email = "juan@mail.com" }
        };

        // GET: /api/Users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(_users);
        }

        // GET: /api/Users/{id}
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // POST: /api/Users
        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] User newUser)
        {
            newUser.Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1;
            _users.Add(newUser);
            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }

        // PUT: /api/Users/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;

            return NoContent();
        }

        // DELETE: /api/Users/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            _users.Remove(user);
            return NoContent();
        }
    }
}