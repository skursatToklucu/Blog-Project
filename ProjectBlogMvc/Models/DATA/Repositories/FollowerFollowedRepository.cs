using ProjectBlogMvc.Models.DAL.Context;
using ProjectBlogMvc.Models.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBlogMvc.Models.DATA.Repositories
{
    public class FollowerFollowedRepository : IFollowerFollowedRepository
    {
        private ProjectContext _context;

        public FollowerFollowedRepository(ProjectContext context)
        {
            _context = context;
        }

        public IQueryable<FollowerFollowed> FollowerFolloweds => _context.FollowerFolloweds;

        public void FollowWriter(int userID, int followingID)
        {
            FollowerFollowed followWriter = new FollowerFollowed()
            {
                FollowerID = userID,
                FollowedID = followingID
            };

            _context.FollowerFolloweds.Add(followWriter);
            _context.SaveChanges();
        }


        public void UnFollowWriter(int userID, int followingID)
        {
            FollowerFollowed unfollowWriter = _context.FollowerFolloweds.Where(ff => ff.FollowerID == userID && ff.FollowedID == followingID).FirstOrDefault();

            _context.FollowerFolloweds.Remove(unfollowWriter);
            _context.SaveChanges();
        }
    }
}

