using Microsoft.AspNetCore.Mvc;

namespace MultipleDocs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecondController : ControllerBase
    {
        [HttpGet(Name = "SecondGet")]
        public ActionResult<MyDto> Get()
        {
            return new MyDto
            {
                Message = "OK from Second controller"
            };
        }
    }
}