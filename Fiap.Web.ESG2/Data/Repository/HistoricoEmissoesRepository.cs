using Fiap.Web.ESG2.Data;
using Fiap.Web.ESG2.Data.Contexts;
using Fiap.Web.ESG2.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Web.ESG2.Data.Repository
{
    public class HistoricoEmissoesRepository
    {
        private readonly DatabaseContext _context;

        public HistoricoEmissoesRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<HistoricoEmissaoModel> GetAll() => _context.HistoricoEmissoes.Include(h => h.Empresa).ToList();

        public HistoricoEmissaoModel GetById(long id) => _context.HistoricoEmissoes.Find(id);

        public void Add(HistoricoEmissaoModel historico)
        {
            _context.HistoricoEmissoes.Add(historico);
            _context.SaveChanges();
        }

        public void Update(HistoricoEmissaoModel historico)
        {
            _context.Update(historico);
            _context.SaveChanges();
        }

        public void Delete(HistoricoEmissaoModel historico)
        {
            _context.HistoricoEmissoes.Remove(historico);
            _context.SaveChanges();
        }
    }
}
