using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Cove.Models;

namespace Cove.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly SignInManager<LoginModel> _signInManager;


        public HomeController(ILogger<HomeController> logger
            //, SignInManager<LoginModel> signInManager
            )
        {
            _logger = logger;
            //_signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Logout()
        {
            return RedirectToAction("Login", "Account");
        }
    }
}
