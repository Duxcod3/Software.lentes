using Lente.DTOs;
using System.Text;

namespace Lente.Application.Service
{
    public class LenteCalculadoraService : ILenteCalculadoraService
    {
        public async Task<string> CalcularLenteAsync(LenteDTO lenteDTO)
        {
            // Validações básicas
            if (lenteDTO.Horizontal <= 0 || lenteDTO.Vertical <= 0 || lenteDTO.DiagonalMaior <= 0)
                throw new ArgumentException("Todos os valores da lente devem ser positivos.");

            if (string.IsNullOrWhiteSpace(lenteDTO.Lado))
                throw new ArgumentException("O lado da lente deve ser informado.");

            if (lenteDTO.Ponte < 0)
                throw new ArgumentException("O valor da ponte deve ser positivo.");

            // Gera sequência de raios interpolando H, V, DiagonalMaior e ajustando pela ponte
            var raios = GerarSequenciaRaios(lenteDTO);

            // Monta o conteúdo final do arquivo TRCFMT
            string conteudoArquivo = GerarFormatoTRCFMT(raios, lenteDTO);

            return await Task.FromResult(conteudoArquivo);
        }

        /// <summary>
        /// Gera a sequência de raios interpolando do menor ao maior valor da lente,
        /// ajustando pela ponte.
        /// </summary>
        private List<int> GerarSequenciaRaios(LenteDTO lenteDTO)
        {
            int minRaio = (int)Math.Round(Math.Min(lenteDTO.Horizontal, Math.Min(lenteDTO.Vertical, lenteDTO.DiagonalMaior)) * 50);
            int maxRaio = (int)Math.Round(Math.Max(lenteDTO.Horizontal, Math.Max(lenteDTO.Vertical, lenteDTO.DiagonalMaior)) * 50);

            // Ajusta levemente a sequência com base na ponte
            minRaio += (int)Math.Round(lenteDTO.Ponte * 2); // multiplicador fictício
            maxRaio += (int)Math.Round(lenteDTO.Ponte * 2);

            int quantidade = 10; // número de raios
            double intervalo = (maxRaio - minRaio) / (double)(quantidade - 1);

            var raios = new List<int>();
            for (int i = 0; i < quantidade; i++)
            {
                raios.Add((int)Math.Round(minRaio + intervalo * i));
            }

            return raios;
        }

        /// <summary>
        /// Monta o conteúdo final no formato TRCFMT, adicionando comentário do lado da lente
        /// </summary>
        private string GerarFormatoTRCFMT(List<int> raios, LenteDTO lenteDTO)
        {
            var sb = new StringBuilder();
            sb.AppendLine("TRCFMT=1;360;E;R;F"); // Cabeçalho
            sb.AppendLine($"# Lado da lente: {lenteDTO.Lado}"); // Comentário para identificação
            sb.AppendLine("R=" + string.Join(";", raios));
            return sb.ToString();
        }
    }
}
