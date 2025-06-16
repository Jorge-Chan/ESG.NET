using Fiap.Web.ESG2.Models;

namespace Fiap.Web.ESG2.Services
{
    public interface ICompensacaoCarbonoService
    {
        IEnumerable<CompensacaoCarbonoModel> ListarCompensacoes();
        CompensacaoCarbonoModel ObterPorId(int id);
        void Criar(CompensacaoCarbonoModel compensacao);
        void Atualizar(CompensacaoCarbonoModel compensacao);
        void Deletar(int id);
    }
}
