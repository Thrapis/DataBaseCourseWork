using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileOperatorApplication.Model
{
    public class Payment
    {
        public int ID { get; set; }
        public int CONTRACT_ID { get; set; }
        public float PAYMENT_AMOUNT { get; set; }
        public DateTime PAYMENT_DATETIME { get; set; }

        public Payment()
        {

        }

        public Payment(int contract_id, float payment_amount, DateTime payment_datetime)
        {
            CONTRACT_ID = contract_id;
            PAYMENT_AMOUNT = payment_amount;
            PAYMENT_DATETIME = payment_datetime;
        }

        public Payment(int id, int contract_id, float payment_amount, DateTime payment_datetime)
        {
            ID = id;
            CONTRACT_ID = contract_id;
            PAYMENT_AMOUNT = payment_amount;
            PAYMENT_DATETIME = payment_datetime;
        }

        public override string ToString()
        {
            return $"{ID} {CONTRACT_ID} {PAYMENT_AMOUNT} {PAYMENT_DATETIME}";
        }
    }
}
