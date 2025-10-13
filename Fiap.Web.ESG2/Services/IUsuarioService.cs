using Fiap.Web.ESG2.Models;
using Fiap.Web.ESG2.ViewModels;

namespace Fiap.Web.ESG2.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioModel?> GetByEmailAsync(string email, CancellationToken ct = default);
        Task<UsuarioModel?> GetByIdAsync(long id, CancellationToken ct = default);

        // precisa do ViewModels/PagedResult.cs incluso no projeto
        Task<PagedResult<UsuarioModel>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default);

        Task<UsuarioModel> CreateAsync(string nome, string email, string senha, string role = "user", CancellationToken ct = default);
        Task<bool> UpdateAsync(long id, string nome, string role, bool ativo, CancellationToken ct = default);
        Task<bool> ChangePasswordAsync(long id, string senhaAtual, string novaSenha, CancellationToken ct = default);
        Task<bool> DeleteAsync(long id, CancellationToken ct = default);

        bool VerifyPassword(UsuarioModel user, string senha);
    }
}
