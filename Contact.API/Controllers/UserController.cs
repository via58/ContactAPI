using Contact.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Contact.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;

        public UserController(IJwtAuthenticationManager jwtAuthenticationManager)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

      

        // POST api/<UserController>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Post([FromBody] UserCred userCred)
        {
          var token=  jwtAuthenticationManager.Authenticate(userCred.username, userCred.password);
            if(token == null)
            {
              return Unauthorized();
            }
            return Ok(token);
        }
        // POST api/<UserController>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] UserCred userCred)
        {
            var newUser = await jwtAuthenticationManager.addUser(userCred);
            return newUser.id;
        }

        
    }
}
