namespace SistemaAdm.Models;
using  System.ComponentModel.DataAnnotations;

public class User
{
    public int Codigo {get;set;}

    [Required(ErrorMessage = "O campo nome é obrigatório")]
    public string nome {get;set;}

    [Required(ErrorMessage = "O campo senha é obrigatório")]
    public string senha {get;set;}

    [Required(ErrorMessage = "O campo email é obrigatório")]
    [EmailAddress(ErrorMessage = "O campo email deve conter um endereço de email válido")]
    public string Email {get;set;}

    [Required(ErrorMessage = "O campo CPF é obrigatório")]
    [StringLength(11, ErrorMessage = "O campo CPF deve conter exatamente 11 caracteres")]
    public string CPF {get;set;}

    public bool Ativo {get;set;}
}
