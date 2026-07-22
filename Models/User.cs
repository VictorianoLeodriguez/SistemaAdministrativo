namespace SistemaAdm.Models;
using  System.ComponentModel.DataAnnotations;

public class User
{
    public int Codigo { get; set; }
    public string RazaoSocial { get; set; }
    public string NomeFantasia { get; set; }
    public DateTime DataFundacao { get; set; }
    public string Situacao { get; set; }
    public string Site { get; set; }
    public string CEP { get; set; }
    public string Logradouro { get; set; }
    public string Numero { get; set; }
    public string Complemento { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public string Telefone { get; set; }
    public string Celular { get; set; }
    public string Email { get; set; }
    public string NomeResponsavel { get; set; }
    public string CNPJ { get; set; }
    public string Senha { get; set; }
    public bool Ativo { get; set; } = true;
    public DateTime DataCadastro { get; set; } = DateTime.Now;
}
