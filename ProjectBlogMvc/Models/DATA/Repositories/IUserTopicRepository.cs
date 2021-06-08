using ProjectBlogMvc.Models.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBlogMvc.Models.DATA.Repositories
{
   public interface IUserTopicRepository
    {
        IQueryable <UsersTopics>  UsersTopics { get; }

        void FollowTopic(int userID, int konuID);

        void UnfollowTopic(int userID, int konuID);
    }
}
