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
	public class DebitRepository : IRepository<Debit>
	{
		OracleProvider provider;

		public DebitRepository()
		{
			this.provider = new OracleProvider();
		}

		public IEnumerable<Debit> GetAll()
		{
			string sql = $"select * from Debit";
			return provider.Connection.Query<Debit>(sql);
		}

		public Debit Get(int id)
		{
			string sql = $"select * from Debit where id = {id}";
			return provider.Connection.QueryFirst<Debit>(sql);
		}

		public int Insert(Debit item)
		{
			string debit_datetime_format = $"TO_TIMESTAMP({item.DEBIT_DATETIME.ToString("yyyy-MM-dd HH:mm:ss")})";

			string sql = $@"insert into Debit (Contract_Id, Debit_Amount, Debit_Datetime, Reason)" +
				$@" values ({item.CONTRACT_ID}, {item.DEBIT_AMOUNT}, {debit_datetime_format}, '{item.REASON}')";
			return provider.Connection.Execute(sql);
		}

		public int Update(Debit item)
		{
			string debit_datetime_format = $"TO_TIMESTAMP({item.DEBIT_DATETIME.ToString("yyyy-MM-dd HH:mm:ss")})";

			string sql = $@"update Debit set Contract_Id = {item.CONTRACT_ID}, Debit_Amount = {item.DEBIT_AMOUNT}," + 
				$@" Debit_Datetime = {debit_datetime_format}, Reason = '{item.REASON}' where Id = {item.ID}";
			return provider.Connection.Execute(sql);
		}

		public int Delete(int id)
		{
			string sql = $"delete from Debit where Id = {id}";
			return provider.Connection.Execute(sql);
		}

		public void Dispose()
		{
			provider.Connection.Close();
		}
	}
}
