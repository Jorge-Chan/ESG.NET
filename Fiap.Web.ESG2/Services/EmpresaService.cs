using Fiap.Web.ESG2.Data.Contexts;
using Fiap.Web.ESG2.Models;
using Fiap.Web.ESG2.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Fiap.Web.ESG2.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly DatabaseContext _context;

        public EmpresaService(DatabaseContext context)
        {
            _context = context;
        }

        // NOVO
        public async Task<PagedResult<EmpresaModel>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var query = _context.Empresas.AsNoTracking().OrderBy(e => e.Id);

            var total = await query.CountAsync(ct);
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return new PagedResult<EmpresaModel>
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = total,
                Items = items
            };
        }

        public IEnumerable<EmpresaModel> ListarEmpresas() => _context.Empresas.ToList();

        public EmpresaModel ObterEmpresaPorId(int id) => _context.Empresas.Find(id);

        public void CriarEmpresa(EmpresaModel empresa)
        {
            _context.Empresas.Add(empresa);
            _context.SaveChanges();
        }

        public void AtualizarEmpresa(EmpresaModel empresa)
        {
            _context.Empresas.Update(empresa);
            _context.SaveChanges();
        }

        public void DeletarEmpresa(int id)
        {
            var empresa = _context.Empresas.Find(id);
            if (empresa != null)
            {
                _context.Empresas.Remove(empresa);
                _context.SaveChanges();
            }
        }
    }
}
