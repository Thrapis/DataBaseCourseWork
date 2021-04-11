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
	public class ServiceRepository : IRepository<Service>
	{
		OracleProvider provider;

		public ServiceRepository()
		{
			this.provider = new OracleProvider();
		}

		public IEnumerable<Service> GetAll()
		{
			string sql = $"select * from Service";
			return provider.Connection.Query<Service>(sql);
		}

		public Service Get(int id)
		{
			string sql = $"select * from Service where id = {id}";
			return provider.Connection.QueryFirst<Service>(sql);
		}

		public int Insert(Service item)
		{
			string connection_date_format = $"TO_TIMESTAMP({item.CONNECTION_DATE.ToString("yyyy-MM-dd HH:mm:ss")})";
			string disconnection_date_format = $"TO_TIMESTAMP({item.DISCONNECTION_DATE.ToString("yyyy-MM-dd HH:mm:ss")})";

			string sql = $@"insert into Service (Contract_Id, Description_Id, Service_Amount, Connection_Date, Disconnection_Date)" +
				$@" values ({item.CONTRACT_ID}, {item.DESCRIPTION_ID}, {item.SERVICE_AMOUNT}, {connection_date_format}, {disconnection_date_format})";
			return provider.Connection.Execute(sql);
		}

		public int Update(Service item)
		{
			string connection_date_format = $"TO_TIMESTAMP({item.CONNECTION_DATE.ToString("yyyy-MM-dd HH:mm:ss")})";
			string disconnection_date_format = $"TO_TIMESTAMP({item.DISCONNECTION_DATE.ToString("yyyy-MM-dd HH:mm:ss")})";

			string sql = $@"update Service set Contract_Id = {item.CONTRACT_ID}, Description_Id = {item.DESCRIPTION_ID}," + 
				$@" Service_Amount = {item.SERVICE_AMOUNT}, Connection_Date = {connection_date_format}, Disconnection_Date = {disconnection_date_format} where Id = {item.ID}";
			return provider.Connection.Execute(sql);
		}

		public int Delete(int id)
		{
			string sql = $"delete from Service where Id = {id}";
			return provider.Connection.Execute(sql);
		}

		public void Dispose()
		{
			provider.Connection.Close();
		}
	}
}
