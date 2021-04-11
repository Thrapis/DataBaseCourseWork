using MobileOperatorApplication.Model;
using Oracle.ManagedDataAccess.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MobileOperatorApplication.Oracle;

namespace MobileOperatorApplication.Repository
{
    public class EmployeeRepository : IRepository<Employee>
    {
        OracleProvider provider;

        public EmployeeRepository()
        {
            this.provider = new OracleProvider();
        }

        public IEnumerable<Employee> GetAll()
        {
            string sql = $"select * from Employee";
            return provider.Connection.Query<Employee>(sql);
        }

        public Employee Get(int id)
        {
            string sql = $"select * from Employee where id = {id}";
            return provider.Connection.QueryFirst<Employee>(sql);
        }

        public int Insert(Employee item)
        {
            string sql = $@"insert into Employee (Full_Name, Post_Id, Account_Login) values ('{item.FULL_NAME}', {item.POST_ID}, '{item.ACCOUNT_LOGIN}')";
            return provider.Connection.Execute(sql);
        }

        public int Update(Employee item)
        {
            string sql = $@"update Employee set Full_Name = '{item.FULL_NAME}', Post_Id = {item.POST_ID}, Account_Login = '{item.ACCOUNT_LOGIN}' where Id = {item.ID}";
            return provider.Connection.Execute(sql);
        }

        public int Delete(int id)
        {
            string sql = $"delete from Employee where Id = {id}";
            return provider.Connection.Execute(sql);
        }

        public void Dispose()
        {
            provider.Connection.Close();
        }
    }
}
