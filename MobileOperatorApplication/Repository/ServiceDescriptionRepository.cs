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
    public class ServiceDescriptionRepository : IRepository<ServiceDescription>
    {
        OracleProvider provider;

        public ServiceDescriptionRepository()
        {
            this.provider = new OracleProvider();
        }

        public IEnumerable<ServiceDescription> GetAll()
        {
            string sql = $"select * from Service_Description";
            return provider.Connection.Query<ServiceDescription>(sql);
        }

        public ServiceDescription Get(int id)
        {
            string sql = $"select * from Service_Description where id = {id}";
            return provider.Connection.QueryFirst<ServiceDescription>(sql);
        }

        public int Insert(ServiceDescription item)
        {
            string sql = $@"insert into Service_Description (Service_Name, Service_Description) values ('{item.SERVICE_NAME}', '{item.SERVICE_DESCRIPTION}')";
            return provider.Connection.Execute(sql);
        }

        public int Update(ServiceDescription item)
        {
            string sql = $@"update Service_Description set Service_Name = '{item.SERVICE_NAME}', Service_Description = '{item.SERVICE_DESCRIPTION}' where Id = {item.ID}";
            return provider.Connection.Execute(sql);
        }

        public int Delete(int id)
        {
            string sql = $"delete from Service_Description where Id = {id}";
            return provider.Connection.Execute(sql);
        }

        public void Dispose()
        {
            provider.Connection.Close();
        }
    }
}
