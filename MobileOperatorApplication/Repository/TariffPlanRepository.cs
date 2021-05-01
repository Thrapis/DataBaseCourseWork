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
    public class TariffPlanRepository : IRepository<TariffPlan>
    {
        OracleProvider provider;

        public TariffPlanRepository()
        {
            this.provider = new OracleProvider();
        }

        public TariffPlanRepository(OracleProvider oracleProvider)
        {
            this.provider = oracleProvider;
        }

        public IEnumerable<TariffPlan> GetAll()
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@tariff_plan_cur", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

            string sql = $"TariffPlan_Package.GetAllTariffPlans";
            return provider.Connection.Query<TariffPlan>(sql, queryParameters, commandType: CommandType.StoredProcedure);
        }

        public TariffPlan Get(int id)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_id", id, OracleMappingType.Int64, ParameterDirection.Input);
            queryParameters.Add("@tariff_plan_cur", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

            string sql = $@"TariffPlan_Package.GetTariffPlanById";
            return provider.Connection.QueryFirstOrDefault<TariffPlan>(sql, queryParameters, commandType: CommandType.StoredProcedure);
        }

        public int Insert(TariffPlan item)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_tariff_name", item.TARIFF_NAME, OracleMappingType.NVarchar2, ParameterDirection.Input);
            queryParameters.Add("@par_tariff_amount", item.TARIFF_AMOUNT, OracleMappingType.BinaryFloat, ParameterDirection.Input);
            queryParameters.Add("@inserted", 0, OracleMappingType.Int64, ParameterDirection.Output);

            string sql = $@"TariffPlan_Package.InsertTariffPlan";
            provider.Connection.Query(sql, queryParameters, commandType: CommandType.StoredProcedure);
            int inserted = queryParameters.Get<int>("@inserted");
            return inserted;
        }

        public int Update(TariffPlan item)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_id", item.ID, OracleMappingType.Int64, ParameterDirection.Input);
            queryParameters.Add("@par_tariff_name", item.TARIFF_NAME, OracleMappingType.NVarchar2, ParameterDirection.Input);
            queryParameters.Add("@par_tariff_amount", item.TARIFF_AMOUNT, OracleMappingType.BinaryFloat, ParameterDirection.Input);
            queryParameters.Add("@updated", 0, OracleMappingType.Int64, ParameterDirection.Output);

            string sql = $@"TariffPlan_Package.UpdateTariffPlan";
            provider.Connection.Query(sql, queryParameters, commandType: CommandType.StoredProcedure);
            int updated = queryParameters.Get<int>("@updated");
            return updated;
        }

        public int Delete(int id)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_id", id, OracleMappingType.Int64, ParameterDirection.Input);
            queryParameters.Add("@deleted", 0, OracleMappingType.Int64, ParameterDirection.Output);

            string sql = $@"TariffPlan_Package.DeleteTariffPlan";
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
