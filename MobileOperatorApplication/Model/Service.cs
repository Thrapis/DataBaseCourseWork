using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileOperatorApplication.Model
{
    public class Service
    {
        public int ID { get; set; }
        public int CONTRACT_ID { get; set; }
        public int DESCRIPTION_ID { get; set; }
        public float SERVICE_AMOUNT { get; set; }
        public DateTime CONNECTION_DATE { get; set; }
        public DateTime DISCONNECTION_DATE { get; set; }

        public Service()
        {

        }

        public Service(int contract_id, int description_id, float service_amount, DateTime connection_date, DateTime disconnection_date)
        {
            CONTRACT_ID = contract_id;
            DESCRIPTION_ID = description_id;
            SERVICE_AMOUNT = service_amount;
            CONNECTION_DATE = connection_date;
            DISCONNECTION_DATE = disconnection_date;
        }

        public Service(int id, int contract_id, int description_id, float service_amount, DateTime connection_date, DateTime disconnection_date)
        {
            ID = id;
            CONTRACT_ID = contract_id;
            DESCRIPTION_ID = description_id;
            SERVICE_AMOUNT = service_amount;
            CONNECTION_DATE = connection_date;
            DISCONNECTION_DATE = disconnection_date;
        }

        public override string ToString()
        {
            return $"{ID} {CONTRACT_ID} {DESCRIPTION_ID} {SERVICE_AMOUNT} {CONNECTION_DATE} {DISCONNECTION_DATE}";
        }
    }
}
