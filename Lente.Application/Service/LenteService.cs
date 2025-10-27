using Lente.Application.Exceptions;
using Lente.Domain.Entities;
using Lente.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Software.lentes.DTOs;


namespace Lente.Application.Service
{
    public class LenteService : ILenteService
    {
        private readonly ILenteRepository _lenteRepository;
        private readonly IUsuarioRepository _usuarioRepository; // Adicionando o repositório do usuário
        private readonly ILogger<LenteService> _logger;

        public LenteService(ILenteRepository lenteRepository, IUsuarioRepository usuarioRepository, ILogger<LenteService> logger)
        {
            _lenteRepository = lenteRepository;
            _usuarioRepository = usuarioRepository; // Inicializando o repositório de usuários
            _logger = logger;
        }

        // Criar lente
        public async Task<LenteModelo> CriarLenteAsync(LenteModelo lente)
        {
            if (lente == null)
            {
                _logger.LogWarning("Tentativa de criar uma lente nula.");
                throw new ArgumentNullException(nameof(lente), "A lente não pode ser nula.");
            }

            // Validação dos dados da lente (horizontal, vertical e diagonal devem ser positivos)
            if (lente.Horizontal <= 0 || lente.Vertical <= 0 || lente.Diagonal <= 0)
            {
                _logger.LogWarning("Tentativa de criar lente com valores inválidos: Horizontal={Horizontal}, Vertical={Vertical}, Diagonal={Diagonal}.", lente.Horizontal, lente.Vertical, lente.Diagonal);
                throw new ArgumentException("Os valores de horizontal, vertical e diagonal precisam ser positivos.");
            }

            // Verificando se o UsuarioId é válido
            if (lente.UsuarioId == 0)
            {
                _logger.LogWarning("UsuarioId não informado ou é inválido.");
                throw new ArgumentException("O UsuarioId é obrigatório.");
            }

            // Buscar o usuário associado ao UsuarioId
            var usuario = await _usuarioRepository.GetByIdAsync(lente.UsuarioId);
            if (usuario == null)
            {
                _logger.LogWarning("Usuário não encontrado para o UsuarioId {UsuarioId}.", lente.UsuarioId);
                throw new Exception("Usuário não encontrado.");
            }

            lente.Usuario = usuario; // Associando o usuário à lente

            // Adiciona a lente no repositório
            await _lenteRepository.AddAsync(lente);
            _logger.LogInformation("Lente criada com sucesso: ID {Id}, Horizontal {Horizontal}, Vertical {Vertical}, Diagonal {Diagonal}.", lente.Id, lente.Horizontal, lente.Vertical, lente.Diagonal);
            return lente;
        }

        // Atualizar lente
        public async Task AtualizarLenteAsync(LenteModelo lenteAtualizada)
        {
            if (lenteAtualizada == null)
            {
                _logger.LogWarning("Tentativa de atualizar uma lente nula.");
                throw new ArgumentNullException(nameof(lenteAtualizada), "A lente não pode ser nula.");
            }

            var lenteExistente = await _lenteRepository.GetByIdAsync(lenteAtualizada.Id);
            if (lenteExistente == null)
            {
                _logger.LogWarning("Tentativa de atualizar uma lente inexistente com ID {Id}.", lenteAtualizada.Id);
                throw new LenteNotFoundException(lenteAtualizada.Id);
            }

            // Validação dos dados da lente (horizontal, vertical e diagonal devem ser positivos)
            if (lenteAtualizada.Horizontal <= 0 || lenteAtualizada.Vertical <= 0 || lenteAtualizada.Diagonal <= 0)
            {
                _logger.LogWarning("Tentativa de atualizar lente com valores inválidos: Horizontal={Horizontal}, Vertical={Vertical}, Diagonal={Diagonal}.", lenteAtualizada.Horizontal, lenteAtualizada.Vertical, lenteAtualizada.Diagonal);
                throw new ArgumentException("Os valores de horizontal, vertical e diagonal precisam ser positivos.");
            }

            // Verificando se o UsuarioId é válido
            if (lenteAtualizada.UsuarioId == 0)
            {
                _logger.LogWarning("UsuarioId não informado ou é inválido.");
                throw new ArgumentException("O UsuarioId é obrigatório.");
            }

            // Buscar o usuário associado ao UsuarioId
            var usuario = await _usuarioRepository.GetByIdAsync(lenteAtualizada.UsuarioId);
            if (usuario == null)
            {
                _logger.LogWarning("Usuário não encontrado para o UsuarioId {UsuarioId}.", lenteAtualizada.UsuarioId);
                throw new Exception("Usuário não encontrado.");
            }

            lenteExistente.Usuario = usuario; // Atualizando o usuário associado à lente

            // Atualiza os dados da lente
            lenteExistente.Horizontal = lenteAtualizada.Horizontal;
            lenteExistente.Vertical = lenteAtualizada.Vertical;
            lenteExistente.Diagonal = lenteAtualizada.Diagonal;

            await _lenteRepository.UpdateAsync(lenteExistente);
            _logger.LogInformation("Lente com ID {Id} atualizada com sucesso: Horizontal {Horizontal}, Vertical {Vertical}, Diagonal {Diagonal}.", lenteExistente.Id, lenteExistente.Horizontal, lenteExistente.Vertical, lenteExistente.Diagonal);
        }

        // Obter lente por ID
        public async Task<LenteModelo?> ObterLentePorIdAsync(int id)
        {
            var lente = await _lenteRepository.GetByIdAsync(id);
            if (lente == null)
            {
                _logger.LogWarning("Lente com ID {Id} não encontrada.", id);
                throw new LenteNotFoundException(id);
            }

            return lente;
        }

        // Excluir lente
        public async Task ExcluirLenteAsync(int id)
        {
            var lente = await _lenteRepository.GetByIdAsync(id);
            if (lente == null)
            {
                _logger.LogWarning("Tentativa de excluir uma lente inexistente com ID {Id}.", id);
                throw new LenteNotFoundException(id);
            }

            await _lenteRepository.RemoveAsync(lente);
            _logger.LogInformation("Lente com ID {Id} removida com sucesso.", id);
        }

        // Listar todas as lentes
        public async Task<IEnumerable<LenteModelo>> ListarLentesAsync()
        {
            return await _lenteRepository.GetAllAsync();
        }

        // Calcular formato da lente em raio (baseado nos dados horizontais, verticais e diagonais)
        public string GerarFormatoRaio(LenteModelo lente)
        {
            if (lente == null)
            {
                throw new ArgumentNullException(nameof(lente), "Lente não pode ser nula.");
            }

            // Lógica para gerar os valores de raio com base nos dados
            // Vamos apenas criar um formato fictício de exemplo.
            var formatoRaio = $"TRCFMT=1;360;E;R;F\nR={lente.Horizontal};{lente.Vertical};{lente.Diagonal}";

            return formatoRaio;
        }
    }
}
