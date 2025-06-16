using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiap.Web.ESG2.Models
{
    [Table("historico_emissoes")]
    public class HistoricoEmissaoModel
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("ano")]
        public DateTime Ano { get; set; }

        [Column("total_emitido_ton_co2")]
        public double TotalEmitidoTonCO2 { get; set; }

        [Column("empresa_id")]
        public long EmpresaId { get; set; }

        [ForeignKey("EmpresaId")]
        public EmpresaModel Empresa { get; set; }
    }
}
