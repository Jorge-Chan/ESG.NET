using Fiap.Web.ESG2.Data;
using Fiap.Web.ESG2.Data.Contexts;
using Fiap.Web.ESG2.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Web.ESG2.Data.Repository
{
    public class RelatorioEmissaoRepository
    {
        private readonly DatabaseContext _context;

        public RelatorioEmissaoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<RelatorioEmissaoModel> GetAll() => _context.RelatoriosEmissao.Include(r => r.Empresa).ToList();

        public RelatorioEmissaoModel GetById(long id) => _context.RelatoriosEmissao.Find(id);

        public void Add(RelatorioEmissaoModel relatorio)
        {
            _context.RelatoriosEmissao.Add(relatorio);
            _context.SaveChanges();
        }

        public void Update(RelatorioEmissaoModel relatorio)
        {
            _context.Update(relatorio);
            _context.SaveChanges();
        }

        public void Delete(RelatorioEmissaoModel relatorio)
        {
            _context.RelatoriosEmissao.Remove(relatorio);
            _context.SaveChanges();
        }
    }
}
