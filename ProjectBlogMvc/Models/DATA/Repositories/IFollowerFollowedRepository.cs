using ProjectBlogMvc.Models.DAL.Context;
using ProjectBlogMvc.Models.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBlogMvc.Models.DATA.Repositories
{
    public interface IFollowerFollowedRepository
    {


        IQueryable<FollowerFollowed> FollowerFolloweds { get; }


        void FollowWriter(int userID, int followingID);

        void UnFollowWriter(int userID, int followingID);
      

    }
}
