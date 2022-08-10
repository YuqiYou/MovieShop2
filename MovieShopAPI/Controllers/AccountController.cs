using ApplicationCore.Models;
using ApplicationCore.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;
        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
        {
            var user = await _accountService.CreateUser(model);
            return Ok(user);
        }

        //[HttpPost]
        //[Route("login")]
        //public async Task<IActionResult> Login()
        //{

        //} 

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            var user = await _accountService.ValidateUser(model);
            if(user != null)
            {
                //create token
                var token = CreateJWtToken(user);
                return Ok(new { token = token });
            }

            throw new UnauthorizedAccessException("Please check email and password");
            // return Unauthorized(new { ErrorMessage = "Please check email and password" });

            //IOS() ,  Android,Angular,React,Java
            //Token, JWT(Json Web Token)
            //Client will send email/password to API
            //API will validate that email/password and if valid then API will create the JWT token and send
            // to client, client's reposonsibility to store it somewhere
            //Angukar,React(local storage or sessionStorage in browser)

            // IOS/Android, -> device
            //when client needs some secure information or needs to perform any operation that require users to be
            // authenticated then client needs to sent the token to the API in the Http header.
            //Once the API receives the token from client, it will validate the JWT token , if valid it will
            //send the data back to the client. If invalid or token is expired then API will send 401

           

        }

        private string CreateJWtToken(UserInfoResponseModel user)
        {
            //create the token

            //create claims

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim("Country", "USA"),
                new Claim("language","englsih"),
                new Claim("isAdmin",(user.Id == 4) ? "true":"false")

            };

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            //specify a secret key
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["secretKey"]));

            //specify the algorithm
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            //specify the epiration of the token
            var tokenExpiration = DateTime.UtcNow.AddHours(2);

            //create an objeect with all the above info to create the token
            var tokenDetails = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = tokenExpiration,
                SigningCredentials = credentials,
                Issuer = "MovieShop, Inc",
                Audience = "MovieShop Clients"
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var encodedJwt = tokenHandler.CreateToken(tokenDetails);




            return  tokenHandler.WriteToken(encodedJwt);


        }

        [HttpGet]
        [Route("check-email")]

        public async Task<IActionResult> checkEmail([FromBody] UserLoginModel model)
        {

            var user = await _accountService.ValidateUser(model);
            if(user == null)
            {
                return NotFound(new { errorMessage = $"No User Found for {user}" });
            }

            return Ok(user);
        }




    }
}
