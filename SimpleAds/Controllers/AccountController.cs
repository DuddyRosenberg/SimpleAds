using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SimpleAds.Data;

namespace SimpleAds.Controllers
{
    public class AccountController : Controller
    {
        private GiveAwayDB _databaase = new GiveAwayDB("Data Source =.\\sqlexpress69; Initial Catalog = GiveAway; Integrated Security = True");
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(string email, string Password)
        {
            var user = _databaase.GetUser(email);
            if (BCrypt.Net.BCrypt.Verify(Password, user.PasswordHash))
            {
                HttpContext.SignInAsync(new ClaimsPrincipal(
                    new ClaimsIdentity(new List<Claim>{new Claim("user",$"{user.ID}")},
                    "Cookies", "user", "role"))).Wait();
            }
            return Redirect("/");
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return Redirect("/");
        }
    }
}
