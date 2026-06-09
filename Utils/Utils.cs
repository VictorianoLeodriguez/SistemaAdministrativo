using System.Net.Mail;
using BCrypt.Net;

namespace SistemaAdm.Utils;

public class Utils
{
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

    public static string HashSenha(string senha)
    {
        return BCrypt.Net.BCrypt.HashPassword(senha);
    }

    public static bool VerificarSenha (string senha, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(senha, hash);
    }
}
