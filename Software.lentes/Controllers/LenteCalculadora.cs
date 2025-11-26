using Lente.DTOs;
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
                return BadRequest("Dados da lente não foram fornecidos.");

            if (string.IsNullOrWhiteSpace(lenteDTO.Job))
                return BadRequest("O número do Job (ou OS) é obrigatório para gerar o arquivo.");

            try
            {
                // Gera o conteúdo do arquivo no padrão TRCFMT
                var conteudoArquivo = await _lenteCalculadoraService.CalcularLenteAsync(lenteDTO);

                // Garante que o campo JOB esteja no final do arquivo
                if (!conteudoArquivo.TrimEnd().EndsWith($"JOB={lenteDTO.Job}", StringComparison.OrdinalIgnoreCase))
                {
                    conteudoArquivo = $"{conteudoArquivo.TrimEnd()}\nJOB={lenteDTO.Job}";
                }

                // Converte o conteúdo para bytes
                var fileBytes = System.Text.Encoding.UTF8.GetBytes(conteudoArquivo);

                // Define o nome do arquivo com base no número da OS
                var nomeArquivo = $"{lenteDTO.Job}.vca";

                // Retorna o arquivo .vca para download
                return File(fileBytes, "application/octet-stream", nomeArquivo);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Erro interno ao calcular a lente.",
                    Details = ex.Message
                });
            }
        }
    }
}
