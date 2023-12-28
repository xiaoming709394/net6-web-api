using Microsoft.AspNetCore.Mvc;
using net6_web_api.DB;
using net6_web_api.Entity;

namespace net6_web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly SchoolContext _context;

        public StudentsController(ILogger<StudentsController> logger, SchoolContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public MainResponse GetAllStudent()
        {
            var student = _context.Students.ToList();
            return new MainResponse()
            {
                IsSuccess = true,
                Content = student
            };
        }
    }
}
