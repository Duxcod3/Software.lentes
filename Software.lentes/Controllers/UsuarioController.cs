using AutoMapper;
using Lente.Application.Exceptions;
using Lente.Application.Interface;
using Lente.Domain.Entities;
using Lente.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Software.lentes.DTOs;

namespace Software.lentes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;  // Mapper

        public UsuarioController(IUsuarioService usuarioService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        // GET: api/usuario
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var usuarios = await _usuarioService.ListarUsuariosAsync();
            return Ok(usuarios);
        }

        // GET: api/usuario/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var usuarios = await _usuarioService.ListarUsuariosAsync();
            var usuarioEncontrado = usuarios.FirstOrDefault(u => u.Id == id);

            if (usuarioEncontrado == null)
                return NotFound();

            return Ok(usuarioEncontrado);
        }
        // POST: api/usuario
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UsuarioAtualizarDTO usuarioDto)
        {
            if (usuarioDto == null || string.IsNullOrWhiteSpace(usuarioDto.Senha))
                return BadRequest("Usuário e senha são obrigatórios.");

            var usuario = new Usuario
            {
                NomeDeUsuario = usuarioDto.NomeDeUsuario,
                Email = usuarioDto.Email,
                Senha = usuarioDto.Senha,
                Perfil = usuarioDto.Perfil
           
            };

            var novoUsuario = await _usuarioService.CriarUsuarioAsync(usuario, usuarioDto.Senha);

            var usuarioRetorno = new UsuarioDTO
            {
                Id = novoUsuario.Id,
                NomeDeUsuario = novoUsuario.NomeDeUsuario,
                Email = novoUsuario.Email,
                Senha = usuarioDto.Senha,
                Perfil = usuarioDto.Perfil,
                Nome = usuarioDto.Nome
            };
            return CreatedAtAction(nameof(Get), new { id = usuarioRetorno.Id }, usuarioRetorno);
        }
        // PUT: api/usuario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UsuarioAtualizarDTO usuarioDto)
        {
            if (id != usuarioDto.Id)
                return BadRequest("ID incompatível.");

            try
            {
                var usuarioExistente = await _usuarioService.ObterUsuarioPorIdAsync(id);
                if (usuarioExistente == null)
                    return NotFound();

                // Atualiza apenas campos que não são nulos
                if (!string.IsNullOrWhiteSpace(usuarioDto.NomeDeUsuario)) usuarioExistente.NomeDeUsuario = usuarioDto.NomeDeUsuario;
                if (!string.IsNullOrWhiteSpace(usuarioDto.Email)) usuarioExistente.Email = usuarioDto.Email;
                if (!string.IsNullOrWhiteSpace(usuarioDto.Perfil)) usuarioExistente.Perfil = usuarioDto.Perfil;

                await _usuarioService.AtualizarUsuarioAsync(usuarioExistente);

                if (!string.IsNullOrWhiteSpace(usuarioDto.Senha))
                {
                    await _usuarioService.AtualizarSenhaAsync(id, usuarioDto.Senha);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno no servidor: " + ex.Message);
            }
        }


        // DELETE: api/usuario/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuarioExistente = await _usuarioService.ObterUsuarioPorIdAsync(id);
            if (usuarioExistente == null)
                return NotFound();

            await _usuarioService.ExcluirUsuarioAsync(id);
            return NoContent();
        }
    }
}



