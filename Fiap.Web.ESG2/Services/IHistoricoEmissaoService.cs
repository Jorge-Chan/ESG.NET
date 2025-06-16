using Fiap.Web.ESG2.Models;

namespace Fiap.Web.ESG2.Services
{
    public interface IHistoricoEmissaoService
    {
        IEnumerable<HistoricoEmissaoModel> ListarHistoricos();
        HistoricoEmissaoModel ObterPorId(int id);
        void Criar(HistoricoEmissaoModel historico);
        void Atualizar(HistoricoEmissaoModel historico);
        void Deletar(int id);
    }
}
