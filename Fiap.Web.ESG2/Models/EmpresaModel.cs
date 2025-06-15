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

        [Column("nome")]
        public string Nome { get; set; }

        [Column("cnpj")]
        public string Cnpj { get; set; }

        [Column("setor")]
        public string Setor { get; set; }

        public ICollection<CompensacaoCarbonoModel> CompensacoesCarbono { get; set; }
        public ICollection<HistoricoEmissoesModel> HistoricoEmissoes { get; set; }
        public ICollection<RelotorioEmissaoModel> RelatoriosEmissao { get; set; }
    }
}
    }
}
