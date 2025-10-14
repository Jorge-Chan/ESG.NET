using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fiap.Web.ESG2.Models;
using Fiap.Web.ESG2.ViewModels;

namespace Fiap.Web.ESG2.Services
{
    public interface ICompensacaoCarbonoService
    {
        // Novo contrato paginado
        Task<PagedResult<CompensacaoCarbonoModel>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default);

        // Mantidos (legado)
        IEnumerable<CompensacaoCarbonoModel> ListarCompensacoes();
        CompensacaoCarbonoModel ObterPorId(int id);
        void Criar(CompensacaoCarbonoModel compensacao);
        void Atualizar(CompensacaoCarbonoModel compensacao);
        void Deletar(int id);
    }
}
