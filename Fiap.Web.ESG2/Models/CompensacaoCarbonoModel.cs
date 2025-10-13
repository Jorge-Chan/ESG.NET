using System;
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

        [Required]
        [Column("tipo_iniciativa")]
        public string TipoIniciativa { get; set; } = string.Empty;

        [Column("quantidade_compensada")]
        public double QuantidadeCompensada { get; set; }

        [Column("data_compensacao")]
        public DateTime DataCompensacao { get; set; }

        [Column("empresa_id")]
        public long EmpresaId { get; set; }   // FK

        [ForeignKey(nameof(EmpresaId))]
        public EmpresaModel? Empresa { get; set; }  // Propriedade de navegação (NÃO chame de "EmpresaModel")
    }
}
