using MobileOperatorApplication.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MobileOperatorApplication.Oracle;

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
            string sql = $"select * from Post";
            return provider.Connection.Query<Post>(sql);
        }

        public Post Get(int id)
        {
            string sql = $"select * from Post where id = {id}";
            return provider.Connection.QueryFirst<Post>(sql);
        }

        public int Insert(Post item)
        {
            string sql = $@"insert into Post (Post_Name, Category) values ('{item.POST_NAME}', '{item.CATEGORY}')";
            return provider.Connection.Execute(sql);
        }

        public int Update(Post item)
        {
            string sql = $@"update Post set Post_Name = '{item.POST_NAME}', Category = '{item.CATEGORY}' where Id = {item.ID}";
            return provider.Connection.Execute(sql);
        }

        public int Delete(int id)
        {
            string sql = $"delete from Post where Id = {id}";
            return provider.Connection.Execute(sql);
        }

        public void Dispose()
        {
            provider.Connection.Close();
        }
    }
}
