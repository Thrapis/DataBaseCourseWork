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
    public class ClientRepository : IRepository<Client>
    {
        OracleProvider provider;

        public ClientRepository()
        {
            this.provider = new OracleProvider();
        }

        public IEnumerable<Client> GetAll()
        {
            string sql = $"select * from Client";
            return provider.Connection.Query<Client>(sql);
        }

        public Client Get(int id)
        {
            string sql = $"select * from Client where id = {id}";
            return provider.Connection.QueryFirst<Client>(sql);
        }

        public int Insert(Client item)
        {
            string sql = $@"insert into Client (Full_Name, Passport_Number) values ('{item.FULL_NAME}', '{item.PASSPORT_NUMBER}')";
            return provider.Connection.Execute(sql);
        }

        public int Update(Client item)
        {
            string sql = $@"update Client set Full_Name = '{item.FULL_NAME}', Passport_Number = '{item.PASSPORT_NUMBER}' where Id = {item.ID}";
            return provider.Connection.Execute(sql);
        }

        public int Delete(int id)
        {
            string sql = $"delete from Client where Id = {id}";
            return provider.Connection.Execute(sql);
        }

        public void Dispose()
        {
            provider.Connection.Close();
        }
    }
}
