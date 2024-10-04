using KBZLifeInsuranceCodeTest.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult Content(object obj)
        {
            return Content(obj.ToJson(), "application/json");
        }
    }
}
