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

        public Post()
        {

        }

        public Post(string post_name, string category)
        {
            POST_NAME = post_name;
            CATEGORY = category;
        }

        public Post(int id, string post_name, string category)
        {
            ID = id;
            POST_NAME = post_name;
            CATEGORY = category;
        }

        public override string ToString()
        {
            return $"{ID} {POST_NAME} {CATEGORY}";
        }
    }
}
