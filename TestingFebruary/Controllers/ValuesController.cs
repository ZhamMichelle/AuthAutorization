using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestingFebruary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [Authorize]
        [Route("getlogin")]
        public IActionResult GetLogin()
        {
            return Ok($"Ваш логин: {User.Identity.Name}");
        }

        [Authorize(Roles = "admin")]
        [Route("getrole")]
        public IActionResult GetRole()
        {
            return Ok("Ваша роль: Администратор");
        }

        [Authorize(Roles = "manager")]
        [Route("getmanager")]
        public IActionResult Manager()
        {
            return View();
        }

        [Authorize(Roles = "verificator")]
        [Route("getverificator")]
        public IActionResult Verificator()
        {
            return Verificator();
      
        }
    }
}
