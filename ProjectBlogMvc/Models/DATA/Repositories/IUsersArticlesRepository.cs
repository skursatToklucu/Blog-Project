using ProjectBlogMvc.Models.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBlogMvc.Models.DATA.Repositories
{
    public interface IUsersArticlesRepository
    {
        IQueryable<UsersArticles> UsersArticles { get; }

        void FollowArticle(int userID, int articleID);

        void UnFollowArticle(int userID, int articleID);

    }
}
