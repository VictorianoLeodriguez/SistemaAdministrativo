using System.ComponentModel.DataAnnotations;

namespace SistemaAdm.ViewModel;

public class CadastroModel
{
    [Required(ErrorMessage = "O campo Nome é Obrigatório")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo de Email é Obrigatório")]
    [EmailAddress(ErrorMessage = "Necessario um Endereço de email Valido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O Campo de CPF/CNPJ é Obrigatório")]
    public string CPFJ { get; set; }

    [Required(ErrorMessage = "O Campo Senha é Obrigatório")]
    public string Senha { get; set; }
}
