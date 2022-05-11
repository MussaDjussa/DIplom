using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{

    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationContext _appCont;

        public UserController(ApplicationContext appCont)
        {
            _appCont = appCont;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _appCont.Users;
        }

        [HttpPost]
        public void Post([FromBody] User user)
        {
            _appCont.Users.Add(user);
            _appCont.SaveChanges();
        }
    }
}
