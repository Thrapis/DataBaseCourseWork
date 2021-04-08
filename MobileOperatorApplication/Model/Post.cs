using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileOperatorApplication.Model
{
    public class Post
    {
        public int ID { get; set; }
        public string POST_NAME { get; set; }
        public string CATEGORY { get; set; }

        public override string ToString()
        {
            return $"{ID} {POST_NAME} {CATEGORY}";
        }
    }
}
