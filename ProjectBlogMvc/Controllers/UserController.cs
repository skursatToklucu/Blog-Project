using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using ProjectBlogMvc.Models.DAL.Context;
using ProjectBlogMvc.Models.DATA.Entities;
using ProjectBlogMvc.Models.DATA.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBlogMvc.Controllers
{

    public class UserController : Controller
    {
        private IUserRepository _repository;
        private IArticleRepository _articleRepository;
        private ITopicRepository _topicRepository;
        private IUserTopicRepository _userTopicRepository;
        private IArticleTopicRepository _articleTopicRepository;
        private IFollowerFollowedRepository _followerFollowedRepository;
        private IUsersArticlesRepository _usersArticlesRepository;

        public UserController(IUserRepository repository, IArticleRepository articleRepository, ITopicRepository topicRepository, IUserTopicRepository userTopicRepository, IArticleTopicRepository articleTopicRepository, IFollowerFollowedRepository followerFollowedRepository, IUsersArticlesRepository usersArticlesRepository)
        {
            _repository = repository;
            _articleRepository = articleRepository;
            _topicRepository = topicRepository;
            _userTopicRepository = userTopicRepository;
            _articleTopicRepository = articleTopicRepository;
            _followerFollowedRepository = followerFollowedRepository;
            _usersArticlesRepository = usersArticlesRepository;
        }


        [Route("/User/Session/@{url}/{guid}")]
        public IActionResult Session(string url, Guid guid)
        {

            if (guid == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string email = _repository.Users.Where(x => x.URL == url).Select(x => x.Email).FirstOrDefault();

                HttpContext.Session.SetString("getEmail", email);
                HttpContext.Session.SetString("guid", guid.ToString());
                string Url = string.Format("~/User/Index/@{0}", url);
                return Redirect(Url);
            }
        }



        [Route("/User/Index/@{url}")]
        public IActionResult Index(string url)
        {

            if (HttpContext.Session.GetString("guid") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            string email = _repository.Users.Where(x => x.URL == url).Select(x => x.Email).FirstOrDefault();



            ViewBag.Email = email;

            //HttpContext.Session.SetString("getEmail", email);

            string bagUrl = string.Format("~/User/Index/@{0}", url);

            HttpContext.Session.SetString("getUrl", bagUrl);
            HttpContext.Session.SetString("url", url);

            ViewBag.bagUrl = bagUrl;
            ViewBag.url = url;

            int id = _repository.Users.Where(x => x.Email == email).Select(x => x.ID).FirstOrDefault();

            HttpContext.Session.SetString("getID", id.ToString());

            List<Article> articles = _articleRepository.Articles.ToList();
            List<User> users = _repository.Users.ToList();
            List<Topic> topics = _topicRepository.Topics.ToList();
            List<UsersTopics> usersTopics = _userTopicRepository.UsersTopics.ToList();
            List<FollowerFollowed> followerFolloweds = _followerFollowedRepository.FollowerFolloweds.ToList();
            List<ArticlesTopics> articlesTopics = _articleTopicRepository.ArticlesTopics.ToList();
            List<UsersArticles> usersArticles = _usersArticlesRepository.UsersArticles.ToList();


            return View(Tuple.Create<List<User>, List<Article>, List<Topic>, List<UsersTopics>, List<FollowerFollowed>, List<ArticlesTopics>, List<UsersArticles>>(users, articles, topics, usersTopics, followerFolloweds, articlesTopics, usersArticles));
        }


        [HttpPost]
        public IActionResult Index()
        {
            //email = ((User)TempData["User"]).Email;
            //email = HttpContext.Session.GetString("email");
            //int id = _repository.Users.Where(x => x.Email == email).Select(x => x.ID).FirstOrDefault();

            //List<User> users = _repository.Users.ToList();
            //List<Article> articles = _articleRepository.Articles.Where(x => x.ID == id).ToList();
            //List<Topic> topics = _topicRepository.Topics.Where(x => x.ID == id).ToList();
            //ViewBag.email = email;
            return View();
        }

        [Route("/User/LikeArticle/{id}")]
        public IActionResult LikeArticle(int id)
        {

            if (HttpContext.Session.GetString("guid") == null)
            {
                return RedirectToAction("Index", "Home");
            }


            Article article = _articleRepository.Articles.Where(a => a.ID == id).FirstOrDefault();

            article.HitRate++;

            _articleRepository.Update(article);


            string email = HttpContext.Session.GetString("getEmail");

            string url = HttpContext.Session.GetString("getUrl");

            return Redirect(url);

        }


        [Route("/User/FollowTopic/{id}")]
        public IActionResult FollowTopic(int id)
        {

            if (HttpContext.Session.GetString("guid") == null)
            {
                return RedirectToAction("Index", "Home");
            }


            _userTopicRepository.FollowTopic(Convert.ToInt32(HttpContext.Session.GetString("getID")), id);


            string email = HttpContext.Session.GetString("getEmail");

            string url = HttpContext.Session.GetString("getUrl");

            return RedirectToAction("AllTopics", "User");
        }

        [Route("/User/UnfollowTopic/{id}")]
        public IActionResult UnfollowTopic(int id)
        {

            if (HttpContext.Session.GetString("guid") == null)
            {
                return RedirectToAction("Index", "Home");
            }


            _userTopicRepository.UnfollowTopic(Convert.ToInt32(HttpContext.Session.GetString("getID")), id);

            string email = HttpContext.Session.GetString("getEmail");

            string url = HttpContext.Session.GetString("getUrl");

            return RedirectToAction("AllTopics","User");

        }

        // GET: UserController/Details/5
        public IActionResult Details(int id)
        {

            if (HttpContext.Session.GetString("guid") == null)
            {
                return RedirectToAction("Index", "Home");
            }


            User user = _repository.GetById(id);
            ViewBag.ID = id;
            return View(user);
        }



        public IActionResult Post(int id)
        {

            if (HttpContext.Session.GetString("guid") == null)
            {
                return RedirectToAction("Index", "Home");
            }


            ViewBag.Email = HttpContext.Session.GetString("getEmail");
            ViewBag.url = HttpContext.Session.GetString("url");

            Article article = _articleRepository.Articles.Where(a => a.ID == id).FirstOrDefault();
            int getUserID = _articleRepository.Articles.Where(a => a.ID == id).Select(u => u.UserID).FirstOrDefault();
            User user = _repository.Users.Where(u => u.ID == getUserID).FirstOrDefault();
            int getTopicID = _articleTopicRepository.ArticlesTopics.Where(a => a.ArticleID == id).Select(t => t.TopicID).FirstOrDefault();
            List<Topic> topics = _topicRepository.Topics.ToList();
            article.HitRate++;
            _articleRepository.Update(article);
            List<Article> articleAll = _articleRepository.Articles.ToList();
            List<ArticlesTopics> topicAll = _articleTopicRepository.ArticlesTopics.ToList();


            return View(Tuple.Create<Article, User, List<Topic>, List<Article>, List<ArticlesTopics>>(article, user, topics, articleAll, topicAll));
        }


        #region Create

        // GET: UserController/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            try
            {
                _repository.Insert(user);
                return RedirectToAction("List", "User");
            }
            catch
            {
                return View(user);
            }
        }

        #endregion


        // GET: UserController/Edit/5
        public IActionResult Edit()
        {

            if (HttpContext.Session.GetString("guid") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            int id = Convert.ToInt32(HttpContext.Session.GetString("getID"));
            ViewBag.url = HttpContext.Session.GetString("url");
            User user = _repository.GetById(id);

            return View(user);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User user)
        {

            if (HttpContext.Session.GetString("guid") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            string email = HttpContext.Session.GetString("getEmail");
            ViewBag.url = HttpContext.Session.GetString("url");
            string url = HttpContext.Session.GetString("getUrl");

            try
            {
                _repository.Update(user);
                return Redirect(url);
            }
            catch
            {
                return View(user);
            }
        }

        // GET: UserController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {

            if (HttpContext.Session.GetString("guid") == null)
            {
                return RedirectToAction("Index", "Home");
            }


            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public IActionResult List() => View(_repository.Users);


        public IActionResult LogOut()
        {

            if (HttpContext.Session.GetString("guid") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            HttpContext.Session.Remove(HttpContext.Session.GetString("guid"));
            return RedirectToAction("Index", "Home");
        }


        [Route("~/@{url}/{id}")]
        public IActionResult WriterProfile(int id)
        {

            if (HttpContext.Session.GetString("guid") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            User user = _repository.Users.Where(x => x.ID == id).FirstOrDefault();

            user.HitRate++;
            _repository.Update(user);

            List<Article> articles = _articleRepository.Articles.Where(x => x.UserID == id).ToList();
            int getArticleID = _articleRepository.Articles.Where(x => x.UserID == id).Select(x => x.ID).FirstOrDefault();

            List<Topic> relatedTopics = new List<Topic>();

            foreach (var item in _articleTopicRepository.ArticlesTopics.Where(x => x.ArticleID == getArticleID).ToList())
            {
                Topic topics = _topicRepository.Topics.Where(x => x.ID == item.TopicID).FirstOrDefault();
                relatedTopics.Add(topics);
            }

            ViewBag.RelatedTopics = relatedTopics;
            ViewBag.Articles = articles;
            ViewBag.url = HttpContext.Session.GetString("url");
            string url = HttpContext.Session.GetString("url");

            int userID = _repository.Users.Where(x => x.URL == url).Select(x => x.ID).FirstOrDefault();

            bool IsFollow = _followerFollowedRepository.FollowerFolloweds.Where(x => x.FollowerID == userID && x.FollowedID == id).FirstOrDefault() != null;

            ViewBag.IsFollow = IsFollow;


            return View(user);
        }


        public IActionResult FollowWriter(string url, int id)
        {

            if (HttpContext.Session.GetString("guid") == null)
            {
                return RedirectToAction("Index", "Home");
            }


            int followerID = _repository.Users.Where(x => x.URL == url).Select(x => x.ID).FirstOrDefault();

            _followerFollowedRepository.FollowWriter(followerID, id);
            string setUrl = string.Format("~/@{0}/{1}", url, id);

            return Redirect(setUrl);
        }

        public IActionResult UnFollowWriter(string url, int id)
        {


            if (HttpContext.Session.GetString("guid") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            int followerID = _repository.Users.Where(x => x.URL == url).Select(x => x.ID).FirstOrDefault();

            _followerFollowedRepository.UnFollowWriter(followerID, id);
            string setUrl = string.Format("~/@{0}/{1}", url, id);
            return Redirect(setUrl);

        }


        public IActionResult AllTopics()
        {
            List<Topic> topics = _topicRepository.Topics.ToList();
            List<UsersTopics> usersTopics = _userTopicRepository.UsersTopics.ToList();

            int getUserID = _repository.Users.Where(x => x.URL == HttpContext.Session.GetString("url")).Select(x => x.ID).FirstOrDefault();

            ViewBag.UserID = getUserID;
            ViewBag.url = HttpContext.Session.GetString("url");

            return View(Tuple.Create<List<Topic>, List<UsersTopics>>(topics, usersTopics));
        }

        public IActionResult FollowArticle(string url, int id)
        {
            int userID = _repository.Users.Where(x => x.URL == url).Select(x => x.ID).FirstOrDefault();

            _usersArticlesRepository.FollowArticle(userID, id);

            string routeUrl = HttpContext.Session.GetString("getUrl");
            return Redirect(routeUrl);
        }

        public IActionResult UnFollowArticle(string url, int id)
        {
            int userID = _repository.Users.Where(x => x.URL == url).Select(x => x.ID).FirstOrDefault();

            _usersArticlesRepository.UnFollowArticle(userID, id);

            string routeUrl = HttpContext.Session.GetString("getUrl");
            return Redirect(routeUrl);
        }


        public IActionResult SavedArticles(string url)
        {

            if (HttpContext.Session.GetString("guid") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            int getUserID = _repository.Users.Where(x => x.URL == url).Select(x => x.ID).FirstOrDefault();
            ViewBag.getUserID = getUserID;
            ViewBag.url = url;
            List<UsersArticles> usersArticles = _usersArticlesRepository.UsersArticles.Where(x => x.UserID == getUserID).ToList();
            List<Article> articles = _articleRepository.Articles.ToList();
            List<User> users = _repository.Users.ToList();
            List<ArticlesTopics> articlesTopics = _articleTopicRepository.ArticlesTopics.ToList();
            List<Topic> topics = _topicRepository.Topics.ToList();



            return View(Tuple.Create<List<UsersArticles>, List<Article>, List<User>, List<ArticlesTopics>, List<Topic>>(usersArticles, articles, users, articlesTopics, topics));
        }

    }
}
