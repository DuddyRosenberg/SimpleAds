using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleAds.Models;
using SimpleAds.Data;
using Microsoft.AspNetCore.Authorization;

namespace SimpleAds.Controllers
{
    public class HomeController : Controller
    {
        private GiveAwayDB _databaase = new GiveAwayDB("Data Source =.\\sqlexpress69; Initial Catalog = GiveAway; Integrated Security = True");
        public IActionResult Index()
        {
            return View(new IndexViewModel
            {
                Posts = _databaase.GetPosts(),
                userID = User.Identity.IsAuthenticated ? int.Parse(User.Identity.Name) : 0
            });
        }
        [HttpPost]
        public IActionResult DeletePost(int id)
        {
            _databaase.DeletePost(id);
            return Redirect("/");
        }
        public IActionResult NewAd()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(_databaase.GetUser(int.Parse(User.Identity.Name)));
            }
            return Redirect("/Account/LogIn");
        }
        [HttpPost]
        public IActionResult NewAd(Post post)
        {
            _databaase.AddPost(post);
            return Redirect("/");
        }
        [Authorize]
        public IActionResult MyAccount()
        {
            return View(_databaase.GetPosts(int.Parse(User.Identity.Name)));
        }
    }
}
