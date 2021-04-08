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
        OracleConnection connection;

        public OracleProvider(string connectionString)
        {
            connection = new OracleConnection(connectionString);
            connection.Open();
            Console.WriteLine(connection.GetSessionInfo());
        }

        public void Dispose()
        {
            connection.Close();
        }

        public string GetHash(string username, string password)
        {
            string sql = $"select CreateAccount(\'{username}\', \'{password}\') from dual";
            return connection.QueryFirst<string>(sql);
        }

        public IEnumerable<Post> GetPosts()
        {
            return connection.Query<Post>("select * from POSTS");
        }
    }
}
