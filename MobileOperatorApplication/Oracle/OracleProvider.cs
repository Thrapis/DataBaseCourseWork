using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MobileOperatorApplication.Model;
using Dapper.Oracle;

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

        public int CreateAccount(string login, string password, int access_level)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_username", login, OracleMappingType.NVarchar2, ParameterDirection.Input);
            queryParameters.Add("@par_password", password, OracleMappingType.NVarchar2, ParameterDirection.Input);
            queryParameters.Add("@par_access_level", access_level, OracleMappingType.Int32, ParameterDirection.Input);
            queryParameters.Add("@created", 0, OracleMappingType.Int32, ParameterDirection.Output);

            string sql = $@"Account_Package.CreateAccount";
            Connection.Query(sql, queryParameters, commandType: CommandType.StoredProcedure);
            int created = queryParameters.Get<int>("@created");
            return created;
        }

        public AccountInfo GetAccount(string login, string password)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_username", login, OracleMappingType.NVarchar2, ParameterDirection.InputOutput);
            queryParameters.Add("@par_password", password, OracleMappingType.NVarchar2, ParameterDirection.Input);
            queryParameters.Add("@ret_access_level", -999, OracleMappingType.Int32, ParameterDirection.Output);

            string sql = $@"Account_Package.GetAccount";
            Connection.Query(sql, queryParameters, commandType: CommandType.StoredProcedure);
            string outlogin = queryParameters.Get<string>("@par_username"); ;
            int access_level = queryParameters.Get<int>("@ret_access_level");
            
            if (outlogin == login && access_level > 0)
            {
                return new AccountInfo(login, access_level);
            }
            else
            {
                return null;
            }
        }

        public float GetContractBalance(int id)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_id", id, OracleMappingType.Int64, ParameterDirection.Input);
            queryParameters.Add("@balance", 0, OracleMappingType.BinaryFloat, ParameterDirection.Output);

            string sql = $@"Contract_Package.GetContractBalance";
            Connection.Query(sql, queryParameters, commandType: CommandType.StoredProcedure);
            float balance = queryParameters.Get<float>("@balance");
            return balance;
        }

        public IEnumerable<Post> GetPosts()
        {
            return Connection.Query<Post>("select * from POST");
        }
    }
}
