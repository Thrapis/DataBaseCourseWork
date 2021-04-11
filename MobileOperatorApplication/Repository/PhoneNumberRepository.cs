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
    public class PhoneNumberRepository : IRepository<PhoneNumber>
    {
        OracleProvider provider;

        public PhoneNumberRepository()
        {
            this.provider = new OracleProvider();
        }

        public IEnumerable<PhoneNumber> GetAll()
        {
            string sql = $"select * from Phone_Number";
            return provider.Connection.Query<PhoneNumber>(sql);
        }

        public PhoneNumber Get(int id)
        {
            string sql = $"select * from Phone_Number where id = {id}";
            return provider.Connection.QueryFirst<PhoneNumber>(sql);
        }

        public int Insert(PhoneNumber item)
        {
            string sql = $@"insert into Phone_Number (Phone_Number, Contract_Id) values ('{item.PHONE_NUMBER}', {item.CONTRACT_ID})";
            return provider.Connection.Execute(sql);
        }

        public int Update(PhoneNumber item)
        {
            string sql = $@"update Phone_Number set Phone_Number = '{item.PHONE_NUMBER}', Contract_Id = {item.CONTRACT_ID} where Id = {item.ID}";
            return provider.Connection.Execute(sql);
        }

        public int Delete(int id)
        {
            string sql = $"delete from Phone_Number where Id = {id}";
            return provider.Connection.Execute(sql);
        }

        public void Dispose()
        {
            provider.Connection.Close();
        }
    }
}
