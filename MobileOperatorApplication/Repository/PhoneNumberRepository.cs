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

        public PhoneNumberRepository(OracleProvider oracleProvider)
        {
            this.provider = oracleProvider;
        }

        public IEnumerable<PhoneNumber> GetAll()
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@phone_number_cur", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

            string sql = $"C##BAA.PhoneNumber_Package.GetAllPhoneNumbers";
            return provider.Connection.Query<PhoneNumber>(sql, queryParameters, commandType: CommandType.StoredProcedure);
        }

        public PhoneNumber Get(int id)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_id", id, OracleMappingType.Int64, ParameterDirection.Input);
            queryParameters.Add("@phone_number_cur", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

            string sql = $@"C##BAA.PhoneNumber_Package.GetPhoneNumberById";
            return provider.Connection.QueryFirstOrDefault<PhoneNumber>(sql, queryParameters, commandType: CommandType.StoredProcedure);
        }

        public PhoneNumber Get(string phone_number)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_number", phone_number, OracleMappingType.NVarchar2, ParameterDirection.Input);
            queryParameters.Add("@phone_number_cur", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

            string sql = $@"C##BAA.PhoneNumber_Package.GetPhoneNumberByNumber";
            return provider.Connection.QueryFirstOrDefault<PhoneNumber>(sql, queryParameters, commandType: CommandType.StoredProcedure);
        }

        public int Insert(PhoneNumber item)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_phone_number", item.PHONE_NUMBER, OracleMappingType.NVarchar2, ParameterDirection.Input);
            queryParameters.Add("@par_contract_id", item.CONTRACT_ID, OracleMappingType.Int64, ParameterDirection.Input);
            queryParameters.Add("@inserted", 0, OracleMappingType.Int64, ParameterDirection.Output);

            string sql = $@"C##BAA.PhoneNumber_Package.InsertPhoneNumber";
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

            string sql = $@"C##BAA.PhoneNumber_Package.UpdatePhoneNumber";
            provider.Connection.Query(sql, queryParameters, commandType: CommandType.StoredProcedure);
            int updated = queryParameters.Get<int>("@updated");
            return updated;
        }

        public int Delete(int id)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_id", id, OracleMappingType.Int64, ParameterDirection.Input);
            queryParameters.Add("@deleted", 0, OracleMappingType.Int64, ParameterDirection.Output);

            string sql = $@"C##BAA.PhoneNumber_Package.DeletePhoneNumber";
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