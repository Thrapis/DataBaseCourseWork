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
    public class ServiceDescriptionRepository : IRepository<ServiceDescription>
    {
        OracleProvider provider;

        public ServiceDescriptionRepository()
        {
            this.provider = new OracleProvider();
        }

        public ServiceDescriptionRepository(OracleProvider oracleProvider)
        {
            this.provider = oracleProvider;
        }

        public IEnumerable<ServiceDescription> GetAll()
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@service_desc_cur", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

            string sql = $"ServiceDescription_Package.GetAllServiceDescriptions";
            return provider.Connection.Query<ServiceDescription>(sql, queryParameters, commandType: CommandType.StoredProcedure);
        }

        public ServiceDescription Get(int id)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_id", id, OracleMappingType.Int64, ParameterDirection.Input);
            queryParameters.Add("@service_desc_cur", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

            string sql = $@"ServiceDescription_Package.GetServiceDescriptionById";
            return provider.Connection.QueryFirst<ServiceDescription>(sql, queryParameters, commandType: CommandType.StoredProcedure);
        }

        public int Insert(ServiceDescription item)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_service_name", item.SERVICE_NAME, OracleMappingType.NVarchar2, ParameterDirection.Input);
            queryParameters.Add("@par_service_description", item.SERVICE_DESCRIPTION, OracleMappingType.NVarchar2, ParameterDirection.Input);
            queryParameters.Add("@inserted", 0, OracleMappingType.Int64, ParameterDirection.Output);

            string sql = $@"ServiceDescription_Package.InsertServiceDescription";
            provider.Connection.Query(sql, queryParameters, commandType: CommandType.StoredProcedure);
            int inserted = queryParameters.Get<int>("@inserted");
            return inserted;
        }

        public int Update(ServiceDescription item)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_id", item.ID, OracleMappingType.Int64, ParameterDirection.Input);
            queryParameters.Add("@par_service_name", item.SERVICE_NAME, OracleMappingType.NVarchar2, ParameterDirection.Input);
            queryParameters.Add("@par_service_description", item.SERVICE_DESCRIPTION, OracleMappingType.NVarchar2, ParameterDirection.Input);
            queryParameters.Add("@updated", 0, OracleMappingType.Int64, ParameterDirection.Output);

            string sql = $@"ServiceDescription_Package.UpdateServiceDescription";
            provider.Connection.Query(sql, queryParameters, commandType: CommandType.StoredProcedure);
            int updated = queryParameters.Get<int>("@updated");
            return updated;
        }

        public int Delete(int id)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_id", id, OracleMappingType.Int64, ParameterDirection.Input);
            queryParameters.Add("@deleted", 0, OracleMappingType.Int64, ParameterDirection.Output);

            string sql = $@"ServiceDescription_Package.DeleteServiceDescription";
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
