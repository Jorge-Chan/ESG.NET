using Fiap.Web.ESG2.Data.Contexts;
using Fiap.Web.ESG2.Models;

namespace Fiap.Web.ESG2.Services
{
    public class RelatorioEmissaoService : IRelatorioEmissaoService
    {
        private readonly DatabaseContext _context;

        public RelatorioEmissaoService(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<RelotorioEmissaoModel> ListarRelatorios() => _context.RelatoriosEmissao.ToList();

        public RelotorioEmissaoModel ObterPorId(int id) => _context.RelatoriosEmissao.Find(id);

        public void Criar(RelotorioEmissaoModel relatorio)
        {
            _context.RelatoriosEmissao.Add(relatorio);
            _context.SaveChanges();
        }

        public void Atualizar(RelotorioEmissaoModel relatorio)
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
