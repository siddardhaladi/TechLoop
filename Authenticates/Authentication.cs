using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechLoop.Data;

namespace TechLoop.Authenticates
{
    public class Authentication : IAuthentication
    {
        private readonly TechLoopDbContext _context;
        private readonly string _key;
        public Authentication(TechLoopDbContext context, string key)
        {
            _context = context;
            _key = key;
        }

        public string Authenticate(string email, string password,string role)
        {
            var x = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (x == null) { return null; }
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, email));
            claims.Add(new Claim(ClaimTypes.Role, role));
            var Expires = DateTime.UtcNow.AddHours(1);
            var keyBytes = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(keyBytes,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "TechLoop",
                audience: "TechLoopUsers",
                signingCredentials: creds,
                claims: claims,
                expires: Expires
                );
            return (new JwtSecurityTokenHandler().WriteToken(token));
            
        }
    }
}
