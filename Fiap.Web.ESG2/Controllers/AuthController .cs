using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Fiap.Web.ESG2.Models;
using Fiap.Web.ESG2.Services;
using Fiap.Web.ESG2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// Aliases para evitar ambiguidade com Microsoft.AspNetCore.Identity.Data
using LoginRequest = Fiap.Web.ESG2.ViewModels.LoginRequest;
using RegisterRequest = Fiap.Web.ESG2.ViewModels.RegisterRequest;
using AuthResponse = Fiap.Web.ESG2.ViewModels.AuthResponse;

namespace Fiap.Web.ESG2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _users;
        private readonly IConfiguration _cfg;

        public AuthController(IUsuarioService users, IConfiguration cfg)
        {
            _users = users;
            _cfg = cfg;
        }

        [HttpPost("register")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest req, CancellationToken ct)
        {
            var existing = await _users.GetByEmailAsync(req.Email, ct);
            if (existing != null) return Conflict("E-mail já cadastrado.");

            var user = await _users.CreateAsync(req.Nome, req.Email, req.Senha, req.Role, ct);
            return Ok(new AuthResponse { Token = GenerateToken(user), Nome = user.Nome, Role = user.Role });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest req, CancellationToken ct)
        {
            var user = await _users.GetByEmailAsync(req.Email, ct);
            if (user is null || !_users.VerifyPassword(user, req.Senha) || !user.Ativo)
                return Unauthorized();

            return Ok(new AuthResponse { Token = GenerateToken(user), Nome = user.Nome, Role = user.Role });
        }

        private string GenerateToken(UsuarioModel user)
        {
            var section = _cfg.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(section["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Nome),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: section["Issuer"],
                audience: section["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
