using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiap.Web.ESG2.Models
{
    [Table("empresa")]
    public class EmpresaModel
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("nome")]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [Column("cnpj")]
        public string Cnpj { get; set; } = string.Empty;

        [Required]
        [Column("setor")]
        public string Setor { get; set; } = string.Empty;

        // Se usar lazy loading, pode marcar como "virtual"
        public ICollection<CompensacaoCarbonoModel> CompensacoesCarbono { get; set; } =
            new List<CompensacaoCarbonoModel>();

        public ICollection<HistoricoEmissaoModel> HistoricoEmissoes { get; set; } =
            new List<HistoricoEmissaoModel>();

        // ATENÇÃO: se sua classe se chama "RelatorioEmissaoModel" (sem 't'),
        // ajuste o tipo abaixo e os lugares onde é usada.
        public ICollection<RelatorioEmissaoModel> RelatoriosEmissao { get; set; } =
            new List<RelatorioEmissaoModel>();
    }
}
