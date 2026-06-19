using MySqlConnector;
using SistemaAdm.Models;
using SistemaAdm.ViewModel;

namespace SistemaAdm.database;

public class UserDB
{
    public static User BuscarUser(string cpfj)
    {
        const string sqlCommand = @"SELECT USR_AIC, USR_NAME, USR_PASS, USR_EML, 
                            USR_CPFJ, USR_ATV, USR_CAD_DT
                            FROM USRK
                            WHERE USR_CPFJ = @cpfj AND USR_ATV = 1";

        var query = new MySqlCommand(sqlCommand);
        query.Parameters.AddWithValue("@cpfj", cpfj);

        var result = MYSQLHELPER.ExecutaConsultaUnica(query, out string errorMsg);

        if (!string.IsNullOrEmpty(errorMsg))
            throw new Exception(errorMsg);

        if (result == null)
            return null;

        return new User
        {
            Codigo = int.TryParse(result["USR_AIC"], out int cod) ? cod : 0,
            Nome   = result.GetValueOrDefault("USR_NAME") ?? string.Empty,
            Senha  = result.GetValueOrDefault("USR_PASS") ?? string.Empty,
            Email  = result.GetValueOrDefault("USR_EML")  ?? string.Empty,
            CPFJ   = result.GetValueOrDefault("USR_CPFJ") ?? string.Empty,
            Ativo  = result.GetValueOrDefault("USR_ATV")  == "1"
        };
    }

    public static User BuscarEmail(string email)
    {
        const string sqlCommand = @"SELECT USR_AIC, USR_NAME, USR_PASS, USR_EML
                                    USR_CPFJ, USR_ATV, USR_CAD_DT
                                    FROM USRK
                                    WHERE USR_EML = @EMAIL AND USR_ATV = 1";

        var query = new MySqlCommand(sqlCommand);
        query.Parameters.AddWithValue("@EMAIL", email);

        var results = MYSQLHELPER.ExecutaConsultaUnica(query, out string errorMsg);

        if(!string.IsNullOrEmpty(errorMsg))
            throw new Exception(errorMsg);

        if(results == null)
            return null;

        return new User
        {
            Codigo = int.TryParse(results["USR_AIC"], out int cod) ? cod : 0,
            Nome   = results.GetValueOrDefault("USR_NAME") ?? string.Empty,
            Senha  = results.GetValueOrDefault("USR_PASS") ?? string.Empty,
            Email  = results.GetValueOrDefault("USR_EML")  ?? string.Empty,
            CPFJ   = results.GetValueOrDefault("USR_CPFJ") ?? string.Empty,
            Ativo  = results.GetValueOrDefault("USR_ATV")  == "1"
        };
    }

    public static bool CadastrarUser(User cad, out string errorMsg)
    {
        const string sqlCommand = @"INSERT INTO USRK (USR_NAME, USR_EML, USR_CPFJ, USR_ATV, USR_PASS, USR_CAD_DT)
                                    VALUES (@nome, @email, @cpfj, 1, @senha, NOW())";

        var parametros = new List<MySqlParameter>
        {
                new("@nome",  cad.Nome),
                new("@senha", cad.Senha),
                new("@email", cad.Email),
                new("@cpfj",  cad.CPFJ)
        };

        var query = new MySqlCommand(sqlCommand);
        query.Parameters.AddRange(parametros.ToArray());

        return MYSQLHELPER.Executar(query, MYSQLHELPER.QueryMode.Insert, out errorMsg, out _);       
    }
}