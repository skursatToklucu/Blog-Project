using ProjectBlogMvc.Models.DAL.Context;
using ProjectBlogMvc.Models.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBlogMvc.Models.DATA.Repositories
{
    public class UserTopicRepository : IUserTopicRepository
    {
        private ProjectContext _context;

        public UserTopicRepository(ProjectContext context)
        {
            _context = context;
        }

        public IQueryable<UsersTopics> UsersTopics => _context.UsersTopics;


        public void FollowTopic(int userID, int topicID)
        {
            UsersTopics followTopic = new UsersTopics()
            {
                UserID = userID,
                TopicID = topicID
            };

            _context.UsersTopics.Add(followTopic);
            _context.SaveChanges();
        }

        public void UnfollowTopic(int userID, int topicID)
        {
            UsersTopics unfollowTopic = _context.UsersTopics.Where(ut => ut.UserID == userID && ut.TopicID == topicID).FirstOrDefault();

            _context.UsersTopics.Remove(unfollowTopic);
            _context.SaveChanges();
        }
    }
}
