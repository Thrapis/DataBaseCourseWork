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
	public class DebitRepository : IRepository<Debit>
	{
		OracleProvider provider;

		public DebitRepository()
		{
			this.provider = new OracleProvider();
		}

		public IEnumerable<Debit> GetAll()
		{
			OracleDynamicParameters queryParameters = new OracleDynamicParameters();
			queryParameters.Add("@debit_cur", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

			string sql = $"Debit_Package.GetAllDebits";
			return provider.Connection.Query<Debit>(sql, queryParameters, commandType: CommandType.StoredProcedure);
		}

		public Debit Get(int id)
		{
			OracleDynamicParameters queryParameters = new OracleDynamicParameters();
			queryParameters.Add("@par_id", id, OracleMappingType.Int64, ParameterDirection.Input);

			string sql = $@"Debit_Package.GetDebitById";
			return provider.Connection.QueryFirst<Debit>(sql, queryParameters);
		}

		public int Insert(Debit item)
		{
			OracleDynamicParameters queryParameters = new OracleDynamicParameters();
			queryParameters.Add("@par_contract_id", item.CONTRACT_ID, OracleMappingType.Int64, ParameterDirection.Input);
			queryParameters.Add("@par_debit_amount", item.DEBIT_AMOUNT, OracleMappingType.BinaryFloat, ParameterDirection.Input);
			queryParameters.Add("@par_debit_datetime", item.DEBIT_DATETIME.ToString("yyyy-MM-dd HH:mm:ss"), OracleMappingType.NVarchar2, ParameterDirection.Input);
			queryParameters.Add("@par_reason", item.REASON, OracleMappingType.NVarchar2, ParameterDirection.Input);
			queryParameters.Add("@inserted", 0, OracleMappingType.Int64, ParameterDirection.Output);

			string sql = $@"Debit_Package.InsertDebit";
			provider.Connection.Query(sql, queryParameters, commandType: CommandType.StoredProcedure);
			int inserted = queryParameters.Get<int>("@inserted");
			return inserted;
		}

		public int Update(Debit item)
		{
			OracleDynamicParameters queryParameters = new OracleDynamicParameters();
			queryParameters.Add("@par_id", item.ID, OracleMappingType.Int64, ParameterDirection.Input);
			queryParameters.Add("@par_contract_id", item.CONTRACT_ID, OracleMappingType.Int64, ParameterDirection.Input);
			queryParameters.Add("@par_debit_amount", item.DEBIT_AMOUNT, OracleMappingType.BinaryFloat, ParameterDirection.Input);
			queryParameters.Add("@par_debit_datetime", item.DEBIT_DATETIME.ToString("yyyy-MM-dd HH:mm:ss"), OracleMappingType.NVarchar2, ParameterDirection.Input);
			queryParameters.Add("@par_reason", item.REASON, OracleMappingType.NVarchar2, ParameterDirection.Input);
			queryParameters.Add("@updated", 0, OracleMappingType.Int64, ParameterDirection.Output);

			string sql = $@"Debit_Package.UpdateDebit";
			provider.Connection.Query(sql, queryParameters, commandType: CommandType.StoredProcedure);
			int updated = queryParameters.Get<int>("@updated");
			return updated;
		}

		public int Delete(int id)
		{
			OracleDynamicParameters queryParameters = new OracleDynamicParameters();
			queryParameters.Add("@par_id", id, OracleMappingType.Int64, ParameterDirection.Input);
			queryParameters.Add("@deleted", 0, OracleMappingType.Int64, ParameterDirection.Output);

			string sql = $@"Debit_Package.DeleteDebit";
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
