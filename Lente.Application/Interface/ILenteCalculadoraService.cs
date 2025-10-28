using Lente.DTOs;
using Software.lentes.DTOs;
using System.Threading.Tasks;

namespace Lente.Application.Service
{
    public interface ILenteCalculadoraService
    {
        Task<string> CalcularLenteAsync(LenteDTO lenteDTO);
    }
}



