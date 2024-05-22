using WebApi.Domain.Model.Entity;
using WebApi.Security.Handlers.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using WebApi.Security.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;




namespace WebApi.Security.Handlers.Implementations
{
    public class JwtHandler : IJwtHandler
    {
        private readonly AppSettings _appSettings;


        public JwtHandler(IOptions<AppSettings> appSettings) {
        
        
        _appSettings = appSettings.Value;
        
        
        }
        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.secret);
            List<string> scopes = new List<string>();
            foreach (var roleName in user.roles)
            {
                scopes.Add(roleName.Rolname.ToString());
            }



            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("roles",string.Join(",",scopes))

                }),              
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GetUserNameFromToken(string token)
        {
            Console.WriteLine("buscando usuario");

            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtHandler.ReadJwtToken(token);
            var username = jwtToken.Claims.FirstOrDefault();           
            return username.Value;

        }
        public string[] GetRolesFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            return jwtToken.Claims.Where(c => c.Type == "roles").Select(c => c.Value).ToArray();
        }
        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =  new SymmetricSecurityKey (key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);


                return true;
            }
            catch (Exception ex) when (ex is SecurityTokenExpiredException ||
                                        ex is SecurityTokenInvalidSignatureException ||
                                        ex is SecurityTokenInvalidIssuerException ||
                                        ex is SecurityTokenInvalidAudienceException)
            {
                
                Console.WriteLine(ex);
                return false;
            }


        }
    }
}
