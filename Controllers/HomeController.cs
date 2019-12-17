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

        private string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "AppConfigKeyNotFound";
                return result;
            }
            catch (ConfigurationErrorsException)
            {
                return "ConfigErrorException";
            }
        }
        public void Login()
        {
            
            var client_id = ReadSetting("client_id");
            var redirect_uri = ReadSetting("redirect_uri");
            var response_type = ReadSetting("response_type");
            var authUrl = ReadSetting("fb_token");
            authUrl+="&client_id=" + client_id
                    + "&redirect_uri=" + redirect_uri
                    + "&response_type=" + response_type;
            
            Response.Redirect(authUrl);
            
        }
    }
}
