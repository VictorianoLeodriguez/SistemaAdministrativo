namespace SistemaAdm.Models;
using  System.ComponentModel.DataAnnotations;

public class User
{
    public int Codigo {get;set;}
    public string RazaoSocial {get;set;}
    public string Senha {get;set;}
    public string Email {get;set;}
    public string CPFJ {get;set;}
    public bool Ativo {get;set;} = true;
}
