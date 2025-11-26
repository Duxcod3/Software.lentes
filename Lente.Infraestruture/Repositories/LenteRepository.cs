using Lente.Domain.Entities;
using Lente.Domain.Interfaces;
using Lente.Infraestruture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lente.Infraestruture.Repositories
{
    public class LenteRepository : RepositoryBase<LenteModelo>, ILenteRepository
    {

        public LenteRepository(UsuarioContext context) : base(context)
        {
        }
        // Se precisar de métodos específicos para Lente, pode implementá-los aqui
        // Por exemplo, métodos de consulta que não estão cobertos pelo repositório base

        public async Task<LenteModelo?> GetByDimensionsAsync(double horizontal, double vertical, double diagonal)
        {
            return await _dbSet
                .FirstOrDefaultAsync(l => l.Horizontal == horizontal && l.Vertical == vertical && l.DiagonalMaior == diagonal);
        }
    }
}
