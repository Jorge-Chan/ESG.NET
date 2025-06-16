using Fiap.Web.ESG2.Data;
using Fiap.Web.ESG2.Data.Contexts;
using Fiap.Web.ESG2.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Web.ESG2.Data.Repository
{
    public class EmpresaRepository
    {
        private readonly DatabaseContext _context;

        public EmpresaRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<EmpresaModel> GetAll() => _context.Empresas.ToList();

        public EmpresaModel GetById(long id) => _context.Empresas.Find(id);

        public void Add(EmpresaModel empresa)
        {
            _context.Empresas.Add(empresa);
            _context.SaveChanges();
        }

        public void Update(EmpresaModel empresa)
        {
            _context.Update(empresa);
            _context.SaveChanges();
        }

        public void Delete(EmpresaModel empresa)
        {
            _context.Empresas.Remove(empresa);
            _context.SaveChanges();
        }
    }
}
