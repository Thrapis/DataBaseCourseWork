using MobileOperatorApplication.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MobileOperatorApplication.Oracle;
using System.Data;
using Dapper.Oracle;

namespace MobileOperatorApplication.Repository
{
    public class PostRepository : IRepository<Post>
    {
        OracleProvider provider;

        public PostRepository()
        {
            this.provider = new OracleProvider();
        }

        public IEnumerable<Post> GetAll()
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@post_cur", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

            string sql = $"Post_Package.GetAllPosts";
            return provider.Connection.Query<Post>(sql, queryParameters, commandType: CommandType.StoredProcedure);
        }

        public Post Get(int id)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_id", id, OracleMappingType.Int64, ParameterDirection.Input);

            string sql = $@"Post_Package.GetPostById";
            return provider.Connection.QueryFirst<Post>(sql, queryParameters);
        }

        public int Insert(Post item)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_post_name", item.POST_NAME, OracleMappingType.NVarchar2, ParameterDirection.Input);
            queryParameters.Add("@par_category", item.CATEGORY, OracleMappingType.NVarchar2, ParameterDirection.Input);
            queryParameters.Add("@inserted", 0, OracleMappingType.Int64, ParameterDirection.Output);

            string sql = $@"Post_Package.InsertPost";
            provider.Connection.Query(sql, queryParameters, commandType: CommandType.StoredProcedure);
            int inserted = queryParameters.Get<int>("@inserted");
            return inserted;
        }

        public int Update(Post item)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_id", item.ID, OracleMappingType.Int64, ParameterDirection.Input);
            queryParameters.Add("@par_post_name", item.POST_NAME, OracleMappingType.NVarchar2, ParameterDirection.Input);
            queryParameters.Add("@par_category", item.CATEGORY, OracleMappingType.NVarchar2, ParameterDirection.Input);
            queryParameters.Add("@updated", 0, OracleMappingType.Int64, ParameterDirection.Output);

            string sql = $@"Post_Package.UpdatePost";
            provider.Connection.Query(sql, queryParameters, commandType: CommandType.StoredProcedure);
            int updated = queryParameters.Get<int>("@updated");
            return updated;
        }

        public int Delete(int id)
        {
            OracleDynamicParameters queryParameters = new OracleDynamicParameters();
            queryParameters.Add("@par_id", id, OracleMappingType.Int64, ParameterDirection.Input);
            queryParameters.Add("@deleted", 0, OracleMappingType.Int64, ParameterDirection.Output);

            string sql = $@"Post_Package.DeletePost";
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
