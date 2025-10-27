using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Lente.Application.HasHelper
{
    public class JwtService
    {
        private readonly string _secret;
        private readonly int _expirationMinutes;

        public JwtService(string secret, int expirationMinutes)
        {
            _secret = secret;
            _expirationMinutes = expirationMinutes;
        }

        public string GerarToken(string usuarioId, string perfil)
        {
            //Esse objeto vai gerar e escrever o token JWT.
            var tokenHandler = new JwtSecurityTokenHandler();
            //Converte a chave secreta em bytes
            var key = Encoding.ASCII.GetBytes(_secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.NameIdentifier, usuarioId),
                    new Claim(ClaimTypes.Role, perfil)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_expirationMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}


    

