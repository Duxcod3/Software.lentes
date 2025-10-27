using AutoMapper;
using Lente.Application.Interfaces;
using Lente.Domain.Entities;
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
            // Verificação de valores positivos antes de chamar o serviço
            if (lenteDTO.Horizontal <= 0 || lenteDTO.Vertical <= 0 || lenteDTO.DiagonalMaior <= 0)
            {
                return BadRequest(new { Message = "Os valores de horizontal, vertical e diagonal precisam ser positivos." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Retorna erros de validação do modelo
            }

            try
            {
                var lente = _mapper.Map<LenteModelo>(lenteDTO);

                // Garantir que o UsuarioId seja atribuído corretamente
                if (lenteDTO.UsuarioId != 0)  // Caso o UsuarioId não seja 0, associar corretamente
                {
                    lente.UsuarioId = lenteDTO.UsuarioId;
                }
                else
                {
                    // Retornar erro se o UsuarioId for obrigatório
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
        public async Task<IActionResult> AtualizarLente(int id, LenteAtualizarDTO lenteAtualizarDTO)
        {
            // Verificação de valores positivos antes de chamar o serviço
            if (lenteAtualizarDTO.Horizontal <= 0 || lenteAtualizarDTO.Vertical <= 0 || lenteAtualizarDTO.DiagonalMaior <= 0)
            {
                return BadRequest(new { Message = "Os valores de horizontal, vertical e diagonal precisam ser positivos." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Retorna erros de validação do modelo
            }

            var lenteExistente = await _lenteService.ObterLentePorIdAsync(id);
            if (lenteExistente == null)
            {
                return NotFound(new { Message = "Lente não encontrada." });
            }

            try
            {
                var lenteAtualizada = _mapper.Map<LenteModelo>(lenteAtualizarDTO);
                lenteAtualizada.Id = id;  // Garantir que o ID seja mantido

                // Se o UsuarioId não for passado, mantemos o valor atual
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
                return NoContent(); // Retorna 204 No Content após excluir a lente
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
