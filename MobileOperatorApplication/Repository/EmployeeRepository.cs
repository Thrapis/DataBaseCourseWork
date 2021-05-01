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
    public class EmployeeRepository : IRepository<Employee>
    {
        OracleProvider provider;

        public EmployeeRepository()
        {
            this.provider = new OracleProvider();
        }

        public EmployeeRepository(OracleProvider oracleProvider)
        {
            this.provider = oracleProvider;
        }

        public IEnumerable<Employee> GetAll()
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@employee_cur", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

            string sql = $"Employee_Package.GetAllEmployees";
            return provider.Connection.Query<Employee>(sql, queryParameters, commandType: CommandType.StoredProcedure);
        }

        public Employee Get(int id)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_id", id, OracleMappingType.Int64, ParameterDirection.Input);
            queryParameters.Add("@employee_cur", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

            string sql = $@"Employee_Package.GetEmployeeById";
            return provider.Connection.QueryFirstOrDefault<Employee>(sql, queryParameters, commandType: CommandType.StoredProcedure);
        }

        public int Insert(Employee item)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_full_name", item.FULL_NAME, OracleMappingType.NVarchar2, ParameterDirection.Input);
            queryParameters.Add("@par_post_id", item.POST_ID, OracleMappingType.Int64, ParameterDirection.Input);
            queryParameters.Add("@par_account_login", item.ACCOUNT_LOGIN, OracleMappingType.NVarchar2, ParameterDirection.Input);
            queryParameters.Add("@inserted", 0, OracleMappingType.Int64, ParameterDirection.Output);

            string sql = $@"Employee_Package.InsertEmployee";
            provider.Connection.Query(sql, queryParameters, commandType: CommandType.StoredProcedure);
            int inserted = queryParameters.Get<int>("@inserted");
            return inserted;
        }

        public int Update(Employee item)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_id", item.ID, OracleMappingType.Int64, ParameterDirection.Input);
            queryParameters.Add("@par_full_name", item.FULL_NAME, OracleMappingType.NVarchar2, ParameterDirection.Input);
            queryParameters.Add("@par_post_id", item.POST_ID, OracleMappingType.Int64, ParameterDirection.Input);
            queryParameters.Add("@par_account_login", item.ACCOUNT_LOGIN, OracleMappingType.NVarchar2, ParameterDirection.Input);
            queryParameters.Add("@updated", 0, OracleMappingType.Int64, ParameterDirection.Output);

            string sql = $@"Employee_Package.UpdateEmployee";
            provider.Connection.Query(sql, queryParameters, commandType: CommandType.StoredProcedure);
            int updated = queryParameters.Get<int>("@updated");
            return updated;
        }

        public int Delete(int id)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_id", id, OracleMappingType.Int64, ParameterDirection.Input);
            queryParameters.Add("@deleted", 0, OracleMappingType.Int64, ParameterDirection.Output);

            string sql = $@"Employee_Package.DeleteEmployee";
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
