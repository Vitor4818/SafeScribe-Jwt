using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SafeScribe.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SafeScribe.Business
{
    public class JwtService
    {
        private readonly string _secret;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expireMinutes;

        public JwtService(IConfiguration config)
        {
            _secret = config["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key não encontrado");
            _issuer = config["Jwt:Issuer"] ?? throw new ArgumentNullException("Jwt:Issuer não encontrado");
            _audience = config["Jwt:Audience"] ?? throw new ArgumentNullException("Jwt:Audience não encontrado");
            _expireMinutes = int.Parse(config["Jwt:ExpireMinutes"] ?? "60");

        }

        public string GenerateToken(User user)
        {
 

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                expires: DateTime.UtcNow.AddMinutes(_expireMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
