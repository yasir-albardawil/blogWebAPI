using Microsoft.AspNetCore.Mvc;

namespace PieShop.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ContactController : Controller
    {
        [HttpGet]
        public ActionResult GetContact()
        {
            return Ok();
        }
    }
}
