using System.Net.Mail;
using SistemaAdm.Models;

namespace SistemaAdm.Helpers;

public class Utils
{

    #region Helpers
    public static DateTime GetCurrentTime()
    {
        return DateTime.Now;
    }

    public static TimeSpan GetTimeOfDay()
    {
        return DateTime.Now.TimeOfDay;
    }

    public static DateTime GetcurentDate()
    {
        return DateTime.Now.Date;
    }

    public static string FormatDate(DateTime date)
    {
        return date.ToString("dd/MM/yyyy");
    }

    public static string FormatTime(TimeSpan time)
    {
        return time.ToString("hh\\:mm\\:ss");   
    }

    public static bool ValidarEmail (string email)
    {
        try
        {
            var EnderecoEmail = new MailAddress(email);
            return EnderecoEmail.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public static bool ValidarCpf (string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return false;        
        
        cpf = cpf.Replace(".","").Replace("-","").Trim();

        if(cpf.Length != 11)
            return false;
        
        //Muliplicadores dos 9 Primeiros Numeros
        int[] multiplicador1 = {10,9,8,7,6,5,4,3,2};
        
        //Multiplicador dos 2 Numeros Finais
        int[] multiplicador2 = {11,10,9,8,7,6,5,4,3,2};

        string rangeCpf = cpf.Substring(0,9);
        int soma = 0;

        for (int i=0; i < 9; i++)
            soma += (rangeCpf[i] - '0') * multiplicador1[i];
        
        int resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        string digito = resto.ToString();

        rangeCpf += digito;
        soma = 0;

        for(int i=0; i < 10; i++)
            soma += (rangeCpf[i] - '0') * multiplicador2[i];

        int resto2 = soma % 11;
            resto2 = resto2 < 2 ? 0 : 11 - resto2;

        digito += resto2.ToString();

        return cpf.EndsWith(digito);
    }

    public static bool ValidarCnpj(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj))
            return false;

        cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "").Trim();

        if (cnpj.Length != 14)
            return false;

        if (cnpj.Distinct().Count() == 1)
            return false;

        int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCnpj = cnpj.Substring(0, 12);

        int soma = 0;
        for (int i = 0; i < 12; i++)
            soma += (tempCnpj[i] - '0') * multiplicador1[i];

        int resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        string digito = resto.ToString();

        tempCnpj += digito;

        soma = 0;
        for (int i = 0; i < 13; i++)
            soma += (tempCnpj[i] - '0') * multiplicador2[i];

        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        digito += resto.ToString();

        return cnpj.EndsWith(digito);
    }

public static bool ValidarDocumento(string documento)
    {
        documento = documento.Replace(".", "").Replace("-", "").Replace("/", "").Trim();

        if (documento.Length == 11)
            return ValidarCpf(documento);

        if (documento.Length == 14)
            return ValidarCnpj(documento);

        return false;
    }

    public static string HashSenha(string senha)
    {
        return BCrypt.Net.BCrypt.HashPassword(senha);
    }

    public static bool VerificarSenha (string senha, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(senha, hash);
    }
    #endregion

    #region Status

    public enum StatusEmpresa
    {
        Ativo = 1,
        Inativo = 2,
        Pendente = 3,
        EmAnalise = 4,
        Bloqueado = 5
    }

    public static List<CodigoNome> GetStatusEmpresa()
    {
        return new List<CodigoNome>
        {
            new CodigoNome { Codigo = (int)StatusEmpresa.Ativo, Nome = "Ativo" },
            new CodigoNome { Codigo = (int)StatusEmpresa.Inativo, Nome = "Inativo" },
            new CodigoNome { Codigo = (int)StatusEmpresa.Pendente, Nome = "Pendente" },
            new CodigoNome { Codigo = (int)StatusEmpresa.EmAnalise, Nome = "Em Análise" },
            new CodigoNome { Codigo = (int)StatusEmpresa.Bloqueado, Nome = "Bloqueado" }
        };
    }

    #endregion
}   
    