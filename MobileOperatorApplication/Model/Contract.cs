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

        public Contract()
        {

        }

        public Contract(int tariff_id, int client_id, int employee_id)
        {
            TARIFF_ID = tariff_id;
            CLIENT_ID = client_id;
            EMPLOYEE_ID = employee_id;
        }

        public Contract(int id, int tariff_id, int client_id, int employee_id)
        {
            ID = id;
            TARIFF_ID = tariff_id;
            CLIENT_ID = client_id;
            EMPLOYEE_ID = employee_id;
        }

        public override string ToString()
        {
            return $"{ID} {TARIFF_ID} {CLIENT_ID} {EMPLOYEE_ID}";
        }
    }
}
