using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fiap.Web.ESG2.Models;
using Fiap.Web.ESG2.ViewModels;

namespace Fiap.Web.ESG2.Services
{
    public interface IEmpresaService
    {
        // Novo contrato paginado
        Task<PagedResult<EmpresaModel>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default);

        // Mantidos (legado)
        IEnumerable<EmpresaModel> ListarEmpresas();
        EmpresaModel ObterEmpresaPorId(int id);
        void CriarEmpresa(EmpresaModel empresa);
        void AtualizarEmpresa(EmpresaModel empresa);
        void DeletarEmpresa(int id);
    }
}
