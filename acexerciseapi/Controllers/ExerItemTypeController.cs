using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace acexerciseapi.Controllers
{
    [Produces("application/json")]
    [Route("api/ExerItemType")]
    public class ExerItemTypeController : Controller
    {
        // GET: api/ExerItemType
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ExerItemType/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/ExerItemType
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/ExerItemType/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
