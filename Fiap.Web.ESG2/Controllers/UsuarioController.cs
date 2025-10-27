using Fiap.Web.ESG2.Services;
using Fiap.Web.ESG2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

// Alias para o tipo paginado
using PagedUsuarios = Fiap.Web.ESG2.ViewModels.PagedResult<Fiap.Web.ESG2.Models.UsuarioModel>;

namespace Fiap.Web.ESG2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _users;

        public UsuariosController(IUsuarioService users) => _users = users;

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<PagedUsuarios>> Get(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            try
            {
                var result = await _users.GetPagedAsync(page, pageSize, ct);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        [HttpGet("{id:long}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Fiap.Web.ESG2.Models.UsuarioModel>> GetById(long id, CancellationToken ct = default)
        {
            try
            {
                var user = await _users.GetByIdAsync(id, ct);
                return user is null ? NotFound() : Ok(user);
            }
            catch
            {
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        [HttpPut("{id:long}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateUserRequest req, CancellationToken ct = default)
        {
            try
            {
                var ok = await _users.UpdateAsync(id, req.Nome, req.Role, req.Ativo, ct);
                return ok ? NoContent() : NotFound();
            }
            catch
            {
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        [HttpPut("{id:long}/password")]
        public async Task<IActionResult> ChangePassword(long id, [FromBody] ChangePasswordRequest req, CancellationToken ct = default)
        {
            try
            {
                var ok = await _users.ChangePasswordAsync(id, req.SenhaAtual, req.NovaSenha, ct);
                return ok ? NoContent() : Unauthorized();
            }
            catch
            {
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        [HttpDelete("{id:long}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(long id, CancellationToken ct = default)
        {
            try
            {
                var ok = await _users.DeleteAsync(id, ct);
                return ok ? NoContent() : NotFound();
            }
            catch
            {
                return StatusCode(500, "Erro interno no servidor");
            }
        }
    }
}
