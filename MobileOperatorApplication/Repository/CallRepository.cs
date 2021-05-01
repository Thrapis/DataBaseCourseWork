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
	public class CallRepository : IRepository<Call>
	{
		OracleProvider provider;

		public CallRepository()
		{
			this.provider = new OracleProvider();
		}

		public CallRepository(OracleProvider oracleProvider)
		{
			this.provider = oracleProvider;
		}

		public IEnumerable<Call> GetAll()
		{
			OracleDynamicParameters queryParameters = new OracleDynamicParameters();
			queryParameters.Add("@call_cur", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

			string sql = $"Call_Package.GetAllCalls";
			return provider.Connection.Query<Call>(sql, queryParameters, commandType: CommandType.StoredProcedure);
		}

		public Call Get(int id)
		{
			OracleDynamicParameters queryParameters = new OracleDynamicParameters();
			queryParameters.Add("@par_id", id, OracleMappingType.Int64, ParameterDirection.Input);
			queryParameters.Add("@call_cur", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

			string sql = $@"Call_Package.GetCallById";
			return provider.Connection.QueryFirstOrDefault<Call>(sql, queryParameters, commandType: CommandType.StoredProcedure);
		}

		public int Insert(Call item)
		{
			OracleDynamicParameters queryParameters = new OracleDynamicParameters();
			queryParameters.Add("@par_contract_id", item.CONTRACT_ID, OracleMappingType.Int64, ParameterDirection.Input);
			queryParameters.Add("@par_to_phone_number", item.TO_PHONE_NUMBER, OracleMappingType.NVarchar2, ParameterDirection.Input);
			queryParameters.Add("@par_talk_time", item.TALK_TIME.ToString(@"dd\ hh\:mm\:ss"), OracleMappingType.NVarchar2, ParameterDirection.Input);
			queryParameters.Add("@par_call_datetime", item.CALL_DATETIME.ToString("yyyy-MM-dd HH:mm:ss"), OracleMappingType.NVarchar2, ParameterDirection.Input);
			queryParameters.Add("@inserted", 0, OracleMappingType.Int64, ParameterDirection.Output);

			string sql = $@"Call_Package.InsertCall";
			provider.Connection.Query(sql, queryParameters, commandType: CommandType.StoredProcedure);
			int inserted = queryParameters.Get<int>("@inserted");
			return inserted;
		}

		public int Update(Call item)
		{
			OracleDynamicParameters queryParameters = new OracleDynamicParameters();
			queryParameters.Add("@par_id", item.ID, OracleMappingType.Int64, ParameterDirection.Input);
			queryParameters.Add("@par_contract_id", item.CONTRACT_ID, OracleMappingType.Int64, ParameterDirection.Input);
			queryParameters.Add("@par_to_phone_number", item.TO_PHONE_NUMBER, OracleMappingType.NVarchar2, ParameterDirection.Input);
			queryParameters.Add("@par_talk_time", item.TALK_TIME.ToString(@"dd\ hh\:mm\:ss"), OracleMappingType.NVarchar2, ParameterDirection.Input);
			queryParameters.Add("@par_call_datetime", item.CALL_DATETIME.ToString("yyyy-MM-dd HH:mm:ss"), OracleMappingType.NVarchar2, ParameterDirection.Input);
			queryParameters.Add("@updated", 0, OracleMappingType.Int64, ParameterDirection.Output);

			string sql = $@"Call_Package.UpdateCall";
			provider.Connection.Query(sql, queryParameters, commandType: CommandType.StoredProcedure);
			int updated = queryParameters.Get<int>("@updated");
			return updated;
		}

		public int Delete(int id)
		{
			OracleDynamicParameters queryParameters = new OracleDynamicParameters();
			queryParameters.Add("@par_id", id, OracleMappingType.Int64, ParameterDirection.Input);
			queryParameters.Add("@deleted", 0, OracleMappingType.Int64, ParameterDirection.Output);

			string sql = $@"Call_Package.DeleteCall";
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
