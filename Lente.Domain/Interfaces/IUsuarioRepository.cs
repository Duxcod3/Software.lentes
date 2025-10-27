using Lente.Domain.Entities;



namespace Lente.Domain.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario?> GetByUsernameAsync(string NomeUsuario);
    }
}
    

