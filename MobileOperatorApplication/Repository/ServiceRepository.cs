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
	public class ServiceRepository : IRepository<Service>
	{
		OracleProvider provider;

		public ServiceRepository()
		{
			this.provider = new OracleProvider();
		}

		public ServiceRepository(OracleProvider oracleProvider)
		{
			this.provider = oracleProvider;
		}

		public IEnumerable<Service> GetAll()
		{
			OracleDynamicParameters queryParameters = new OracleDynamicParameters();
			queryParameters.Add("@service_cur", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

			string sql = $"Service_Package.GetAllServices";
			return provider.Connection.Query<Service>(sql, queryParameters, commandType: CommandType.StoredProcedure);
		}

		public Service Get(int id)
		{
			OracleDynamicParameters queryParameters = new OracleDynamicParameters();
			queryParameters.Add("@par_id", id, OracleMappingType.Int64, ParameterDirection.Input);
			queryParameters.Add("@service_cur", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

			string sql = $@"Service_Package.GetServiceById";
			return provider.Connection.QueryFirst<Service>(sql, queryParameters, commandType: CommandType.StoredProcedure);
		}

		public int Insert(Service item)
		{
			OracleDynamicParameters queryParameters = new OracleDynamicParameters();
			queryParameters.Add("@par_contract_id", item.CONTRACT_ID, OracleMappingType.Int64, ParameterDirection.Input);
			queryParameters.Add("@par_description_id", item.DESCRIPTION_ID, OracleMappingType.Int64, ParameterDirection.Input);
			queryParameters.Add("@par_service_amount", item.SERVICE_AMOUNT, OracleMappingType.BinaryFloat, ParameterDirection.Input);
			queryParameters.Add("@par_connection_date", item.CONNECTION_DATE.ToString("yyyy-MM-dd HH:mm:ss"), OracleMappingType.NVarchar2, ParameterDirection.Input);
			queryParameters.Add("@par_disconnection_date", item.DISCONNECTION_DATE.ToString("yyyy-MM-dd HH:mm:ss"), OracleMappingType.NVarchar2, ParameterDirection.Input);
			queryParameters.Add("@inserted", 0, OracleMappingType.Int64, ParameterDirection.Output);

			string sql = $@"Service_Package.InsertService";
			provider.Connection.Query(sql, queryParameters, commandType: CommandType.StoredProcedure);
			int inserted = queryParameters.Get<int>("@inserted");
			return inserted;
		}

		public int Update(Service item)
		{
			OracleDynamicParameters queryParameters = new OracleDynamicParameters();
			queryParameters.Add("@par_id", item.ID, OracleMappingType.Int64, ParameterDirection.Input);
			queryParameters.Add("@par_contract_id", item.CONTRACT_ID, OracleMappingType.Int64, ParameterDirection.Input);
			queryParameters.Add("@par_description_id", item.DESCRIPTION_ID, OracleMappingType.Int64, ParameterDirection.Input);
			queryParameters.Add("@par_service_amount", item.SERVICE_AMOUNT, OracleMappingType.BinaryFloat, ParameterDirection.Input);
			queryParameters.Add("@par_connection_date", item.CONNECTION_DATE.ToString("yyyy-MM-dd HH:mm:ss"), OracleMappingType.NVarchar2, ParameterDirection.Input);
			queryParameters.Add("@par_disconnection_date", item.DISCONNECTION_DATE.ToString("yyyy-MM-dd HH:mm:ss"), OracleMappingType.NVarchar2, ParameterDirection.Input);
			queryParameters.Add("@updated", 0, OracleMappingType.Int64, ParameterDirection.Output);

			string sql = $@"Service_Package.UpdateService";
			provider.Connection.Query(sql, queryParameters, commandType: CommandType.StoredProcedure);
			int updated = queryParameters.Get<int>("@updated");
			return updated;
		}

		public int Delete(int id)
		{
			OracleDynamicParameters queryParameters = new OracleDynamicParameters();
			queryParameters.Add("@par_id", id, OracleMappingType.Int64, ParameterDirection.Input);
			queryParameters.Add("@deleted", 0, OracleMappingType.Int64, ParameterDirection.Output);

			string sql = $@"Service_Package.DeleteService";
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
