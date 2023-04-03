using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using Data.Utility;

namespace Data.Classes
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<TokenSettings> _config;
        // private readonly IConfiguration _config;
        private readonly UserManager<AppUser> _userManager;
        private readonly SymmetricSecurityKey _key;
        public TokenService(IOptions<TokenSettings> Config,UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _config = Config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Value.SecretKey));
            
        }
        public async Task<string> CreateToken(AppUser appUser)
        {
            //List of who the current user claim to be! 
            var ClaimsList = new List<Claim>
            {
               new Claim(JwtRegisteredClaimNames.GivenName,appUser.Email),
               new Claim(JwtRegisteredClaimNames.Email,appUser.Email),
               new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };
            var roles = await _userManager.GetRolesAsync(appUser);
            ClaimsList.AddRange(roles.Select(x => new Claim(ClaimTypes.Role,x)));
            //Creating the Credentials for the current User.
            var authKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config.Value.SecretKey));
            //Describing the Token, first, sec, and third part of the token.
            // var TokenDescription = new SecurityTokenDescriptor
            // {
            //     Subject = new ClaimsIdentity(ClaimsList),
            //     Expires = DateTime.Now.AddDays(10),
            //     SigningCredentials = new SigningCredentials(authKey, SecurityAlgorithms.HmacSha512Signature)
            // };
            var Expires = DateTime.Now.AddDays(10);
            var creds = new SigningCredentials(authKey, SecurityAlgorithms.HmacSha512Signature);
            //Creating a token handler which will create the token
            var Tinfo = new JwtSecurityToken(issuer:_config.Value.VaildIssuer,audience:_config.Value.VaildAudience,claims:ClaimsList,signingCredentials:creds,expires:Expires);
            var TokenHandler = new JwtSecurityTokenHandler();
            // var Token = TokenHandler.CreateToken();
            //returning the actual token.
            return TokenHandler.WriteToken(Tinfo);
        }
    }
}