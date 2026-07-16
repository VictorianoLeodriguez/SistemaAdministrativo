using SistemaAdm.Helpers;
using SistemaAdm.Models;
using SistemaAdm.database;
using SistemaAdm.Contracts;
using SistemaAdm.ViewModel;

namespace SistemaAdm.Service;

public class LoginService
{
    public static LoginResult Autenticar(string documento, string senha)
    {
        documento = documento.Replace(".", "").Replace("-", "").Replace("/", "").Trim();

        User userBanco = UserDB.BuscarUserAuth(documento);

        if (userBanco == null || !Utils.VerificarSenha(senha, userBanco.Senha))
        {
            return new LoginResult
            {
              Sucesso = false,
              Mensagem = "CPF/CNPJ ou Senha Inválidos"
            };
        }

        return new LoginResult
        {
            Sucesso = true
        };
    }
}
