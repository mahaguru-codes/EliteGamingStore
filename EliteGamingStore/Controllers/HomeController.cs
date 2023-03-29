using EliteGamingStore.Models;
using EliteGamingStore.Models.Beans;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EliteGamingStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public Result<List<Friend>> GetFriends()
        {
            Result<List<Friend>> result = new Result<List<Friend>>();
            try
            {
                FriendModel model = new FriendModel();
                result = model.GetFriends();
            }
            catch(Exception err)
            {
                result.isSuccess = false;
                result.message = err.Message;
                result.exception = err;
            }

            return result;
        }

        [HttpPost]
        public Result<Friend> CreateNewFriend([FromBody] Friend friend)
        {
            Result<Friend> result = new Result<Friend>();
            try
            {
                FriendModel obj = new FriendModel();
                result = obj.CreateFriend(friend);
            }
            catch(Exception err)
            {
                result.isSuccess = false;
                result.exception = err;
            }

            return result;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
