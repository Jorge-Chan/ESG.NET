using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiap.Web.ESG2.Models
{
    [Table("usuario")]
    public class UsuarioModel
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required, MaxLength(120)]
        [Column("nome")]
        public string Nome { get; set; } = string.Empty;

        [Required, MaxLength(120), EmailAddress]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Column("senha_hash")]
        public string SenhaHash { get; set; } = string.Empty;

        [Required, MaxLength(30)]
        [Column("role")]
        public string Role { get; set; } = "user"; // ex.: user/admin

        [Column("ativo")]
        public bool Ativo { get; set; } = true;
    }
}
