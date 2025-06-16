using Fiap.Web.ESG2.Data.Contexts;
using Fiap.Web.ESG2.Models;

namespace Fiap.Web.ESG2.Services
{
     public class HistoricoEmissaoService : IHistoricoEmissaoService
    {
        private readonly DatabaseContext _context;

        public HistoricoEmissaoService(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<HistoricoEmissaoModel> ListarHistoricos() => _context.HistoricoEmissoes.ToList();

        public HistoricoEmissaoModel ObterPorId(int id) => _context.HistoricoEmissoes.Find(id);

        public void Criar(HistoricoEmissaoModel historico)
        {
            _context.HistoricoEmissoes.Add(historico);
            _context.SaveChanges();
        }

        public void Atualizar(HistoricoEmissaoModel historico)
        {
            _context.HistoricoEmissoes.Update(historico);
            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            var historico = _context.HistoricoEmissoes.Find(id);
            if (historico != null)
            {
                _context.HistoricoEmissoes.Remove(historico);
                _context.SaveChanges();
            }
        }
    }
}
