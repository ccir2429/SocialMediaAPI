using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class User
    {
        [Key]
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Email { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string ProfePicture { get; set; }
        public string Feed { get; set; }
        public double Likes { get; set; }
        public string Location { get; set; }
        public string token { get; set; }
        public string realUrlList {get;set ;}
        public override string ToString()
        {
            if (ID == "" || ID == null)
            { return "[]"; }
            else
                return "["
                    + "\"id\":\"" + ID + "\","
                    + "\"name\":\"" + Name + "\","
                    + "\"email\":\"" + Email + "\","
                    + "\"likes\":" + Likes + ","
                    + "\"location\":\"" + Location + "\","
                    + "\"feed\":" + Feed 
                    + "]";
        }

    }
}
