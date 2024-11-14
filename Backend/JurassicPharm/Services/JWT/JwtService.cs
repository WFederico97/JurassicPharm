using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JurassicPharm.Models;
using Microsoft.IdentityModel.Tokens;

namespace JurassicPharm.Services.JWT
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(string userEmail, Empleado employee, double? expireTime = null)
        {
            double time = expireTime ?? Convert.ToDouble(_configuration["Jwt:ExpiresInMinutes"]);

            // Define los claims del token.
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, userEmail),
                new Claim(ClaimTypes.Role, employee.Rol),
                new Claim("UserId", employee.LegajoEmpleado.ToString()),
                new Claim("FullName", $"{employee.Nombre} {employee.Apellido}")
            };

            // Obtén la clave de firma desde la configuración.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Genera el token con la fecha de expiración calculada.
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(time), // Usa el tiempo calculado.
                signingCredentials: creds
            );

            // Devuelve el token generado.
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool VerifyToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();


            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };


            var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);


            var jwtToken = validatedToken as JwtSecurityToken;

            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }

            return true;

        }

        public Dictionary<string, string>? DecodeToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            if (jwtToken == null)
                return null;

            // Extraer todos los claims en un diccionario
            var claims = jwtToken.Claims.ToDictionary(c => c.Type, c => c.Value);

            return claims;
        }


    }
}
