using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiap.Web.ESG2.Models
{
    [Table("compensacao_carbono")]
    public class CompensacaoCarbonoModel
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("tipo_iniciativa")]
        public string TipoIniciativa { get; set; }

        [Column("quantidade_compensada")]
        public double QuantidadeCompensada { get; set; }

        [Column("data_compensacao")]
        public DateTime DataCompensacao { get; set; }

        [ForeignKey("Empresa")]
        [Column("empresa_id")]
        public long EmpresaId { get; set; }

        public Empresa Empresa { get; set; }
    }
}
