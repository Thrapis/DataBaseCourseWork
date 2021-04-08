using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MobileOperatorApplication.Model;

namespace MobileOperatorApplication.Oracle
{
    public class OracleProvider : IDisposable
    {
        public OracleConnection Connection { get; private set; }

        public OracleProvider()
        {
            string connectionString = "DATA SOURCE = DESKTOP-H8ENAQU:1521 / orcl;" +
                " USER ID=c##baa; PASSWORD=12345; Pooling = False;";
            Connection = new OracleConnection(connectionString);
            Connection.Open();
            Console.WriteLine(Connection.GetSessionInfo());
        }

        public void Dispose()
        {
            Connection.Close();
        }

        public string GetHash(string username, string password)
        {
            string sql = $"select CreateAccount(\'{username}\', \'{password}\') from dual";
            return Connection.QueryFirst<string>(sql);
        }

        public IEnumerable<Post> GetPosts()
        {
            return Connection.Query<Post>("select * from POST");
        }
    }
}
