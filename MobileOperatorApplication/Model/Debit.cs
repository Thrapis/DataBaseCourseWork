using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileOperatorApplication.Model
{
    public class Debit
    {
        public int ID { get; set; }
        public int CONTRACT_ID { get; set; }
        public float DEBIT_AMOUNT { get; set; }
        public DateTime DEBIT_DATETIME { get; set; }
        public string REASON { get; set; }

        public Debit()
        {

        }

        public Debit(int contract_id, float debit_amount, DateTime debit_datetime, string reason)
        {
            CONTRACT_ID = contract_id;
            DEBIT_AMOUNT = debit_amount;
            DEBIT_DATETIME = debit_datetime;
            REASON = reason;
        }

        public Debit(int id, int contract_id, float debit_amount, DateTime debit_datetime, string reason)
        {
            ID = id;
            CONTRACT_ID = contract_id;
            DEBIT_AMOUNT = debit_amount;
            DEBIT_DATETIME = debit_datetime;
            REASON = reason;
        }

        public override string ToString()
        {
            return $"{ID} {CONTRACT_ID} {DEBIT_AMOUNT} {DEBIT_DATETIME} {REASON}";
        }
    }
}
