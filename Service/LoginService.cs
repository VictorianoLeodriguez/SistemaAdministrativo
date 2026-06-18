using SistemaAdm.Helpers;
using SistemaAdm.Models;
using SistemaAdm.database;
using SistemaAdm.Contracts;

namespace SistemaAdm.Service;

public class LoginService
{
    public static LoginResult Autenticar(string documento, string senha)
    {
        if (!Utils.ValidarDocumento(documento))
        {
            return new LoginResult
            {
                Sucesso = false,
                Mensagem = documento.Length >= 11 ? "Cpf Inválido" : "CNPJ Inválido"
            };
        }

        User userBanco = UserDB.Authenticar(documento);

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
