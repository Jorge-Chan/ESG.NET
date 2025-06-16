using Fiap.Web.ESG2.Data.Contexts;
using Fiap.Web.ESG2.Models;

namespace Fiap.Web.ESG2.Services
{
    public class CompensacaoCarbonoService : ICompensacaoCarbonoService
    {
        private readonly DatabaseContext _context;

        public CompensacaoCarbonoService(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<CompensacaoCarbonoModel> ListarCompensacoes() => _context.CompensacoesCarbono.ToList();

        public CompensacaoCarbonoModel ObterPorId(int id) => _context.CompensacoesCarbono.Find(id);

        public void Criar(CompensacaoCarbonoModel compensacao)
        {
            _context.CompensacoesCarbono.Add(compensacao);
            _context.SaveChanges();
        }

        public void Atualizar(CompensacaoCarbonoModel compensacao)
        {
            _context.CompensacoesCarbono.Update(compensacao);
            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            var compensacao = _context.CompensacoesCarbono.Find(id);
            if (compensacao != null)
            {
                _context.CompensacoesCarbono.Remove(compensacao);
                _context.SaveChanges();
            }
        }
    }
}
