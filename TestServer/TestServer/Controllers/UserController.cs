using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TestServer.Models;
using TestServer.Models.ViewModels;

namespace TestServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class UserController : ControllerBase
    {
        private TestContext db;

        public UserController(TestContext context)
        {
            db = context;
            if (!db.Users.Any())
            {
                Role r1 = new Role {Name = "Admin"};
                db.Roles.Add(r1);
                db.SaveChanges();

                Role adminRole = db.Roles.FirstOrDefault(x => x.Name == "Admin");
                db.Users.Add(new User { Login = "Admin", Password = "123456", Token = "AdministratorToken", Role = r1 });
                db.SaveChanges();
            }
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public IActionResult LogIn([FromBody] UserVM requestUser)
        {
            if (requestUser == null)
            {
                return BadRequest("Некорректные входные данные");
            }

            try
            {
              User finedUser =  db.Users.FirstOrDefault(x => x.Login == requestUser.Login && x.Password == requestUser.Password);
              Role finedRole = db.Roles.FirstOrDefault(x => x.RoleId == finedUser.RoleId);
                if (finedUser == null)
                {
                    return BadRequest("Пользователь не найден");
                }
                else
                {
                    UserVM responseUser = new UserVM(finedUser);
                    if (responseUser.Role == null)
                        responseUser.Role = finedRole.Name;
                    return Ok(responseUser);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Ошибка работы сервера");
            }
            
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {


        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
