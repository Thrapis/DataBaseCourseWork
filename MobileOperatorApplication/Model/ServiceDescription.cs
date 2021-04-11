using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileOperatorApplication.Model
{
    public class ServiceDescription
    {
        public int ID { get; set; }
        public string SERVICE_NAME { get; set; }
        public string SERVICE_DESCRIPTION { get; set; }

        public ServiceDescription()
        {

        }

        public ServiceDescription(string service_name, string service_description)
        {
            SERVICE_NAME = service_name;
            SERVICE_DESCRIPTION = service_description;
        }

        public ServiceDescription(int id, string service_name, string service_description)
        {
            ID = id;
            SERVICE_NAME = service_name;
            SERVICE_DESCRIPTION = service_description;
        }

        public override string ToString()
        {
            return $"{ID} {SERVICE_NAME} {SERVICE_DESCRIPTION}";
        }
    }
}
