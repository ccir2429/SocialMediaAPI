using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
   
    public class CallBackController : Controller
    {
        private static User crtUser = new User();
        private readonly UserContext _userContext;

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

            return Redirect(ConfigurationManager.AppSettings["localhostRedirect"]);
        }

        private async Task GetDataFromFb(string code)
        {
            User newUser = new User(); 
            var rawParameters = new Dictionary<string, string>
            {
                {"client_id",ConfigurationManager.AppSettings["client_id"]},
                {"redirect_uri",ConfigurationManager.AppSettings["redirect_uri"]},
                {"client_secret",ConfigurationManager.AppSettings["client_secret"] },
                {"code",code}
            };
            var urlContent = new FormUrlEncodedContent(rawParameters);
            string access_token = String.Empty;
            using( var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(ConfigurationManager.AppSettings["fb_access_token"],urlContent);
                var responseString = await response.Content.ReadAsStringAsync();
                var jsResult = (JObject)JsonConvert.DeserializeObject(responseString);
                access_token = (string)jsResult["access_token"];
            }
            using (var requestClient = new HttpClient())
            {
                var fields = ConfigurationManager.AppSettings["media_fields"];
                var response = await requestClient.GetAsync(ConfigurationManager.AppSettings["fb_media"]+ "?fields="+fields+" & access_token="+access_token);
                var responseString = await response.Content.ReadAsStringAsync();
                var jsResult = (JObject)JsonConvert.DeserializeObject(responseString);
                try
                {
                    newUser.Location = jsResult["location"]["name"].ToString();// locatie
                }
                catch (Exception e) { }
                //need to add: name, email, profile pic
                newUser.Name = jsResult["name"].ToString(); // username
                newUser.ID = jsResult["id"].ToString(); // id
                try { 
                newUser.Email = jsResult["email"].ToString(); // email
                newUser.Feed = jsResult["feed"].ToString(); // email
                var feedDataList= jsResult["feed"]["data"];
                   /* foreach (var feedData in feedDataList)
                    {
                        var urlResponse = await requestClient.GetAsync("https://graph.facebook.com/v5.0/" + feedData["id"] +"/?access_token="  + access_token);
                        var stringReal= await urlResponse.Content.ReadAsStringAsync();
                        var jsReal = (JObject)JsonConvert.DeserializeObject(stringReal);
                        var real = jsReal;

                        newUser.realUrlList +=" "+real.ToString();
                    }*/
                }
                catch (Exception e) { }
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
    }
}