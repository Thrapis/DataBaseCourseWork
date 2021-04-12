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
    public class ContractRepository : IRepository<Contract>
    {
        OracleProvider provider;

        public ContractRepository()
        {
            this.provider = new OracleProvider();
        }

        public IEnumerable<Contract> GetAll()
        {
            string sql = $"select * from Contract";
            return provider.Connection.Query<Contract>(sql);
        }

        public Contract Get(int id)
        {
            string sql = $"select * from Contract where id = {id}";
            return provider.Connection.QueryFirst<Contract>(sql);
        }

        public int Insert(Contract item)
        {
            string signing_datetime_format = $"TO_TIMESTAMP({item.SIGNING_DATETIME.ToString("yyyy-MM-dd HH:mm:ss")})";

            string sql = $@"insert into Contract (Tariff_Id, Client_Id, Employee_Id, Signing_Datetime) values ({item.TARIFF_ID}, {item.CLIENT_ID}, {item.EMPLOYEE_ID}, {signing_datetime_format})";
            return provider.Connection.Execute(sql);
        }

        public int Update(Contract item)
        {
            string signing_datetime_format = $"TO_TIMESTAMP({item.SIGNING_DATETIME.ToString("yyyy-MM-dd HH:mm:ss")})";

            string sql = $@"update Contract set Tariff_Id = {item.TARIFF_ID}, Client_Id = {item.CLIENT_ID}, Employee_Id = {item.EMPLOYEE_ID}, Signing_Datetime = {signing_datetime_format} where Id = {item.ID}";
            return provider.Connection.Execute(sql);
        }

        public int Delete(int id)
        {
            string sql = $"delete from Contract where Id = {id}";
            return provider.Connection.Execute(sql);
        }

        public void Dispose()
        {
            provider.Connection.Close();
        }
    }
}
