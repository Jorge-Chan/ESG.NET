using Fiap.Web.ESG2.Data.Contexts;
using Fiap.Web.ESG2.Models;
using Fiap.Web.ESG2.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Fiap.Web.ESG2.Services
{
    public class CompensacaoCarbonoService : ICompensacaoCarbonoService
    {
        private readonly DatabaseContext _context;

        public CompensacaoCarbonoService(DatabaseContext context)
        {
            _context = context;
        }

        // NOVO
        public async Task<PagedResult<CompensacaoCarbonoModel>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var query = _context.CompensacoesCarbono.AsNoTracking().OrderBy(c => c.Id);

            var total = await query.CountAsync(ct);
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return new PagedResult<CompensacaoCarbonoModel>
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = total,
                Items = items
            };
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
