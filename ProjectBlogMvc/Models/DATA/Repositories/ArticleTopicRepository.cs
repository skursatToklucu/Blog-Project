using ProjectBlogMvc.Models.DAL.Context;
using ProjectBlogMvc.Models.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBlogMvc.Models.DATA.Repositories
{
    public class ArticleTopicRepository : IArticleTopicRepository
    {
        private ProjectContext _context;

        public ArticleTopicRepository(ProjectContext context)
        {
            _context = context;
        }

        public IQueryable<ArticlesTopics> ArticlesTopics => _context.ArticlesTopics;

        public void AddTopicsToArticle(int articleID, List<int> topicIDList)
        {
            foreach (int item in topicIDList)
            {
                ArticlesTopics articleTopic = new ArticlesTopics()
                {
                    ArticleID = articleID,

                    TopicID = item
                };
                _context.ArticlesTopics.Add(articleTopic);
                _context.SaveChanges();
            }
        }

        public void RemoveTopicsFromArticle(int articleID) 
        {
            List<ArticlesTopics> deleteList = _context.ArticlesTopics.Where(x => x.ArticleID == articleID).ToList();

            foreach (var item in deleteList)
            {
                _context.ArticlesTopics.Remove(item);
                _context.SaveChanges();
            }
        }

        public void RemoveTopicFromArticle(int articleId, int topicID)
        {
            ArticlesTopics removeArticleTopic = _context.ArticlesTopics.Where(at => at.ArticleID == articleId && at.TopicID == topicID).FirstOrDefault();

            _context.ArticlesTopics.Remove(removeArticleTopic);
            _context.SaveChanges();

        }
    }
}
