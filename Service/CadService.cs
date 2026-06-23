using SistemaAdm.Contracts;
using SistemaAdm.database;
using SistemaAdm.Helpers;
using SistemaAdm.Models;
using SistemaAdm.ViewModel;

namespace SistemaAdm.Service;

public class CadService
{
    public static CadastroResult SalvarUser(string cpfj, string email, string nome, string senha)
    {
        if (!Utils.ValidarDocumento(cpfj))
        {
            return new CadastroResult
            {
                Sucesso = false,
                Mensagem = cpfj.Length == 11 ? "CPF Inválido" : "CNPJ Inválido"
            };
        }

        if (!Utils.ValidarEmail(email))
        {
            return new CadastroResult
            {
                Sucesso = false,
                Mensagem = "Email Inválido",
            };
        }

        User userExiste = UserDB.ExisteUser(email, cpfj);
        
        if(userExiste != null)
        {
            return new CadastroResult
            {
                Sucesso = false,
                Mensagem = "E-mail ou CPF/CNPJ já cadastrado."
            };
        }

        var hash = Utils.HashSenha(senha);

        var user = UserDB.CadastrarUser(nome, hash, email, cpfj, out string errorMsg);

        if(!string.IsNullOrEmpty(errorMsg))
        {
            return new CadastroResult
            {
                Sucesso = false,
                Mensagem = "Erro ao Cadastrar usuário:" + errorMsg
            };
        }

        return new CadastroResult
        {
            Sucesso = true,
            Mensagem = "Usuário Cadastrado com sucesso"
        };

    }
}
