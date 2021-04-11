using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileOperatorApplication.Model
{
    public class PhoneNumber
    {
        public int ID { get; set; }
        public string PHONE_NUMBER { get; set; }
        public int CONTRACT_ID { get; set; }

        public PhoneNumber()
        {

        }

        public PhoneNumber(string phone_number, int contract_id)
        {
            PHONE_NUMBER = phone_number;
            CONTRACT_ID = contract_id;
        }

        public PhoneNumber(int id, string phone_number, int contract_id)
        {
            ID = id;
            PHONE_NUMBER = phone_number;
            CONTRACT_ID = contract_id;
        }

        public override string ToString()
        {
            return $"{ID} {PHONE_NUMBER} {CONTRACT_ID}";
        }
    }
}
