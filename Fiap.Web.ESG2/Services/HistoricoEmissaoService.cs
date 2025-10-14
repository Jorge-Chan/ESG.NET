using Fiap.Web.ESG2.Data.Contexts;
using Fiap.Web.ESG2.Models;
using Fiap.Web.ESG2.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Fiap.Web.ESG2.Services
{
    public class HistoricoEmissaoService : IHistoricoEmissaoService
    {
        private readonly DatabaseContext _context;

        public HistoricoEmissaoService(DatabaseContext context)
        {
            _context = context;
        }

        // NOVO
        public async Task<PagedResult<HistoricoEmissaoModel>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var query = _context.HistoricoEmissoes.AsNoTracking().OrderBy(h => h.Id);

            var total = await query.CountAsync(ct);
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return new PagedResult<HistoricoEmissaoModel>
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = total,
                Items = items
            };
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
