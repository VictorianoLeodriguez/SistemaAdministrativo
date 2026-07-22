using System.ComponentModel.DataAnnotations;

namespace SistemaAdm.ViewModel;

public class CadastroModel
{
    [Required(ErrorMessage = "O campo Razão Social é Obrigatório")]
    [Display(Name = "Razão Social")]
    public string RazaoSocial { get; set; }

    [Required(ErrorMessage = "O campo Nome Fantasia é Obrigatório")]
    [Display(Name = "Nome Fantasia")]
    public string NomeFantasia { get; set; }

    [Required(ErrorMessage = "O Campo de Email é Obrigatório")]
    [EmailAddress(ErrorMessage = "Necessario um Endereço de email Valido")]
    [Display(Name = "E-Mail")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O Campo de CNPJ é Obrigatório")]
    public string CNPJ { get; set; }

    [Required(ErrorMessage = "O Campo CEP é Obrigatório")]
    public string CEP { get; set; }

    [Required(ErrorMessage = "O campo Logradouro é Obrigatório")]
    public string Logradouro { get; set; }

    [Required(ErrorMessage = "O campo Número é Obrigatório")]
    [Display(Name = "Número")]
    public string Numero { get; set; }
    public string Complemento { get; set; }

    [Required(ErrorMessage = "O campo Bairro é Obrigatório")]
    public string Bairro { get; set; }

    [Required(ErrorMessage = "O campo Cidade é Obrigatório")]
    public string Cidade { get; set; }

    [Required(ErrorMessage = "O campo Estado é Obrigatório")]
    public string Estado { get; set; }

    [Required(ErrorMessage = "O Campo Telefone é Obrigatório")]
    public string Telefone { get; set; }

    [Required(ErrorMessage = "O Campo Celular é Obrigatório")]
    public string Celular { get; set; }

    [Required(ErrorMessage = "O campo Nome do Responsável é Obrigatório")]
    [Display(Name = "Nome do Responsável")]
    public string NomeResponsavel { get; set; }

    public string Site { get; set; }

    [Required(ErrorMessage = "O campo Data de Fundação é obrigatório.")]
    [Display(Name = "Data de Fundação")]
    [DataType(DataType.Date)]
    public DateTime? DataFundacao { get; set; }

    [Required(ErrorMessage = "O Campo Senha é Obrigatório")]
    public string Senha { get; set; }
}