using Fiap.Web.ESG2.Data.Contexts;
using Fiap.Web.ESG2.Models;
using Fiap.Web.ESG2.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Fiap.Web.ESG2.Services
{
    public class RelatorioEmissaoService : IRelatorioEmissaoService
    {
        private readonly DatabaseContext _context;

        public RelatorioEmissaoService(DatabaseContext context)
        {
            _context = context;
        }

        // NOVO
        public async Task<PagedResult<RelatorioEmissaoModel>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var query = _context.RelatoriosEmissao
                .Include(r => r.Empresa)
                .AsNoTracking()
                .OrderBy(r => r.Id);

            var total = await query.CountAsync(ct);
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return new PagedResult<RelatorioEmissaoModel>
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = total,
                Items = items
            };
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
