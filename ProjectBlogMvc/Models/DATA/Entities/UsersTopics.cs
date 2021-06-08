using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBlogMvc.Models.DATA.Entities
{
    public class UsersTopics
    {

        public int UserID { get; set; }

        public int TopicID { get; set; }

        public User User { get; set; }

        public Topic Topic { get; set; }

    }
}
