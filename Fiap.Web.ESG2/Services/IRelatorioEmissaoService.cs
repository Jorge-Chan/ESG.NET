using Fiap.Web.ESG2.Models;

namespace Fiap.Web.ESG2.Services
{
    public interface IRelatorioEmissaoService
    {
        IEnumerable<RelatorioEmissaoModel> ListarRelatorios();
        RelatorioEmissaoModel? ObterPorId(int id);
        void Criar(RelatorioEmissaoModel relatorio);
        void Atualizar(RelatorioEmissaoModel relatorio);
        void Deletar(int id);
    }
}
