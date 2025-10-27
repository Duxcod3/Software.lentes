using Lente.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Software.lentes.DTOs;

namespace Software.lentes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IJwtService _jwtService;

        public AuthController(IUsuarioService usuarioService, IJwtService jwtService)
        {
            _usuarioService = usuarioService;
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            if (loginDto == null
                || string.IsNullOrWhiteSpace(loginDto.NomeDeUsuario)
                || string.IsNullOrWhiteSpace(loginDto.Senha))
                return BadRequest("Usuário e senha são obrigatórios.");

            // Remover espaços em branco extras
            var nomeDeUsuario = loginDto.NomeDeUsuario.Trim();
            var senha = loginDto.Senha.Trim();

            var usuario = await _usuarioService.AutenticarAsync(nomeDeUsuario, senha);

            if (usuario == null)
                return Unauthorized("Usuário ou senha inválidos.");

            var token = _jwtService.GerarToken(usuario);

            return Ok(new { token });
        }
    }
}




