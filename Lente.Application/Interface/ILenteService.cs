using Lente.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Software.lentes.DTOs
{
    public interface ILenteService
    {
        // Criar uma nova lente
        Task<LenteModelo> CriarLenteAsync(LenteModelo lente);

        // Atualizar uma lente existente
        Task AtualizarLenteAsync(LenteModelo lenteAtualizada);

        // Obter uma lente por ID
        Task<LenteModelo?> ObterLentePorIdAsync(int id);

        // Excluir uma lente
        Task ExcluirLenteAsync(int id);

        // Listar todas as lentes
        Task<IEnumerable<LenteModelo>> ListarLentesAsync();

        // Gerar formato de raio com base nas dimensões da lente
        string GerarFormatoRaio(LenteModelo lente);
    }
}
