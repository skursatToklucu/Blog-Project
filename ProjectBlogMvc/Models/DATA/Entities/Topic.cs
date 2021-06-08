using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBlogMvc.Models.DATA.Entities
{
    public class Topic 
    {

        public int ID { get; set; }

        public string Name { get; set; }

        public int HitRate { get; set; }

             
        public virtual ICollection<ArticlesTopics> ArticlesTopics { get; set; }

        public virtual ICollection<UsersTopics> UsersTopics { get; set; }
    }
}
