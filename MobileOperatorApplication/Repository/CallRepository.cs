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
	public class CallRepository : IRepository<Call>
	{
		OracleProvider provider;

		public CallRepository()
		{
			this.provider = new OracleProvider();
		}

		public IEnumerable<Call> GetAll()
		{
			string sql = $"select * from Call";
			return provider.Connection.Query<Call>(sql);
		}

		public Call Get(int id)
		{
			string sql = $"select * from Call where id = {id}";
			return provider.Connection.QueryFirst<Call>(sql);
		}

		public int Insert(Call item)
		{
			string talk_time_format = $@"INTERVAL '{item.TALK_TIME.ToString("dd HH:mm:ss")}' DAY TO SECOND";
			string call_datetime_format = $"TO_TIMESTAMP({item.CALL_DATETIME.ToString("yyyy-MM-dd HH:mm:ss")})";

			string sql = $@"insert into Call (Contract_Id, To_Phone_Number, Talk_Time, Call_Datetime)" +
				$@" values ({item.CONTRACT_ID}, '{item.TO_PHONE_NUMBER}', {talk_time_format}, {call_datetime_format})";
			return provider.Connection.Execute(sql);
		}

		public int Update(Call item)
		{
			string talk_time_format = $@"INTERVAL '{item.TALK_TIME.ToString("dd HH:mm:ss")}' DAY TO SECOND";
			string call_datetime_format = $"TO_TIMESTAMP({item.CALL_DATETIME.ToString("yyyy-MM-dd HH:mm:ss")})";

			string sql = $@"update Call set Contract_Id = {item.CONTRACT_ID}, To_Phone_Number = '{item.TO_PHONE_NUMBER}'," + 
				$@" Talk_Time = {talk_time_format}, Call_Datetime = {call_datetime_format} where Id = {item.ID}";
			return provider.Connection.Execute(sql);
		}

		public int Delete(int id)
		{
			string sql = $"delete from Call where Id = {id}";
			return provider.Connection.Execute(sql);
		}

		public void Dispose()
		{
			provider.Connection.Close();
		}
	}
}
