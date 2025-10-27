using Lente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lente.Domain.Interfaces
{
    public interface ILenteRepository : IRepository<LenteModelo>
    {
        // Aqui   adicionar métodos específicos para a lente, caso necessário
        // Exemplo de um método específico:
        Task<LenteModelo?> GetByDimensionsAsync(double horizontal, double vertical, double diagonal);
        // Outros métodos específicos de consulta ou comportamento
    }
}
