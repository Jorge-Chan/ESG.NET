using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fiap.Web.ESG2.Models;
using Fiap.Web.ESG2.ViewModels;

namespace Fiap.Web.ESG2.Services
{
    public interface IRelatorioEmissaoService
    {
        // Novo contrato paginado (usado pelos testes e pelos controllers Get(page, pageSize))
        Task<PagedResult<RelatorioEmissaoModel>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default);

        // Mantidos (legado) — pode remover depois se não usar mais
        IEnumerable<RelatorioEmissaoModel> ListarRelatorios();
        RelatorioEmissaoModel? ObterPorId(int id);
        void Criar(RelatorioEmissaoModel relatorio);
        void Atualizar(RelatorioEmissaoModel relatorio);
        void Deletar(int id);
    }
}
