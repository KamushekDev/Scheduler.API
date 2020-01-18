using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class TestController : ControllerBase
    {
        private IHostEnvironment _environment;

        public TestController(IHostEnvironment environment)
        {
            _environment = environment;
        }

        // GET: api/Test
        [HttpGet]
        public IActionResult Get()
        {
            var variables = Environment.GetEnvironmentVariables().Cast<DictionaryEntry>()
                .Select(x => new {name = x.Key, value = x.Value}).ToList();

            variables.Add(new
            {
                name = (object) "user ip address",
                value = (object) Request.HttpContext.Connection.RemoteIpAddress.ToString()
            });

            var headers = Request.Headers.Select(x => new {name = (object) x.Key, value = (object) x.Value.ToString()});
            variables.AddRange(headers);

            return new OkObjectResult(variables);
        }

        // GET: api/Test/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "Text from controller protected with [Autorize]";
        }

        // POST: api/Test
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Test/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}