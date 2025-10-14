using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fiap.Web.ESG2.Models;
using Fiap.Web.ESG2.ViewModels;

namespace Fiap.Web.ESG2.Services
{
    public interface IHistoricoEmissaoService
    {
        // Novo contrato paginado
        Task<PagedResult<HistoricoEmissaoModel>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default);

        // Mantidos (legado)
        IEnumerable<HistoricoEmissaoModel> ListarHistoricos();
        HistoricoEmissaoModel? ObterPorId(int id);
        void Criar(HistoricoEmissaoModel historico);
        void Atualizar(HistoricoEmissaoModel historico);
        void Deletar(int id);
    }
}
