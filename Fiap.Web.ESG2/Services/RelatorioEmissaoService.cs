using Fiap.Web.ESG2.Data.Contexts;
using Fiap.Web.ESG2.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Web.ESG2.Services
{
    public class RelatorioEmissaoService : IRelatorioEmissaoService
    {
        private readonly DatabaseContext _context;

        public RelatorioEmissaoService(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<RelatorioEmissaoModel> ListarRelatorios()
        {
            return _context.RelatoriosEmissao
                .Include(r => r.Empresa)
                .AsNoTracking()
                .ToList();
        }

        public RelatorioEmissaoModel? ObterPorId(int id)
        {
            return _context.RelatoriosEmissao
                .Include(r => r.Empresa)
                .AsNoTracking()
                .FirstOrDefault(r => r.Id == id);
        }

        public void Criar(RelatorioEmissaoModel relatorio)
        {
            _context.RelatoriosEmissao.Add(relatorio);
            _context.SaveChanges();
        }

        public void Atualizar(RelatorioEmissaoModel relatorio)
        {
            _context.RelatoriosEmissao.Update(relatorio);
            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            var relatorio = _context.RelatoriosEmissao.Find(id);
            if (relatorio != null)
            {
                _context.RelatoriosEmissao.Remove(relatorio);
                _context.SaveChanges();
            }
        }
    }
}
