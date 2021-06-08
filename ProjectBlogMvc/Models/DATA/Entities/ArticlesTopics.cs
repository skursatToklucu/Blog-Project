using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBlogMvc.Models.DATA.Entities
{
    public class ArticlesTopics
    {
        public int ArticleID { get; set; }
        public int TopicID { get; set; }

        public Article Article { get; set; }

        public Topic Topic { get; set; }
    }
}
