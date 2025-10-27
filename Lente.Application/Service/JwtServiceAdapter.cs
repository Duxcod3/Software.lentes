using Lente.Application.Interface;
using Lente.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lente.Application.Service
{
    public class JwtServiceAdapter : IJwtService
    {
        private readonly JwtService _jwtService;

        public JwtServiceAdapter(IConfiguration configuration)
        {
            var jwtSection = configuration.GetSection("Jwt");

            var secret = jwtSection["Key"];
            var issuer = jwtSection["Issuer"];
            var audience = jwtSection["Audience"];
            var expiresInMinutes = int.Parse(jwtSection["ExpiresInMinutes"]);

            _jwtService = new JwtService(secret, issuer, audience, expiresInMinutes);
        }

        public string GerarToken(Usuario usuario)
        {
            return _jwtService.GerarToken(usuario.Id.ToString(), usuario.Perfil);
        }
    }
}

    

