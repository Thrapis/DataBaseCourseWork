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
	public class PaymentRepository : IRepository<Payment>
	{
		OracleProvider provider;

		public PaymentRepository()
		{
			this.provider = new OracleProvider();
		}

		public PaymentRepository(OracleProvider oracleProvider)
		{
			this.provider = oracleProvider;
		}

		public IEnumerable<Payment> GetAll()
		{
			OracleDynamicParameters queryParameters = new OracleDynamicParameters();
			queryParameters.Add("@payment_cur", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

			string sql = $"Payment_Package.GetAllPayments";
			return provider.Connection.Query<Payment>(sql, queryParameters, commandType: CommandType.StoredProcedure);
		}

		public Payment Get(int id)
		{
			OracleDynamicParameters queryParameters = new OracleDynamicParameters();
			queryParameters.Add("@par_id", id, OracleMappingType.Int64, ParameterDirection.Input);
			queryParameters.Add("@payment_cur", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

			string sql = $@"Payment_Package.GetPaymentById";
			return provider.Connection.QueryFirstOrDefault<Payment>(sql, queryParameters, commandType: CommandType.StoredProcedure);
		}

		public int Insert(Payment item)
		{
			OracleDynamicParameters queryParameters = new OracleDynamicParameters();
			queryParameters.Add("@par_contract_id", item.CONTRACT_ID, OracleMappingType.Int64, ParameterDirection.Input);
			queryParameters.Add("@par_payment_amount", item.PAYMENT_AMOUNT, OracleMappingType.BinaryFloat, ParameterDirection.Input);
			queryParameters.Add("@par_payment_datetime", item.PAYMENT_DATETIME.ToString("yyyy-MM-dd HH:mm:ss"), OracleMappingType.NVarchar2, ParameterDirection.Input);
			queryParameters.Add("@inserted", 0, OracleMappingType.Int64, ParameterDirection.Output);

			string sql = $@"Payment_Package.InsertPayment";
			provider.Connection.Query(sql, queryParameters, commandType: CommandType.StoredProcedure);
			int inserted = queryParameters.Get<int>("@inserted");
			return inserted;
		}

		public int Update(Payment item)
		{
			OracleDynamicParameters queryParameters = new OracleDynamicParameters();
			queryParameters.Add("@par_id", item.ID, OracleMappingType.Int64, ParameterDirection.Input);
			queryParameters.Add("@par_contract_id", item.CONTRACT_ID, OracleMappingType.Int64, ParameterDirection.Input);
			queryParameters.Add("@par_payment_amount", item.PAYMENT_AMOUNT, OracleMappingType.BinaryFloat, ParameterDirection.Input);
			queryParameters.Add("@par_payment_datetime", item.PAYMENT_DATETIME.ToString("yyyy-MM-dd HH:mm:ss"), OracleMappingType.NVarchar2, ParameterDirection.Input);
			queryParameters.Add("@updated", 0, OracleMappingType.Int64, ParameterDirection.Output);

			string sql = $@"Payment_Package.UpdatePayment";
			provider.Connection.Query(sql, queryParameters, commandType: CommandType.StoredProcedure);
			int updated = queryParameters.Get<int>("@updated");
			return updated;
		}

		public int Delete(int id)
		{
			OracleDynamicParameters queryParameters = new OracleDynamicParameters();
			queryParameters.Add("@par_id", id, OracleMappingType.Int64, ParameterDirection.Input);
			queryParameters.Add("@deleted", 0, OracleMappingType.Int64, ParameterDirection.Output);

			string sql = $@"Payment_Package.DeletePayment";
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
