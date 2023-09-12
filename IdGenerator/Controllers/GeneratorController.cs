using IdGenerator.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdGenerator.Controllers
{
    [Route("getnextid")]
    [ApiController]
    public class GeneratorController : ControllerBase
    {
        private readonly IIdGenerator _generator;

        // test using the command in powershell: Invoke-WebRequest -URI https://localhost:7295/getnextid/C001

        public GeneratorController(IIdGenerator generator)
        {
            _generator = generator;
        }

        [HttpGet("{clientId}")]
        public ActionResult<int> GetNextId(string clientId)
        {
            var id = _generator.Generate(clientId);
            return Ok(id);
        }
    }
}
