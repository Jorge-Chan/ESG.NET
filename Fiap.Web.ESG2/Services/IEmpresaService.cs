using Fiap.Web.ESG2.Models;

namespace Fiap.Web.ESG2.Services
{
    public interface IEmpresaService
    {
        IEnumerable<EmpresaModel> ListarEmpresas();
        EmpresaModel ObterEmpresaPorId(int id);
        void CriarEmpresa(EmpresaModel empresa);
        void AtualizarEmpresa(EmpresaModel empresa);
        void DeletarEmpresa(int id);
    }
}
