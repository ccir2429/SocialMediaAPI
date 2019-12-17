using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class Result
    {
        public Result(string responseString,bool isToken) {
            var res = (JObject)JsonConvert.DeserializeObject(responseString);
            if (isToken) 
            {
                Access_token = res["access_token"].ToString();
            }
            else
            {
                Name = res["name"].ToString();
                ID = res["id"].ToString();
                Email = res["email"].ToString();
                Feed = res["feed"].ToString();
                try
                {
                    Location = res["location"]["name"].ToString();
                }
                catch (Exception)
                {
                    Location = "";
                }
            }
        }
        public string Access_token { get; set; }
        public string Name{get;set;}
        public string ID{get;set;}
        public string Email{get;set;}
        public string Feed{get;set;}
        public string Location{get;set; }
    }
}
