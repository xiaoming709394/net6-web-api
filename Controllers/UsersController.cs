using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using net6_web_api.DB;
using net6_web_api.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace net6_web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly SchoolContext _context;
        public UsersController(ILogger<UsersController> logger, SchoolContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public MainResponse AuthenticateUser(LoginModel loginModel)
        {
            return new MainResponse
            {
                IsSuccess = true,
                Content = new AuthenticateRequestAndResponse
                {
                    AccessToken = GenerateToken(),//"xiaoming_testToken",
                    RefreshToken = GenerateToken(),//"xiaoming_testToken2"
                }
            };
        }

        // Generates and returns a JWT token
        private static string GenerateToken()
         {
            var vv = "";
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Jwt: Key"));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier,"xiaoming"),
                new Claim(ClaimTypes.Role,"Role")
            };

                var token = new JwtSecurityToken("Jwt:Issuer",
                    "Jwt:Audience",
                    claims,
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: credentials);

                vv = new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                var msgs = ex.Message;
            }


            return vv;
        }

        [HttpPost]
        public MainResponse RefreshToken(AuthenticateRequestAndResponse authenticateRequestAndResponse)
        {
            return new MainResponse
            {
                IsSuccess = true,
                Content = new AuthenticateRequestAndResponse
                {
                    AccessToken = "xiaoming_testToken",
                    RefreshToken = "xiaoming_testToken2"
                }
            };
        }

        [HttpPost]
        public MainResponse RegisterUser(RegistrationModel registrationModel)
        {
            return new MainResponse
            {
                IsSuccess = true,
            };
        }
    }
}
