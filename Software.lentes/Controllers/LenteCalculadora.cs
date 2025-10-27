
using Microsoft.AspNetCore.Mvc;
using Software.lentes.DTOs;


namespace Lente.Application.Service
{
    [ApiController]
    [Route("api/[controller]")]
    public class LenteCalculadoraController : ControllerBase
    {
        private readonly ILenteCalculadoraService _lenteCalculadoraService;

        public LenteCalculadoraController(ILenteCalculadoraService lenteCalculadoraService)
        {
            _lenteCalculadoraService = lenteCalculadoraService;
        }

        [HttpPost("calcular")]
        public async Task<IActionResult> CalcularLente([FromBody] LenteDTO lenteDTO)
        {
            if (lenteDTO == null)
            {
                return BadRequest("Dados da lente não foram fornecidos.");
            }

            try
            {
                // Chama o serviço para calcular os raios e gerar o arquivo
                var conteudoArquivo = await _lenteCalculadoraService.CalcularLenteAsync(lenteDTO);

                // Retorna o conteúdo como um arquivo de texto (TRCFMT)
                var fileBytes = System.Text.Encoding.UTF8.GetBytes(conteudoArquivo);
                return File(fileBytes, "application/octet-stream", "lente.trcfmt");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro interno ao calcular a lente.", Details = ex.Message });
            }
        }
    }
}
