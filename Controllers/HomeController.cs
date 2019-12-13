using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication3.Models;

namespace WebApplication3.Controllers
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public void Login()
        {
            var client_id = ConfigurationManager.AppSettings["client_id"];
            var redirect_uri = ConfigurationManager.AppSettings["redirect_uri"];
            var response_type = ConfigurationManager.AppSettings["response_type"];
            var authUrl = ConfigurationManager.AppSettings["fb_token"]
                + "&client_id=" + client_id
                + "&redirect_uri=" + redirect_uri
                + "&response_type=" + response_type;
            
            Response.Redirect(authUrl);
            
        }
    }
}
