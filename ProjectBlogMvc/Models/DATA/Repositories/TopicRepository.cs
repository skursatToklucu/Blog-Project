using ProjectBlogMvc.Models.DAL.Context;
using ProjectBlogMvc.Models.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBlogMvc.Models.DATA.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private ProjectContext _context;

        public TopicRepository(ProjectContext context)
        {
            _context = context;
        }
        public IQueryable<Topic> Topics => _context.Topics;

        public bool Delete(int id)
        {
            _context.Topics.Remove(GetById(id));
            return _context.SaveChanges() > 0;
        }

        public Topic GetById(int id)
        {
            return _context.Topics.Find(id);
        }

        public bool Insert(Topic topic)
        {
            _context.Topics.Add(topic);
            return _context.SaveChanges() > 0;
        }

        public bool Update(Topic topic)
        {
            _context.Topics.Update(topic);
            return _context.SaveChanges() > 0;
        }
    }
}
