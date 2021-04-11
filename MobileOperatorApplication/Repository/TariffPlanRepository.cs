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
    public class TariffPlanRepository : IRepository<TariffPlan>
    {
        OracleProvider provider;

        public TariffPlanRepository()
        {
            this.provider = new OracleProvider();
        }

        public IEnumerable<TariffPlan> GetAll()
        {
            string sql = $"select * from Tariff_Plan";
            return provider.Connection.Query<TariffPlan>(sql);
        }

        public TariffPlan Get(int id)
        {
            string sql = $"select * from Tariff_Plan where id = {id}";
            return provider.Connection.QueryFirst<TariffPlan>(sql);
        }

        public int Insert(TariffPlan item)
        {
            string sql = $@"insert into Tariff_Plan (Tariff_Name, Tariff_Amount) values ('{item.TARIFF_NAME}', {item.TARIFF_AMOUNT})";
            return provider.Connection.Execute(sql);
        }

        public int Update(TariffPlan item)
        {
            string sql = $@"update Tariff_Plan set Tariff_Name = '{item.TARIFF_NAME}', Tariff_Amount = {item.TARIFF_AMOUNT} where Id = {item.ID}";
            return provider.Connection.Execute(sql);
        }

        public int Delete(int id)
        {
            string sql = $"delete from Tariff_Plan where Id = {id}";
            return provider.Connection.Execute(sql);
        }

        public void Dispose()
        {
            provider.Connection.Close();
        }
    }
}
