using Microsoft.AspNetCore.Mvc;
using net6_web_api.DB;
using net6_web_api.Entity;

namespace net6_web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LuckySheetController : ControllerBase
    {
        private readonly ILogger<LuckySheetController> _logger;
        private readonly SchoolContext _context;

        public LuckySheetController(ILogger<LuckySheetController> logger, SchoolContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public ResponseResult GetLuckySheetList()
        {
            try
            {
                var data = _context.LuckySheet.ToList().Select(x => new LuckySheet
                {
                    Id = x.Id,
                    Title = x.Title,
                });
                return new ResponseResult()
                {
                    code = 0,
                    data = data
                };
            }
            catch (Exception ex)
            {
                return new ResponseResult()
                {
                    code = 1,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        public ResponseResult SaveLuckySheetData(LuckySheet model)
        {
            try
            {
                if (model.Id == 0)
                {
                    model.Title = "表格" + DateTime.Now.ToString("yyMMddHHmmss");
                    _context.LuckySheet.Add(model);
                }
                else
                {
                    _context.LuckySheet.Update(model);
                }
                _context.SaveChanges();

                return new ResponseResult()
                {
                    code = 0,
                    data = model
                };
            }
            catch (Exception ex)
            {
                return new ResponseResult()
                {
                    code = 1,
                    message = ex.Message
                };
            }
        }

        [HttpGet]
        public ResponseResult GetLuckySheetData(int id)
        {
            try
            {
                var data = _context.LuckySheet.Where(x => x.Id == id).ToList();
                return new ResponseResult()
                {
                    code = 0,
                    data = data[0]
                };
            }
            catch (Exception ex)
            {
                return new ResponseResult()
                {
                    code = 1,
                    message = ex.Message
                };
            }
        }
    }
}
