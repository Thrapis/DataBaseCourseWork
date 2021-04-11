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
	public class PaymentRepository : IRepository<Payment>
	{
		OracleProvider provider;

		public PaymentRepository()
		{
			this.provider = new OracleProvider();
		}

		public IEnumerable<Payment> GetAll()
		{
			string sql = $"select * from Payment";
			return provider.Connection.Query<Payment>(sql);
		}

		public Payment Get(int id)
		{
			string sql = $"select * from Payment where id = {id}";
			return provider.Connection.QueryFirst<Payment>(sql);
		}

		public int Insert(Payment item)
		{
			string payment_datetime_format = $"TO_TIMESTAMP({item.PAYMENT_DATETIME.ToString("yyyy-MM-dd HH:mm:ss")})";

			string sql = $@"insert into Payment (Contract_Id, Payment_Amount, Payment_Datetime)" +
				$@" values ({item.CONTRACT_ID}, {item.PAYMENT_AMOUNT}, {payment_datetime_format})";
			return provider.Connection.Execute(sql);
		}

		public int Update(Payment item)
		{
			string payment_datetime_format = $"TO_TIMESTAMP({item.PAYMENT_DATETIME.ToString("yyyy-MM-dd HH:mm:ss")})";

			string sql = $@"update Payment set Contract_Id = {item.CONTRACT_ID}, Payment_Amount = {item.PAYMENT_AMOUNT}," + 
				$@" Payment_Datetime = {payment_datetime_format} where Id = {item.ID}";
			return provider.Connection.Execute(sql);
		}

		public int Delete(int id)
		{
			string sql = $"delete from Payment where Id = {id}";
			return provider.Connection.Execute(sql);
		}

		public void Dispose()
		{
			provider.Connection.Close();
		}
	}
}
