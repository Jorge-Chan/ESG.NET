using System;
using System.ComponentModel.DataAnnotations;

namespace Fiap.Web.ESG2.ViewModels
{
    public class HistoricoEmissoesViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Ano")]
        [DataType(DataType.Date)]
        public DateTime Ano { get; set; }

        [Display(Name = "Total Emitido (Ton CO2)")]
        public double TotalEmitidoTonCo2 { get; set; }

        public long EmpresaId { get; set; }
    }
}
