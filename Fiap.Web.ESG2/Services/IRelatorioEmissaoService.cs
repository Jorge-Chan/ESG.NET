using Fiap.Web.ESG2.Models;

namespace Fiap.Web.ESG2.Services
{
    public interface IRelatorioEmissaoService
    {
        IEnumerable<RelotorioEmissaoModel> ListarRelatorios();
        RelotorioEmissaoModel ObterPorId(int id);
        void Criar(RelotorioEmissaoModel relatorio);
        void Atualizar(RelotorioEmissaoModel relatorio);
        void Deletar(int id);
    }
}
