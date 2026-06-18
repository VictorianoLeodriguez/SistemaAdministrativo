using MySqlConnector;
using SistemaAdm.Models;

namespace SistemaAdm.database;

public class UserDB
{
   public static User Authenticar(string CPFJ)
  {
     string sqlString = @"SELECT USR_AIC, USR_NAME, USR_PASS, USR_EML, USR_CPFJ,
                          USR_ATV, USR_CAD_DT
                          FROM USRK
                          WHERE USR_CPFJ = @CPFJ AND USR_ATV = 1";

    MySqlCommand query = new(sqlString);
    query.Parameters.AddWithValue("@CPFJ", CPFJ);

    var Result = MYSQLHELPER.ExecutaConsultaUnica(query, out string ErrorMsg);

    if (Result == null)
        return null;

    User user = new User
    {
        Codigo = int.Parse(Result["USR_AIC"]),
        Nome = Result["USR_NAME"],
        Senha = Result["USR_PASS"],
        Email = Result["USR_EML"],
        CPFJ = Result["USR_CPFJ"],
        Ativo = Result["USR_ATV"] == "1"
    };

    return user;
  }
}
