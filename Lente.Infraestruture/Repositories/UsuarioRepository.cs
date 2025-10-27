using Lente.Domain.Entities;
using Lente.Domain.Interfaces;
using Lente.Infraestruture.Data;
using Microsoft.EntityFrameworkCore;


namespace Lente.Infraestruture.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        private readonly UsuarioContext _context;
    
     public UsuarioRepository(UsuarioContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Usuario?> GetByUsernameAsync(string nomeDeUsuario)
        {
            if (string.IsNullOrEmpty(nomeDeUsuario)) return null;

            var nomeMinusculo = nomeDeUsuario.ToLower();
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NomeDeUsuario.ToLower() == nomeMinusculo);
        }
    }
}
    
