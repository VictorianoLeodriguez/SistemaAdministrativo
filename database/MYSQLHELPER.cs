using MySqlConnector;
namespace SistemaAdm.database;


public class MYSQLHELPER
{

    public static string ConexaoPadrao {get;set;} = string.Empty;

    public enum QueryMode
    {
        Insert = 0,
        Edit= 1,
        Delete=2
    }

    public static MySqlConnection AbreConexao(out string erroMsg)
    {


        try
        {
            MySqlConnection Conexao = new MySqlConnection(ConexaoPadrao);
            
            Conexao.Open();

            erroMsg = string.Empty;
            return Conexao;            
        }
        catch(Exception ex)
        {
            erroMsg = ex.Message + Environment.NewLine + "Erro ao Abrir a Conexão";
            return null;
        }
    }

    public static void FechaConexao (MySqlConnection Conexao, out string errorMsg)
    {
        errorMsg = string.Empty;
        try
        {
            if(Conexao == null || Conexao.State == System.Data.ConnectionState.Closed)
                return;

            Conexao.Close();
        }
        catch(Exception ex)
        {
            errorMsg = ex.Message + Environment.NewLine + "erro ao Fechar a Conexão";
        }
        finally
        {
            if(Conexao != null)
                Conexao.Dispose();
        }
    }

    public static List<Dictionary<string, string>> ExecutaConsulta(MySqlCommand query, out string errorMsg)
    {
        errorMsg = string.Empty;

        MySqlConnection Conexao = null;

        List<Dictionary<string, string>> lista = null;

        try
        {
            Conexao = AbreConexao(out errorMsg);
            if(Conexao == null)
                return null;

            query.Connection = Conexao;

            using (MySqlDataReader leitor = query.ExecuteReader())
            {
                lista = new List<Dictionary<string, string>>();

                while (leitor.Read())
                {
                    var linha = new Dictionary<string,string>();

                    for (int i = 0; i <leitor.FieldCount; i++)
                    {
                        string coluna = leitor.GetName(i);
                        string valor = leitor.IsDBNull(i) ? null : leitor.GetValue(i).ToString();

                        linha.Add(coluna, valor);
                    }
                    lista.Add(linha);
                }
            }
            if (lista == null || lista.Count == 0)
                return null;
        }
        catch(Exception ex)
        {
            errorMsg = ex.Message + Environment.NewLine + "Erro ao executar consulta";
            return null;
        }
        finally
        {
            string menssagem = errorMsg;
            FechaConexao(Conexao, out errorMsg);
            errorMsg = string.IsNullOrEmpty(errorMsg) ? menssagem : errorMsg;
        }
        return lista;
    }

    public static Dictionary<string, string> ExecutaConsultaUnica(MySqlCommand query, out string errorMsg)
    {
        errorMsg = string.Empty;

        MySqlConnection Conexao = null;

        Dictionary<string, string> obj = null;

        try
        {
            Conexao = AbreConexao(out errorMsg);
            if(Conexao == null)
                return null;

            query.Connection = Conexao;

            using (MySqlDataReader leitor = query.ExecuteReader())
            {
                if (leitor.Read())
                {
                    obj = new Dictionary<string,string>();

                    for (int i = 0; i <leitor.FieldCount; i++)
                    {
                        string coluna = leitor.GetName(i);
                        string valor = leitor.IsDBNull(i) ? null : leitor.GetValue(i).ToString();

                        obj.Add(coluna, valor);
                    }
                }
            }
        }
        catch(Exception ex)
        {
            errorMsg = ex.Message + Environment.NewLine + "Erro ao executar consulta";
            return null;
        }
        finally
        {
            string menssagem = errorMsg;
            FechaConexao(Conexao, out errorMsg);
            errorMsg = string.IsNullOrEmpty(errorMsg) ? menssagem : errorMsg;
        }
        return obj;
    }
    
    public static bool Executar(MySqlCommand query, QueryMode tipo, out string errorMsg, out long id)
    {
        errorMsg = string.Empty;
        id = 0;
        MySqlConnection Conexao = null;

        try
        {
            Conexao = AbreConexao(out errorMsg);
            if(Conexao == null)
            {
                errorMsg = "Conexão não estabelecida";
                return false;
            }

            query.Connection = Conexao;
            query.ExecuteNonQuery();

            if(tipo == QueryMode.Insert)
            {
                query.CommandText = "SELECT LAST_INSERT_ID()";
                id = Convert.ToInt64(query.ExecuteScalar());
            }
            return true;
        }
        catch(Exception ex)
        {
            errorMsg = ex.Message + Environment.NewLine + "Erro ao Executar Comando sql";
            return false;
        }
        finally
        {
            string msg = errorMsg;
            FechaConexao(Conexao, out errorMsg);
            errorMsg = string.IsNullOrEmpty(errorMsg) ? msg : errorMsg;
        }
    }
}

