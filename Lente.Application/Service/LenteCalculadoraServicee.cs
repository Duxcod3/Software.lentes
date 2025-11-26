using System.Text;
using System.Threading.Tasks;
using Lente.DTOs;

namespace Lente.Application.Service
{
    public class LenteCalculadoraService : ILenteCalculadoraService
    {
        public async Task<string> CalcularLenteAsync(LenteDTO lenteDTO)
        {
            await Task.Delay(50); // Simulação de processamento

            var sb = new StringBuilder();

            // Cabeçalho TRCFMT[´´
            sb.AppendLine("TRCFMT=1;360;E;R;F");

            // Exemplo simples de cálculo usando os dados reais da lente
            // (Substitua por sua fórmula real, se houver)
            double hbox = lenteDTO.Horizontal;
            double vbox = lenteDTO.Vertical;
            double ponte = lenteDTO.Ponte;

            // Gera pontos simulando o contorno, mas proporcional ao tamanho da lente
            int pontos = 100;
            var random = new Random();
            for (int i = 0; i < pontos; i++)
            {
                var valoresR = Enumerable.Range(0, 10)
                    .Select(_ =>
                    {
                        // Simula variação leve (evita deformação extrema)
                        double baseValue = (hbox + vbox) * 18 + random.NextDouble() * 100;
                        return baseValue.ToString("F1", System.Globalization.CultureInfo.InvariantCulture);
                    })
                    .ToArray();

                sb.AppendLine($"R={string.Join(';', valoresR)}");
            }

            // Segunda face
            sb.AppendLine("TRCFMT=1;360;E;L;F");
            for (int i = 0; i < pontos; i++)
            {
                var valoresR = Enumerable.Range(0, 10)
                    .Select(_ =>
                    {
                        double baseValue = (hbox + vbox) * 18 + random.NextDouble() * 100;
                        return baseValue.ToString("F1", System.Globalization.CultureInfo.InvariantCulture);
                    })
                    .ToArray();

                sb.AppendLine($"R={string.Join(';', valoresR)}");
            }

            // Campos técnicos usando dados reais
            sb.AppendLine("UNI=0;0");
            sb.AppendLine($"DBL={ponte.ToString("F1", System.Globalization.CultureInfo.InvariantCulture)}");
            sb.AppendLine($"VBOX={vbox.ToString("F1", System.Globalization.CultureInfo.InvariantCulture)};{vbox.ToString("F1", System.Globalization.CultureInfo.InvariantCulture)}");
            sb.AppendLine($"HBOX={hbox.ToString("F1", System.Globalization.CultureInfo.InvariantCulture)};{hbox.ToString("F1", System.Globalization.CultureInfo.InvariantCulture)}");
            sb.AppendLine("FTYP=1");
            sb.AppendLine($"FMFR={lenteDTO.Fabricante ?? "emmanuel"}");
            sb.AppendLine($"FUPC={lenteDTO.CodigoProduto ?? "001"}");
            sb.AppendLine($"FRAM={lenteDTO.Modelo ?? "emmanuel"}");
            sb.AppendLine("ETYP=1");
            sb.AppendLine("BEVP=7");
            sb.AppendLine("BEVM=");
            sb.AppendLine("GWIDTH=0.6");
            sb.AppendLine("GDEPTH=0.6");
            sb.AppendLine("FPINB=");
            sb.AppendLine("PINB=0.5");
            sb.AppendLine("BTILT=;");
            sb.AppendLine("FCRV=;");
            sb.AppendLine("ZTILT=;");
            sb.AppendLine("POLISH=0");
            sb.AppendLine("_CTO=1;1");

            //  Campo JOB (OS)
            sb.AppendLine($"JOB={lenteDTO.Job}");

            return sb.ToString();
        }
    }
}
