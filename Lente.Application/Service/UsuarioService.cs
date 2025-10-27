
using Lente.Application.Exceptions;
using Lente.Application.HasHelper;
using Lente.Application.Interface;
using Lente.Domain.Entities;
using Lente.Domain.Interfaces;
using Microsoft.Extensions.Logging;


namespace Lente.Application.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogger<UsuarioService> _logger;

        public UsuarioService(IUsuarioRepository usuarioRepository, ILogger<UsuarioService> logger)
        {
            _usuarioRepository = usuarioRepository;
            _logger = logger;
        }

        public async Task<Usuario?> AutenticarAsync(string nomeDeUsuario, string senha)
        {
            var usuario = await _usuarioRepository.GetByUsernameAsync(nomeDeUsuario);

            if (usuario == null)
            {
                _logger.LogWarning("Autenticação falhou: usuário '{User}' não encontrado.", nomeDeUsuario);
                return null;
            }

            if (!HashHelper.ValidarHash(senha, usuario.Senha))
            {
                _logger.LogWarning("Autenticação falhou: senha incorreta para usuário '{User}'.", nomeDeUsuario);
                return null;
            }

            _logger.LogInformation("Usuário '{User}' autenticado com sucesso.", nomeDeUsuario);
            return usuario;
        }

        public async Task<IEnumerable<Usuario>> ListarUsuariosAsync()
        {
            return await _usuarioRepository.GetAllAsync();
        }

        public async Task<Usuario> CriarUsuarioAsync(Usuario usuario, string senha)
        {
            usuario.Senha = HashHelper.GerarHashSenha(senha);
            await _usuarioRepository.AddAsync(usuario);
            _logger.LogInformation("Usuário '{User}' criado com sucesso.", usuario.NomeDeUsuario);
            return usuario;
        }

        public async Task AtualizarUsuarioAsync(Usuario usuarioAtualizado)
        {
            var usuarioExistente = await _usuarioRepository.GetByIdAsync(usuarioAtualizado.Id);
            if (usuarioExistente == null)
            {
                _logger.LogWarning("Tentativa de atualizar usuário inexistente com ID {Id}.", usuarioAtualizado.Id);
                throw new UsuarioNotFoundException(usuarioAtualizado.Id);
            }

            if (string.IsNullOrWhiteSpace(usuarioAtualizado.NomeDeUsuario))
                throw new ArgumentException("Nome de usuário não pode ser vazio.");

            if (string.IsNullOrWhiteSpace(usuarioAtualizado.Email))
                throw new ArgumentException("Email não pode ser vazio.");

            // Atualiza campos permitidos
            usuarioExistente.NomeDeUsuario = usuarioAtualizado.NomeDeUsuario;
            usuarioExistente.Email = usuarioAtualizado.Email;
            usuarioExistente.Perfil = usuarioAtualizado.Perfil;


            await _usuarioRepository.UpdateAsync(usuarioExistente);
            _logger.LogInformation("Usuário com ID {Id} atualizado com sucesso.", usuarioAtualizado.Id);
        }

        public async Task AtualizarSenhaAsync(int usuarioId, string novaSenha)
        {
            var usuarioExistente = await _usuarioRepository.GetByIdAsync(usuarioId);
            if (usuarioExistente == null)
            {
                _logger.LogWarning("Tentativa de atualizar senha de usuário inexistente com ID {Id}.", usuarioId);
                throw new UsuarioNotFoundException(usuarioId);
            }

            usuarioExistente.Senha = HashHelper.GerarHashSenha(novaSenha);
            await _usuarioRepository.UpdateAsync(usuarioExistente);
            _logger.LogInformation("Senha do usuário com ID {Id} atualizada com sucesso.", usuarioId);
        }

        public async Task ExcluirUsuarioAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario != null)
            {
                await _usuarioRepository.RemoveAsync(usuario);
                _logger.LogInformation("Usuário com ID {Id} removido com sucesso.", id);
            }
            else
            {
                _logger.LogWarning("Tentativa de remover usuário inexistente com ID {Id}.", id);
                throw new UsuarioNotFoundException(id);
            }
        }

        public async Task<Usuario?> ObterUsuarioPorIdAsync(int id)
        {
            return await _usuarioRepository.GetByIdAsync(id);
        }
    }
}


    

