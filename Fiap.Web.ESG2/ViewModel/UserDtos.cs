namespace Fiap.Web.ESG2.ViewModels
{
    public class UpdateUserRequest
    {
        public string Nome { get; set; } = string.Empty;
        public string Role { get; set; } = "user";
        public bool Ativo { get; set; } = true;
    }

    public class ChangePasswordRequest
    {
        public string SenhaAtual { get; set; } = string.Empty;
        public string NovaSenha { get; set; } = string.Empty;
    }
}
