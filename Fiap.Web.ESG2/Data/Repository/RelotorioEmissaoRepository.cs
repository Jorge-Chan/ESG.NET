using Fiap.Web.ESG2.Data;
using Fiap.Web.ESG2.Data.Contexts;
using Fiap.Web.ESG2.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Web.ESG2.Data.Repository
{
    public class RelotorioEmissaoRepository
    {
        private readonly DatabaseContext _context;

        public RelotorioEmissaoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<RelotorioEmissaoModel> GetAll() => _context.RelatoriosEmissao.Include(r => r.Empresa).ToList();

        public RelotorioEmissaoModel GetById(long id) => _context.RelatoriosEmissao.Find(id);

        public void Add(RelotorioEmissaoModel relatorio)
        {
            _context.RelatoriosEmissao.Add(relatorio);
            _context.SaveChanges();
        }

        public void Update(RelotorioEmissaoModel relatorio)
        {
            _context.Update(relatorio);
            _context.SaveChanges();
        }

        public void Delete(RelotorioEmissaoModel relatorio)
        {
            _context.RelatoriosEmissao.Remove(relatorio);
            _context.SaveChanges();
        }
    }
}
