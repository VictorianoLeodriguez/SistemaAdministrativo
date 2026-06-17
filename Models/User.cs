namespace SistemaAdm.Models;
using  System.ComponentModel.DataAnnotations;

public class User
{
    public int Codigo {get;set;}

    [Required(ErrorMessage = "O campo nome é obrigatório")]
    public string Nome {get;set;}

    [Required(ErrorMessage = "O campo senha é obrigatório")]
    public string Senha {get;set;}

    [Required(ErrorMessage = "O campo email é obrigatório")]
    [EmailAddress(ErrorMessage = "O campo email deve conter um endereço de email válido")]
    public string Email {get;set;}

    [Required(ErrorMessage = "O campo de Documento (CPF/CNPJ) é obrigatório")]
    public string CPFJ {get;set;}

    public bool Ativo {get;set;}
}
