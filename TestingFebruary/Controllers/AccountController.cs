using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TestingFebruary.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestingFebruary.Controllers
{
    public class AccountController : Controller
    {
        // тестовые данные вместо использования базы данных
        //private List<Person> people = new List<Person>
        //{
        //    new Person {Id=1, Login="admin@gmail.com", Password="12345", Role = "admin" },
        //    new Person {Id=2, Login="qwerty@gmail.com", Password="55555", Role = "user" }
        //};

        PostgresContext db = new PostgresContext();


        [HttpPost("/token")]
        public IActionResult Token(string username, string password)   //метод Token  обрабатывает запросы POST и через параметры принимает логин и пароль пользователя
        {
            var identity = GetIdentity(username, password);    //GetIdentity для поиска пользователя в этом списке по логину и паролю
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,  //в токен добавляются набор объектов Claim, которые содержат информацию о логине и роли пользователя.
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);  //создается Json-представление токена
           
            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name,
                
            };
            
            return Json(response);
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            Person person = db.usersql.FirstOrDefault(x => x.Login == username && x.Password == password);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}
