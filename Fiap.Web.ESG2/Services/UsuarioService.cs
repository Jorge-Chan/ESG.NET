using Fiap.Web.ESG2.Data.Contexts;
using Fiap.Web.ESG2.Models;

namespace Fiap.Web.ESG2.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly DatabaseContext _context;

        public UsuarioService(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<UsuarioModel> ListarUsuarios() => _context.Usuarios.ToList();

        public UsuarioModel ObterPorId(int id) => _context.Usuarios.Find(id);

        public void Criar(UsuarioModel usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void Atualizar(UsuarioModel usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
            }
        }
    }







}
