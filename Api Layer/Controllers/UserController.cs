using Api_Layer.topla;
using Business_Layer.Services;
using Data_Accese_Layer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api_Layer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController:ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtOptions _jwt;

        public UserController(IUserService userService,JwtOptions jwt)
        {
            _userService=userService;
            _jwt=jwt;
        }


        [HttpPost]
        [Route("")]
        public async Task<ActionResult<string>> Authentication(UserRequest request)
        {
            var user = new Users
            {
                TC = request.Tc,
                Password = request.Password
            };
           
            var User=await _userService.CheckUser(user);

            if(User!=null)
            {

                var handler = new JwtSecurityTokenHandler();

                var Descriptor = new SecurityTokenDescriptor
                {
                    Issuer = _jwt.Issuer,
                    Audience = _jwt.Audience,
                    
                    SigningCredentials = new SigningCredentials(new  SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SigningKey)),
                    SecurityAlgorithms.HmacSha256
                    ),
                    Subject=new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier,User.Id.ToString())
                      
                    })
                };

                var token=handler.CreateToken(Descriptor);
                var AccessToken=handler.WriteToken(token);
                return Ok(AccessToken);



            }

            return NotFound("Kullanci Bulunmadi");


        }


    }
}
