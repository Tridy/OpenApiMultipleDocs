using Microsoft.AspNetCore.Mvc;

namespace MultipleDocs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FirstController : ControllerBase
    {
        [HttpGet(Name = "FirstGet")]
        public ActionResult<string> Get()
        {
            return "OK from First controller";
        }
    }
}