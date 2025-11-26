using AutoMapper;
using Lente.Domain.Entities;
using Lente.DTOs;
using Microsoft.AspNetCore.Mvc;
using Software.lentes.DTOs;
using System;
using System.Threading.Tasks;

namespace Software.lentes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LenteController : ControllerBase
    {
        private readonly ILenteService _lenteService;
        private readonly IMapper _mapper;

        public LenteController(ILenteService lenteService, IMapper mapper)
        {
            _lenteService = lenteService;
            _mapper = mapper;
        }

        // GET: api/lente
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lentes = await _lenteService.ListarLentesAsync();
            return Ok(lentes);
        }

        // GET: api/lente/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<LenteDTO>> ObterLente(int id)
        {
            var lente = await _lenteService.ObterLentePorIdAsync(id);
            if (lente == null)
                return NotFound(new { Message = "Lente não encontrada." });

            var lenteDTO = _mapper.Map<LenteDTO>(lente);
            return Ok(lenteDTO);
        }

        // POST: api/lente
        [HttpPost]
        public async Task<IActionResult> CriarLente([FromBody] LenteDTO lenteDTO)
        {
            if (lenteDTO.Horizontal <= 0 || lenteDTO.Vertical <= 0 || !lenteDTO.DiagonalMaior.HasValue || lenteDTO.DiagonalMaior <= 0 || lenteDTO.Ponte < 0)
                return BadRequest(new { Message = "Os valores de horizontal, vertical, diagonal e ponte devem ser positivos e obrigatórios." });

            if (string.IsNullOrWhiteSpace(lenteDTO.Lado))
                return BadRequest(new { Message = "O lado da lente (Esquerda/Direita) é obrigatório." });

            if (lenteDTO.UsuarioId == 0)
                return BadRequest(new { Message = "O UsuarioId é obrigatório." });

            try
            {
                var lente = _mapper.Map<LenteModelo>(lenteDTO);

                // Garante que a data de criação seja registrada
                lente.CreatedAt = DateTime.UtcNow;

                var createdLente = await _lenteService.CriarLenteAsync(lente);

                // Retorna o objeto completo da lente criada
                return Ok(createdLente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro interno ao criar a lente.", Details = ex.Message });
            }
        }

        // PUT: api/lente/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> AtualizarLente(int id, [FromBody] LenteDTO lenteDTO)
        {
            if (lenteDTO.Horizontal <= 0 || lenteDTO.Vertical <= 0 || !lenteDTO.DiagonalMaior.HasValue || lenteDTO.DiagonalMaior <= 0 || lenteDTO.Ponte < 0)
                return BadRequest(new { Message = "Os valores de horizontal, vertical, diagonal e ponte devem ser positivos e obrigatórios." });

            if (string.IsNullOrWhiteSpace(lenteDTO.Lado))
                return BadRequest(new { Message = "O lado da lente (Esquerda/Direita) é obrigatório." });

            var lenteExistente = await _lenteService.ObterLentePorIdAsync(id);
            if (lenteExistente == null)
                return NotFound(new { Message = "Lente não encontrada." });

            try
            {
                var lenteAtualizada = _mapper.Map<LenteModelo>(lenteDTO);
                lenteAtualizada.Id = id;

                if (lenteAtualizada.UsuarioId == 0)
                    lenteAtualizada.UsuarioId = lenteExistente.UsuarioId;

                await _lenteService.AtualizarLenteAsync(lenteAtualizada);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro interno ao atualizar a lente.", Details = ex.Message });
            }
        }

        // DELETE: api/lente/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> ExcluirLente(int id)
        {
            var lenteExistente = await _lenteService.ObterLentePorIdAsync(id);
            if (lenteExistente == null)
                return NotFound(new { Message = "Lente não encontrada." });

            try
            {
                await _lenteService.ExcluirLenteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro interno ao excluir a lente.", Details = ex.Message });
            }
        }
    }
}
