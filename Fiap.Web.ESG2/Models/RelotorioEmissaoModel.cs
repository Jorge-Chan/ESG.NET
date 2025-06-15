using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiap.Web.ESG2.Models
{
    [Table("relatorio_emissao")]
    public class RelotorioEmissaoModel
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("data_envio")]
        public DateTime DataEnvio { get; set; }

        [Column("quantidade_emissao")]
        public double QuantidadeEmissao { get; set; }

        [Column("arquivo_pdf_url")]
        public string ArquivoPdfUrl { get; set; }

        [ForeignKey("Empresa")]
        [Column("empresa_id")]
        public long EmpresaId { get; set; }

        public EmpresaModel Empresa { get; set; }
    }
}
