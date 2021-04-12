using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileOperatorApplication.Model
{
    public class Contract
    {
        public int ID { get; set; }
        public int TARIFF_ID { get; set; }
        public int CLIENT_ID { get; set; }
        public int EMPLOYEE_ID { get; set; }
        public DateTime SIGNING_DATETIME { get; set; }

        public Contract()
        {

        }

        public Contract(int tariff_id, int client_id, int employee_id, DateTime signing_datetime)
        {
            TARIFF_ID = tariff_id;
            CLIENT_ID = client_id;
            EMPLOYEE_ID = employee_id;
            SIGNING_DATETIME = signing_datetime;
        }

        public Contract(int id, int tariff_id, int client_id, int employee_id, DateTime signing_datetime)
        {
            ID = id;
            TARIFF_ID = tariff_id;
            CLIENT_ID = client_id;
            EMPLOYEE_ID = employee_id;
            SIGNING_DATETIME = signing_datetime;
        }

        public override string ToString()
        {
            return $"{ID} {TARIFF_ID} {CLIENT_ID} {EMPLOYEE_ID} {SIGNING_DATETIME}";
        }
    }
}
