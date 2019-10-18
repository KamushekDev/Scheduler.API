using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private IHostEnvironment _environment;

        public TestController(IHostEnvironment environment)
        {
            _environment = environment;
        }

        // GET: api/Test
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var strings = Environment.GetEnvironmentVariables().Cast<DictionaryEntry>()
                .Select(x => $"{x.Key}:{x.Value}").ToList();

            strings.Insert(0, _environment.EnvironmentName);
            return strings;
        }

        // GET: api/Test/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
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