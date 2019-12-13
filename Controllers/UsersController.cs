using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
       // private readonly UserContext _context;

        public UsersController()
        {
            //_context = context;
        }
        /*
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> getUserContext()
        {
            return await _context.UserContexts.ToListAsync();
        }
        */

        [HttpGet]
        public async Task<ActionResult<User>> GetUser() {
            CallBackController ctrl = new CallBackController();
            User userData = ctrl.GetData();
            return  userData;
        }
    }
}