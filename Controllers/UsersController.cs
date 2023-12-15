using Microsoft.AspNetCore.Mvc;
using net6_web_api.DB;

namespace net6_web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<LuckySheetController> _logger;
        private readonly SchoolContext _context;

        
    }
}
