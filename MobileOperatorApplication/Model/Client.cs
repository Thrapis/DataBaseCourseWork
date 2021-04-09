using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileOperatorApplication.Model
{
    public class Client
    {
        public int ID { get; set; }
        public string FULL_NAME { get; set; }
        public string PASSPORT_NUMBER { get; set; }

        public Client()
        {

        }

        public Client(string full_name, string password_number)
        {
            FULL_NAME = full_name;
            PASSPORT_NUMBER = password_number;
        }

        public Client(int id, string full_name, string password_number)
        {
            ID = id;
            FULL_NAME = full_name;
            PASSPORT_NUMBER = password_number;
        }

        public override string ToString()
        {
            return $"{ID} {FULL_NAME} {PASSPORT_NUMBER}";
        }
    }
}
