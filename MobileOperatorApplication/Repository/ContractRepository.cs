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
using System.Globalization;

namespace MobileOperatorApplication.Repository
{
    public class ContractRepository : IRepository<Contract>
    {
        OracleProvider provider;

        public ContractRepository()
        {
            this.provider = new OracleProvider();
        }

        public IEnumerable<Contract> GetAll()
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@contract_cur", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

            string sql = $"Contract_Package.GetAllContracts";
            return provider.Connection.Query<Contract>(sql, queryParameters, commandType: CommandType.StoredProcedure);
        }

        public Contract Get(int id)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_id", id, OracleMappingType.Int64, ParameterDirection.Input);

            string sql = $@"Contract_Package.GetContractById";
            return provider.Connection.QueryFirst<Contract>(sql, queryParameters);
        }

        public int Insert(Contract item)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_tariff_id", item.TARIFF_ID, OracleMappingType.Int64, ParameterDirection.Input);
            queryParameters.Add("@par_client_id", item.CLIENT_ID, OracleMappingType.Int64, ParameterDirection.Input);
            queryParameters.Add("@par_employee_id", item.EMPLOYEE_ID, OracleMappingType.Int64, ParameterDirection.Input);
            queryParameters.Add("@par_signing_datetime", item.SIGNING_DATETIME.ToString("yyyy-MM-dd HH:mm:ss"), OracleMappingType.NVarchar2, ParameterDirection.Input);
            queryParameters.Add("@inserted", 0, OracleMappingType.Int64, ParameterDirection.Output);

            string sql = $@"Contract_Package.InsertContract";
            provider.Connection.Query(sql, queryParameters, commandType: CommandType.StoredProcedure);
            int inserted = queryParameters.Get<int>("@inserted");
            return inserted;
        }

        public int Update(Contract item)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_id", item.ID, OracleMappingType.Int64, ParameterDirection.Input);
            queryParameters.Add("@par_tariff_id", item.TARIFF_ID, OracleMappingType.Int64, ParameterDirection.Input);
            queryParameters.Add("@par_client_id", item.CLIENT_ID, OracleMappingType.Int64, ParameterDirection.Input);
            queryParameters.Add("@par_employee_id", item.EMPLOYEE_ID, OracleMappingType.Int64, ParameterDirection.Input);
            queryParameters.Add("@par_signing_datetime", item.SIGNING_DATETIME.ToString("yyyy-MM-dd HH:mm:ss"), OracleMappingType.NVarchar2, ParameterDirection.Input);
            queryParameters.Add("@updated", 0, OracleMappingType.Int64, ParameterDirection.Output);

            string sql = $@"Contract_Package.UpdateContract";
            provider.Connection.Query(sql, queryParameters, commandType: CommandType.StoredProcedure);
            int updated = queryParameters.Get<int>("@updated");
            return updated;
        }

        public int Delete(int id)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_id", id, OracleMappingType.Int64, ParameterDirection.Input);
            queryParameters.Add("@deleted", 0, OracleMappingType.Int64, ParameterDirection.Output);

            string sql = $@"Contract_Package.DeleteContract";
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
