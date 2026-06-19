using System.ComponentModel.DataAnnotations;

namespace SistemaAdm.ViewModel;

public class LoginModel
{
    [Required(ErrorMessage= "CPF/CNPJ Obrigatorio")]
    public string CPFJ {get;set;}

    [Required(ErrorMessage ="Senha Obrigatoria")]
    public string Senha {get;set;}
}
