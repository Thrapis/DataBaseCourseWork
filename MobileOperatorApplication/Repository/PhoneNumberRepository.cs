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
    public class PhoneNumberRepository : IRepository<PhoneNumber>
    {
        OracleProvider provider;

        public PhoneNumberRepository()
        {
            this.provider = new OracleProvider();
        }

        public IEnumerable<PhoneNumber> GetAll()
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@phone_number_cur", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

            string sql = $"PhoneNumber_Package.GetAllPhoneNumbers";
            return provider.Connection.Query<PhoneNumber>(sql, queryParameters, commandType: CommandType.StoredProcedure);
        }

        public PhoneNumber Get(int id)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_id", id, OracleMappingType.Int64, ParameterDirection.Input);

            string sql = $@"PhoneNumber_Package.GetPhoneNumberById";
            return provider.Connection.QueryFirst<PhoneNumber>(sql, queryParameters);
        }

        public int Insert(PhoneNumber item)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_phone_number", item.PHONE_NUMBER, OracleMappingType.NVarchar2, ParameterDirection.Input);
            queryParameters.Add("@par_contract_id", item.CONTRACT_ID, OracleMappingType.Int64, ParameterDirection.Input);
            queryParameters.Add("@inserted", 0, OracleMappingType.Int64, ParameterDirection.Output);

            string sql = $@"PhoneNumber_Package.InsertPhoneNumber";
            provider.Connection.Query(sql, queryParameters, commandType: CommandType.StoredProcedure);
            int inserted = queryParameters.Get<int>("@inserted");
            return inserted;
        }

        public int Update(PhoneNumber item)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_id", item.ID, OracleMappingType.Int64, ParameterDirection.Input);
            queryParameters.Add("@par_phone_number", item.PHONE_NUMBER, OracleMappingType.NVarchar2, ParameterDirection.Input);
            queryParameters.Add("@par_contract_id", item.CONTRACT_ID, OracleMappingType.Int64, ParameterDirection.Input);
            queryParameters.Add("@updated", 0, OracleMappingType.Int64, ParameterDirection.Output);

            string sql = $@"PhoneNumber_Package.UpdatePhoneNumber";
            provider.Connection.Query(sql, queryParameters, commandType: CommandType.StoredProcedure);
            int updated = queryParameters.Get<int>("@updated");
            return updated;
        }

        public int Delete(int id)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_id", id, OracleMappingType.Int64, ParameterDirection.Input);
            queryParameters.Add("@deleted", 0, OracleMappingType.Int64, ParameterDirection.Output);

            string sql = $@"PhoneNumber_Package.DeletePhoneNumber";
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