using Software.lentes.DTOs;


namespace Lente.Application.Service
{
    public class LenteCalculadoraService : ILenteCalculadoraService
    {
        public async Task<string> CalcularLenteAsync(LenteDTO lenteDTO)
        {
            if (lenteDTO.Horizontal <= 0 || lenteDTO.Vertical <= 0 || lenteDTO.DiagonalMaior <= 0)
                throw new ArgumentException("Todos os valores da lente devem ser positivos.");

            // Calcula a sequência de raios
            var raios = CalcularRaios(lenteDTO);

            // Gera o formato final para o arquivo TRCFMT
            string conteudoArquivo = GerarFormatoTRCFMT(raios);

            return await Task.FromResult(conteudoArquivo);
        }

        private List<int> CalcularRaios(LenteDTO lenteDTO)
        {
            int minRaio = (int)Math.Round(lenteDTO.Horizontal * 50); // multiplicador fictício
            int maxRaio = (int)Math.Round(lenteDTO.DiagonalMaior * 45); // multiplicador fictício

            int quantidade = 10; // quantidade de raios na sequência
            List<int> raios = new List<int>();
            double intervalo = (maxRaio - minRaio) / (double)(quantidade - 1);

            for (int i = 0; i < quantidade; i++)
            {
                int raio = (int)Math.Round(minRaio + intervalo * i);
                raios.Add(raio);
            }

            return raios;
        }

        private string GerarFormatoTRCFMT(List<int> raios)
        {
            string header = "TRCFMT=1;360;E;R;F";
            string corpo = $"R={string.Join(";", raios)}";
            return $"{header}\n{corpo}";
        }
    }
}
