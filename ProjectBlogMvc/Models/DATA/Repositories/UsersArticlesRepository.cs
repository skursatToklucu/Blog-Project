using ProjectBlogMvc.Models.DAL.Context;
using ProjectBlogMvc.Models.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBlogMvc.Models.DATA.Repositories
{
    public class UsersArticlesRepository : IUsersArticlesRepository
    {
        private ProjectContext _context;

        public UsersArticlesRepository(ProjectContext context)
        {
            _context = context;
        }

        public IQueryable<UsersArticles> UsersArticles => _context.UsersArticles;

        public void FollowArticle(int userID, int articleID)
        {
            UsersArticles followArticle = new UsersArticles()
            {
                UserID = userID,
                ArticleID = articleID
            };

            _context.UsersArticles.Add(followArticle);
            _context.SaveChanges();
        }

        public void UnFollowArticle(int userID, int articleID)
        {
            UsersArticles unfollowArticle = _context.UsersArticles.Where(x => x.UserID == userID && x.ArticleID == articleID).FirstOrDefault();

            _context.UsersArticles.Remove(unfollowArticle);
            _context.SaveChanges();
        }
    }
}
