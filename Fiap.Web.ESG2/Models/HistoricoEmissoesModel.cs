using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiap.Web.ESG2.Models
{
    [Table("historico_emissoes")]
    public class HistoricoEmissoesModel
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("ano")]
        public DateTime Ano { get; set; }

        [Column("total_emitido_ton_co2")]
        public double TotalEmitidoTonCo2 { get; set; }

        [ForeignKey("Empresa")]
        [Column("empresa_id")]
        public long EmpresaId { get; set; }

        public EmpresaModel EmpresaModel { get; set; }
    }
}
