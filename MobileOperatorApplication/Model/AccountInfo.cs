using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileOperatorApplication.Model
{
    public class AccountInfo
    {
        public string LOGIN { get; set; }
        public int ACCESS_LEVEL { get; set; }

        public AccountInfo() { }

        public AccountInfo(string login, int access_level)
        {
            LOGIN = login;
            ACCESS_LEVEL = access_level;
        }

        public override string ToString()
        {
            return $"{LOGIN} {ACCESS_LEVEL}";
        }
    }
}
