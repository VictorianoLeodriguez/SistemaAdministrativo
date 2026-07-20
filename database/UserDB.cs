using MySqlConnector;
using SistemaAdm.Models;
using SistemaAdm.ViewModel;

namespace SistemaAdm.database;

public class UserDB
{
    public static User BuscarUserAuth(string cnpj)
    {
        const string sqlCommand = @"SELECT USR_AIC, USR_NAME, USR_PASS, USR_EML, 
                            USR_CPFJ, USR_ATV, USR_CAD_DT
                            FROM USRK
                            WHERE USR_CNPJ = @cnpj AND USR_ATV = 1";

        var query = new MySqlCommand(sqlCommand);
        query.Parameters.AddWithValue("@cnpj", cnpj);

        var result = MYSQLHELPER.ExecutaConsultaUnica(query, out string errorMsg);

        if (!string.IsNullOrEmpty(errorMsg))
            throw new Exception(errorMsg);

        if (result == null)
            return null;

        return new User
        {
            Codigo = int.TryParse(result["USR_AIC"], out int cod) ? cod : 0,
            RazaoSocial   = result.GetValueOrDefault("USR_NAME") ?? string.Empty,
            Senha  = result.GetValueOrDefault("USR_PASS") ?? string.Empty,
            Email  = result.GetValueOrDefault("USR_EML")  ?? string.Empty,
            CNPJ   = result.GetValueOrDefault("USR_CPFJ") ?? string.Empty,
            Ativo  = result.GetValueOrDefault("USR_ATV")  == "1"
        };
    }

    public static User ExisteUser(string email, string cnpj)
    {
        const string sqlCommand = @"SELECT USR_AIC, USR_NAME, USR_PASS, USR_EML
                                    USR_CNPJ, USR_ATV, USR_CAD_DT
                                    FROM USRK
                                    WHERE (USR_EML = @EMAIL OR USR_CNPJ = @CNPJ) 
                                    AND USR_ATV = 1";

        var query = new MySqlCommand(sqlCommand);
        query.Parameters.AddWithValue("@EMAIL", email);
        query.Parameters.AddWithValue("@CNPJ", cnpj);

        var results = MYSQLHELPER.ExecutaConsultaUnica(query, out string errorMsg);

        if(!string.IsNullOrEmpty(errorMsg))
            throw new Exception(errorMsg);

        if(results == null)
            return null;

        return new User
        {
            Codigo = int.TryParse(results["USR_AIC"], out int cod) ? cod : 0,
            RazaoSocial   = results.GetValueOrDefault("USR_NAME") ?? string.Empty,
            Senha  = results.GetValueOrDefault("USR_PASS") ?? string.Empty,
            Email  = results.GetValueOrDefault("USR_EML")  ?? string.Empty,
            CNPJ   = results.GetValueOrDefault("USR_CPFJ") ?? string.Empty,
            Ativo  = results.GetValueOrDefault("USR_ATV")  == "1"
        };
    }

    public static bool CadastrarUser(User user, out string errorMsg)
    {
        const string sqlCommand = @"INSERT INTO USRK (USR_NAME, USR_EML, USR_CNPJ, USR_ATV, USR_PASS, USR_CAD_DT)
                                    VALUES (@nome, @email, @cnpj, 1, @senha, NOW())";

        var parametros = new List<MySqlParameter>
        {
                new("@nome",  user.RazaoSocial),
                new("@senha", user.Senha),
                new("@email", user.Email),
                new("@cnpj",  user.CNPJ)
        };

        var query = new MySqlCommand(sqlCommand);
        query.Parameters.AddRange(parametros.ToArray());

        return MYSQLHELPER.Executar(query, MYSQLHELPER.QueryMode.Insert, out errorMsg, out _);       
    }
}