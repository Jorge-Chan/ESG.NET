using System.ComponentModel.DataAnnotations;

namespace Fiap.Web.ESG2.ViewModels
{
    public class EmpresaViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "O nome da empresa é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CNPJ é obrigatório")]
        public string Cnpj { get; set; }

        public string Setor { get; set; }
    }
}
