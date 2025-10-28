using AutoMapper;
using Lente.Domain.Entities;
using Lente.DTOs;
using Microsoft.AspNetCore.Mvc;
using Software.lentes.DTOs;

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

        // Criar Lente
        [HttpPost]
        public async Task<IActionResult> CriarLente(LenteDTO lenteDTO)
        {
            // Validações básicas
            if (lenteDTO.Horizontal <= 0 || lenteDTO.Vertical <= 0 || lenteDTO.DiagonalMaior <= 0 || lenteDTO.Ponte < 0)
            {
                return BadRequest(new { Message = "Os valores de horizontal, vertical, diagonal e ponte devem ser positivos." });
            }

            if (string.IsNullOrWhiteSpace(lenteDTO.Lado))
            {
                return BadRequest(new { Message = "O lado da lente (Esquerda/Direita) é obrigatório." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var lente = _mapper.Map<LenteModelo>(lenteDTO);

                if (lenteDTO.UsuarioId != 0)
                {
                    lente.UsuarioId = lenteDTO.UsuarioId;
                }
                else
                {
                    return BadRequest(new { Message = "O UsuarioId é obrigatório." });
                }

                var createdLente = await _lenteService.CriarLenteAsync(lente);
                var createdLenteDTO = _mapper.Map<LenteDTO>(createdLente);

                return CreatedAtAction(nameof(ObterLente), new { id = createdLenteDTO.Id }, createdLenteDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro interno ao criar a lente.", Details = ex.Message });
            }
        }

        // Atualizar Lente
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarLente(int id, LenteDTO lenteDTO)
        {
            // Validações básicas
            if (lenteDTO.Horizontal <= 0 || lenteDTO.Vertical <= 0 || lenteDTO.DiagonalMaior <= 0 || lenteDTO.Ponte < 0)
            {
                return BadRequest(new { Message = "Os valores de horizontal, vertical, diagonal e ponte devem ser positivos." });
            }

            if (string.IsNullOrWhiteSpace(lenteDTO.Lado))
            {
                return BadRequest(new { Message = "O lado da lente (Esquerda/Direita) é obrigatório." });
            }

            var lenteExistente = await _lenteService.ObterLentePorIdAsync(id);
            if (lenteExistente == null)
            {
                return NotFound(new { Message = "Lente não encontrada." });
            }

            try
            {
                var lenteAtualizada = _mapper.Map<LenteModelo>(lenteDTO);
                lenteAtualizada.Id = id;

                if (lenteAtualizada.UsuarioId == 0)
                {
                    lenteAtualizada.UsuarioId = lenteExistente.UsuarioId;
                }

                await _lenteService.AtualizarLenteAsync(lenteAtualizada);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro interno ao atualizar a lente.", Details = ex.Message });
            }
        }

        // Excluir Lente
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirLente(int id)
        {
            var lenteExistente = await _lenteService.ObterLentePorIdAsync(id);
            if (lenteExistente == null)
            {
                return NotFound(new { Message = "Lente não encontrada." });
            }

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

        // Obter Lente por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<LenteDTO>> ObterLente(int id)
        {
            var lente = await _lenteService.ObterLentePorIdAsync(id);
            if (lente == null)
            {
                return NotFound(new { Message = "Lente não encontrada." });
            }

            var lenteDTO = _mapper.Map<LenteDTO>(lente);
            return Ok(lenteDTO);
        }
    }
}
