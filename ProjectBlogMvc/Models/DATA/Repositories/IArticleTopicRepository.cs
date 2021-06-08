using ProjectBlogMvc.Models.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBlogMvc.Models.DATA.Repositories
{
    public interface IArticleTopicRepository
    {
        IQueryable<ArticlesTopics> ArticlesTopics { get; }

        void AddTopicsToArticle(int articleID, List<int> topicID);
        void RemoveTopicsFromArticle(int articleID);
        void RemoveTopicFromArticle(int articleId, int topicID);
    }
}
