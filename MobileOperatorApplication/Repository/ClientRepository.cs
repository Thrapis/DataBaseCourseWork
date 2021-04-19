using MobileOperatorApplication.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MobileOperatorApplication.Oracle;
using Dapper.Oracle;
using System.Data;

namespace MobileOperatorApplication.Repository
{
    public class ClientRepository : IRepository<Client>
    {
        OracleProvider provider;

        public ClientRepository()
        {
            this.provider = new OracleProvider();
        }

        public IEnumerable<Client> GetAll()
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@client_cur", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

            string sql = $"Client_Package.GetAllClients";
            return provider.Connection.Query<Client>(sql, queryParameters, commandType: CommandType.StoredProcedure);
        }

        public Client Get(int id)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_id", id, OracleMappingType.Int64, ParameterDirection.Input);

            string sql = $@"Client_Package.GetClientById";
            return provider.Connection.QueryFirst<Client>(sql, queryParameters);
        }

        public int Insert(Client item)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_full_name", item.FULL_NAME, OracleMappingType.NVarchar2, ParameterDirection.Input);
            queryParameters.Add("@par_passport_number", item.PASSPORT_NUMBER, OracleMappingType.NVarchar2, ParameterDirection.Input);
            queryParameters.Add("@par_account_login", item.ACCOUNT_LOGIN, OracleMappingType.NVarchar2, ParameterDirection.Input);
            queryParameters.Add("@inserted", 0, OracleMappingType.Int64, ParameterDirection.Output);

            string sql = $@"Client_Package.InsertClient";
            provider.Connection.Query(sql, queryParameters, commandType: CommandType.StoredProcedure);
            int inserted = queryParameters.Get<int>("@inserted");
            return inserted;
        }

        public int Update(Client item)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_id", item.ID, OracleMappingType.Int64, ParameterDirection.Input);
            queryParameters.Add("@par_full_name", item.FULL_NAME, OracleMappingType.NVarchar2, ParameterDirection.Input);
            queryParameters.Add("@par_passport_number", item.PASSPORT_NUMBER, OracleMappingType.NVarchar2, ParameterDirection.Input);
            queryParameters.Add("@par_account_login", item.ACCOUNT_LOGIN, OracleMappingType.NVarchar2, ParameterDirection.Input);
            queryParameters.Add("@updated", 0, OracleMappingType.Int64, ParameterDirection.Output);

            string sql = $@"Client_Package.UpdateClient";
            provider.Connection.Query(sql, queryParameters, commandType: CommandType.StoredProcedure);
            int updated = queryParameters.Get<int>("@updated");
            return updated;
        }

        public int Delete(int id)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_id", id, OracleMappingType.Int64, ParameterDirection.Input);
            queryParameters.Add("@deleted", 0, OracleMappingType.Int64, ParameterDirection.Output);

            string sql = $@"Client_Package.DeleteClient";
            provider.Connection.Query(sql, queryParameters, commandType: CommandType.StoredProcedure);
            int deleted = queryParameters.Get<int>("@deleted");
            return deleted;
        }

        public void Dispose()
        {
            provider.Connection.Close();
        }
    }
}
