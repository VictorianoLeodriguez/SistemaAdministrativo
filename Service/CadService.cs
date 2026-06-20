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
                Mensagem = "Email Inválido"
            };
        }

        if(UserDB.ExisteUser(email, cpfj) != null)
        {
            return new CadastroResult
            {
              Sucesso = false,
              Mensagem = "Já Existe um User com Esse Email ou Cpf"  
            };
        }

        var user = UserDB.CadastrarUser(nome, senha, email, cpfj, out string errorMsg);

        if(!string.IsNullOrEmpty(errorMsg))
        {
            return new CadastroResult
            {
                Sucesso = false,
                Mensagem = $"{errorMsg}"
            };
        }

        return new CadastroResult
        {
            Sucesso = true,
            Mensagem = "Usuário Cadastrado com sucesso"
        };

    }
}
