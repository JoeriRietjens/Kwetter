using System;
using System.Threading.Tasks;
using kwetter_backend.Data;
using kwetter_backend.models;
using kwetter_backend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace kwetter_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KweetController : ControllerBase
    {
        private KweetContext Context;

        public KweetController(KweetContext context)
        {
            Context = context;
        }

        //For testing the controller
        [HttpGet]
        public string Ping()
        {
            return "Pong";
        }

        [HttpPost]
        public async Task<ActionResult<Kweet>> Create([FromBody] CreateKweetRequest createKweetRequest)
        {
            Kweet kweet = new();
            Console.WriteLine(createKweetRequest.Message);
            kweet.ProfileId = Guid.NewGuid();
            kweet.Message = createKweetRequest.Message;
            try
            {
                var response = await Context.Kweets.AddAsync(kweet);
                await Context.SaveChangesAsync();
                Console.WriteLine(response);
                
                return Ok(response.Entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("All")]
        public async Task<ActionResult> Get()
        {
            var response = Context.Kweets;
            Console.WriteLine(response);
            return Ok(response);
        }
    }
}
