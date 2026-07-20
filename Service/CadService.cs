using SistemaAdm.Contracts;
using SistemaAdm.database;
using SistemaAdm.Helpers;
using SistemaAdm.Models;

namespace SistemaAdm.Service;

public class CadService
{
    public static CadastroResult SalvarUser(User user)
    {
        if (!Utils.ValidarDocumento(user.CNPJ))
        {
            return new CadastroResult
            {
                Sucesso = false,
                Mensagem = "CNPJ inválido."
            };
        }

        if (!Utils.ValidarEmail(user.Email))
        {
            return new CadastroResult
            {
                Sucesso = false,
                Mensagem = "E-mail inválido."
            };
        }

        User usuarioExistente = UserDB.ExisteUser(user.Email, user.CNPJ);

        if (usuarioExistente != null)
        {
            return new CadastroResult
            {
                Sucesso = false,
                Mensagem = "E-mail ou CNPJ já cadastrado."
            };
        }

        user.Senha = Utils.HashSenha(user.Senha);

        UserDB.CadastrarUser(user, out string errorMsg);

        if (string.IsNullOrEmpty(errorMsg))
        {
            return new CadastroResult
            {
                Sucesso = false,
                Mensagem = "Erro ao cadastrar usuário: " + errorMsg
            };
        }

        return new CadastroResult
        {
            Sucesso = true,
            Mensagem = "Usuário cadastrado com sucesso."
        };
    }
}