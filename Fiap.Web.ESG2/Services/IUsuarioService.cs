using Fiap.Web.ESG2.Models;

namespace Fiap.Web.ESG2.Services
{
    public interface IUsuarioService
    {
        IEnumerable<UsuarioModel> ListarUsuarios();
        UsuarioModel ObterPorId(int id);
        void Criar(UsuarioModel usuario);
        void Atualizar(UsuarioModel usuario);
        void Deletar(int id);
    }
}
