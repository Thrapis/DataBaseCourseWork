using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileOperatorApplication.Model
{
    public class Call
    {
        public int ID { get; set; }
        public int CONTRACT_ID { get; set; }
        public string TO_PHONE_NUMBER { get; set; }
        public DateTime TALK_TIME { get; set; }
        public DateTime CALL_DATETIME { get; set; }

        public Call()
        {

        }

        public Call(int contract_id, string to_phone_number, DateTime talk_time, DateTime call_datetime)
        {
            CONTRACT_ID = contract_id;
            TO_PHONE_NUMBER = to_phone_number;
            TALK_TIME = talk_time;
            CALL_DATETIME = call_datetime;
        }

        public Call(int id, int contract_id, string to_phone_number, DateTime talk_time, DateTime call_datetime)
        {
            ID = id;
            CONTRACT_ID = contract_id;
            TO_PHONE_NUMBER = to_phone_number;
            TALK_TIME = talk_time;
            CALL_DATETIME = call_datetime;
        }

        public override string ToString()
        {
            return $"{ID} {CONTRACT_ID} {TO_PHONE_NUMBER} {TALK_TIME} {CALL_DATETIME}";
        }
    }
}
