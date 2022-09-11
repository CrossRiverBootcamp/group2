using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Account.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsHistoryController : ControllerBase
    {
        // GET: api/<OperationsHistoryController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OperationsHistoryController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OperationsHistoryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<OperationsHistoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OperationsHistoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
