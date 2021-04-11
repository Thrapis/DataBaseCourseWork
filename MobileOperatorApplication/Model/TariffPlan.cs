using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileOperatorApplication.Model
{
    public class TariffPlan
    {
        public int ID { get; set; }
        public string TARIFF_NAME { get; set; }
        public float TARIFF_AMOUNT { get; set; }

        public TariffPlan()
        {

        }

        public TariffPlan(string tariff_name, float tariff_amount)
        {
            TARIFF_NAME = tariff_name;
            TARIFF_AMOUNT = tariff_amount;
        }

        public TariffPlan(int id, string tariff_name, float tariff_amount)
        {
            ID = id;
            TARIFF_NAME = tariff_name;
            TARIFF_AMOUNT = tariff_amount;
        }

        public override string ToString()
        {
            return $"{ID} {TARIFF_NAME} {TARIFF_AMOUNT}";
        }
    }
}
