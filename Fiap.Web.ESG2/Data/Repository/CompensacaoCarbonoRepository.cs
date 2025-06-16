using Fiap.Web.ESG2.Data;
using Fiap.Web.ESG2.Data.Contexts;
using Fiap.Web.ESG2.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Web.ESG2.Data.Repository
{
    public class CompensacaoCarbonoRepository
    {
        private readonly DatabaseContext _context;

        public CompensacaoCarbonoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<CompensacaoCarbonoModel> GetAll() => _context.CompensacoesCarbono.Include(c => c.EmpresaModel).ToList();

        public CompensacaoCarbonoModel GetById(long id) => _context.CompensacoesCarbono.Find(id);

        public void Add(CompensacaoCarbonoModel compensacao)
        {
            _context.CompensacoesCarbono.Add(compensacao);
            _context.SaveChanges();
        }

        public void Update(CompensacaoCarbonoModel compensacao)
        {
            _context.Update(compensacao);
            _context.SaveChanges();
        }

        public void Delete(CompensacaoCarbonoModel compensacao)
        {
            _context.CompensacoesCarbono.Remove(compensacao);
            _context.SaveChanges();
        }
    }
}
