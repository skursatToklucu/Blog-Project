using ProjectBlogMvc.Models.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBlogMvc.Models.DATA.Repositories
{
   public interface ITopicRepository
    {
        IQueryable<Topic> Topics { get; }

        bool Insert(Topic topic);

        bool Update(Topic topic);

        bool Delete(int id);

        Topic GetById(int id);
    }
}
