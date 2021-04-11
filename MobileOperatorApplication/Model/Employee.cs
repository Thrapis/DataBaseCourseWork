using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileOperatorApplication.Model
{
    public class Employee
    {
        public int ID { get; set; }
        public string FULL_NAME { get; set; }
        public int POST_ID { get; set; }
        public string ACCOUNT_LOGIN { get; set; }

        public Employee()
        {

        }

        public Employee(string full_name, int post_id, string account_login)
        {
            FULL_NAME = full_name;
            POST_ID = post_id;
            ACCOUNT_LOGIN = account_login;
        }

        public Employee(int id, string full_name, int post_id, string account_login)
        {
            ID = id;
            FULL_NAME = full_name;
            POST_ID = post_id;
            ACCOUNT_LOGIN = account_login;
        }

        public override string ToString()
        {
            return $"{ID} {FULL_NAME} {POST_ID} {ACCOUNT_LOGIN}";
        }
    }
}
