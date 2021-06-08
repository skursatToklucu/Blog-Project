using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBlogMvc.Models.DATA.Entities
{
    public class FollowerFollowed
    {
        //[Key, Column(Order = 0)]
        public int FollowerID { get; set; }
        //[Key, Column(Order = 1)]
        public int FollowedID { get; set; }

    }
}
