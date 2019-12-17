using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{

    public class CallBackController : Controller
    {
        private static User crtUser = new User();
        //private readonly UserContext _userContext;

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult<User> InstantData()
        {
            return crtUser;
        }

        public async Task<ActionResult> CallBack(string code)
        {
            if (!String.IsNullOrEmpty(code))
            {
                code = code.ToString();
                await GetDataFromFb(code);
            }

            return Redirect(ReadSetting("localhostRedirect"));
        }

        private async Task GetDataFromFb(string code)
        {
            User newUser = new User();
            var rawParameters = new Dictionary<string, string>
            {
                {"client_id",ReadSetting("client_id")},
                {"redirect_uri",ReadSetting("redirect_uri")},
                {"client_secret",ReadSetting("client_secret") },
                {"code",code}
            };
            var urlContent = new FormUrlEncodedContent(rawParameters);
            string access_token = String.Empty;
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(ReadSetting("fb_access_token"), urlContent);
                var responseString = await response.Content.ReadAsStringAsync();
                var result = new Models.Result(responseString,true);
                access_token = result.Access_token;
            }
            using (var requestClient = new HttpClient())
            {
                var fields = ReadSetting("media_fields");
                var response = await requestClient.GetAsync(ReadSetting("fb_media") + "?fields=" + fields + " & access_token=" + access_token);
                var responseString = await response.Content.ReadAsStringAsync();
                var result = new Models.Result(responseString, false);
                try
                {
                    newUser.Location = result.Location;// locatie
                }
                catch (Exception e) { Console.Error.WriteLine(e); }
                //need to add: name, email, profile pic
                newUser.Name = result.Name; // username
                newUser.ID = result.ID; // id
                try
                {
                    newUser.Email = result.Email; // email
                    newUser.Feed = result.Feed; // email
                }
                catch (Exception e) { Console.Error.WriteLine(e); }
                newUser.token = access_token;
            }
            SaveData(newUser);
        }

        private void SaveData(User newUser)
        {
            crtUser = newUser;
        }

        public User GetData()
        {
            return crtUser;
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
                return "AppConfigException";
            }
        }
    }
}