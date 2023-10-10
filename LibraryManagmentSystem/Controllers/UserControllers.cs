using LibaryManagmentSystemAPI;
using LibraryManagmentSystem;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Library.Applicatin_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public static LibraryDbContext _Context;
        public UserController(LibraryDbContext db)
        {
            _Context = db;
        }


        [HttpPost("User Login")]

        public IActionResult post(string email, string password)
        {
            var user = _Context.users.FirstOrDefault(x => x.Email == email && x.Password == password);
            if (user != null)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var data = new List<Claim>();
                data.Add(new Claim("Email", user.Email));


                var token = new JwtSecurityToken(
                  issuer: "Muhanad",
                audience: "TRA",
                claims: data,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
                );

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
            {
                return Unauthorized("the user doesn't exist");
            }
        }
    }
}