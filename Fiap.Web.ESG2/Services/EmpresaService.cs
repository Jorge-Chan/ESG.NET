using Fiap.Web.ESG2.Data.Contexts;
using Fiap.Web.ESG2.Models;

namespace Fiap.Web.ESG2.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly DatabaseContext _context;

        public EmpresaService(DatabaseContext context)
        {
            _context = context;
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
