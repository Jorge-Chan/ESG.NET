using Fiap.Web.ESG2.Data.Contexts;
using Fiap.Web.ESG2.Models;
using Fiap.Web.ESG2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Web.ESG2.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly DatabaseContext _db;
        private readonly PasswordHasher<UsuarioModel> _hasher = new();

        public UsuarioService(DatabaseContext db) => _db = db;

        public async Task<UsuarioModel?> GetByEmailAsync(string email, CancellationToken ct = default)
        {
            return await _db.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email, ct);
        }

        public async Task<UsuarioModel?> GetByIdAsync(long id, CancellationToken ct = default)
        {
            return await _db.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id, ct);
        }

        public async Task<PagedResult<UsuarioModel>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default)
        {
            // normaliza parâmetros
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var query = _db.Usuarios.AsNoTracking().OrderBy(u => u.Id);

            var total = await query.CountAsync(ct);
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return new PagedResult<UsuarioModel>
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = total,
                Items = items
            };
        }

        public async Task<UsuarioModel> CreateAsync(string nome, string email, string senha, string role = "user", CancellationToken ct = default)
        {
            var user = new UsuarioModel { Nome = nome, Email = email, Role = role };
            user.SenhaHash = _hasher.HashPassword(user, senha);

            _db.Usuarios.Add(user);
            await _db.SaveChangesAsync(ct);
            return user;
        }

        public bool VerifyPassword(UsuarioModel user, string senha)
        {
            return _hasher.VerifyHashedPassword(user, user.SenhaHash, senha) == PasswordVerificationResult.Success;
        }

        public async Task<bool> UpdateAsync(long id, string nome, string role, bool ativo, CancellationToken ct = default)
        {
            var user = await _db.Usuarios.FirstOrDefaultAsync(u => u.Id == id, ct);
            if (user is null) return false;

            user.Nome = nome;
            user.Role = role;
            user.Ativo = ativo;

            await _db.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> ChangePasswordAsync(long id, string senhaAtual, string novaSenha, CancellationToken ct = default)
        {
            var user = await _db.Usuarios.FirstOrDefaultAsync(u => u.Id == id, ct);
            if (user is null) return false;

            if (!VerifyPassword(user, senhaAtual)) return false;

            user.SenhaHash = _hasher.HashPassword(user, novaSenha);
            await _db.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> DeleteAsync(long id, CancellationToken ct = default)
        {
            var user = await _db.Usuarios.FirstOrDefaultAsync(u => u.Id == id, ct);
            if (user is null) return false;

            _db.Usuarios.Remove(user);
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
