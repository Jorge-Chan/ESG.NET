using System;
using System.ComponentModel.DataAnnotations;

namespace Fiap.Web.ESG2.ViewModels
{
    public class RelatorioEmissaoViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Data de Envio")]
        [DataType(DataType.Date)]
        public DateTime DataEnvio { get; set; }

        [Display(Name = "Quantidade de Emissão")]
        public double QuantidadeEmissao { get; set; }

        [Display(Name = "URL do Arquivo PDF")]
        public string ArquivoPdfUrl { get; set; }

        public long EmpresaId { get; set; }
    }
}
