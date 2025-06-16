using System;
using System.ComponentModel.DataAnnotations;

namespace Fiap.Web.ESG2.ViewModels
{
    public class CompensacaoCarbonoViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "O tipo de iniciativa é obrigatório")]
        public string TipoIniciativa { get; set; }

        [Display(Name = "Quantidade Compensada (tCO2)")]
        public double QuantidadeCompensada { get; set; }

        [Display(Name = "Data da Compensação")]
        public DateTime DataCompensacao { get; set; }

        [Required(ErrorMessage = "Empresa é obrigatória")]
        public long EmpresaId { get; set; }
    }
}
