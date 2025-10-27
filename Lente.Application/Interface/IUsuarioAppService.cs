using Lente.Domain.Entities;


namespace Lente.Application.Interface
{
    public interface IUsuarioService
    {
        Task<Usuario?> AutenticarAsync(string nomeDeUsuario, string senha);
        Task<IEnumerable<Usuario>> ListarUsuariosAsync();
        Task<Usuario> CriarUsuarioAsync(Usuario usuario, string senha);
        Task AtualizarUsuarioAsync(Usuario usuario);
        Task ExcluirUsuarioAsync(int id);

        Task AtualizarSenhaAsync(int usuarioId, string novaSenha);

        // NOVO MÉTODO ADICIONADO
        Task<Usuario?> ObterUsuarioPorIdAsync(int id);
    }
}
