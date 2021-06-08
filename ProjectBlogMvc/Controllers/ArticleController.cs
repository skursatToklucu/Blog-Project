using Grpc.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList;
using ProjectBlogMvc.Models;
using ProjectBlogMvc.Models.DAL.Context;
using ProjectBlogMvc.Models.DATA.Entities;
using ProjectBlogMvc.Models.DATA.Repositories;
using ProjectBlogMvc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectBlogMvc.Controllers
{
    public class ArticleController : Controller
    {

        private IArticleRepository _repository;
        private IUserRepository _userRepository;
        private ITopicRepository _topicRepository;
        private IArticleTopicRepository _articleTopicRepository;
        private IUsersArticlesRepository _usersArticlesRepository;


        [Obsolete]
        private readonly IHostingEnvironment _environment;


        [Obsolete]
        public ArticleController(IArticleRepository repository, IUserRepository userRepository, ITopicRepository topicRepository, IArticleTopicRepository articleTopicRepository, IHostingEnvironment environment, IUsersArticlesRepository usersArticlesRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _topicRepository = topicRepository;
            _articleTopicRepository = articleTopicRepository;
            _environment = environment;
            _usersArticlesRepository = usersArticlesRepository;
        }


        // GET: ArticleController
        public IActionResult Index()
        {
            return View();
        }

        // GET: ArticleController/Details/5
        public IActionResult Details(int id)
        {

            if (HttpContext.Session.GetString("guid") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.url = HttpContext.Session.GetString("url");
            Article article = _repository.GetById(id);
            return View(article);
        }

        // GET: ArticleController/Create
        public IActionResult Create()
        {

            if (HttpContext.Session.GetString("guid") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            string email = HttpContext.Session.GetString("getEmail");

            ViewBag.GetUserID = _userRepository.Users.Where(x => x.Email == email).Select(x => x.ID).FirstOrDefault();
            int getUserID = _userRepository.Users.Where(x => x.Email == email).Select(x => x.ID).FirstOrDefault();

            List<Topic> topiclist = _topicRepository.Topics.ToList();
            CheckBoxTopicList checkboxitems = new CheckBoxTopicList();
            List<CheckBoxTopicModel> checkboxitem = new List<CheckBoxTopicModel>();

            foreach (var item in topiclist)
            {
                CheckBoxTopicModel cboxmodel = new CheckBoxTopicModel { ID = item.ID, Name = item.Name, IsChecked = false };
                checkboxitem.Add(cboxmodel);
            };

            checkboxitems.TopicItems = checkboxitem;
            ViewBag.Checkboxitems = checkboxitems.TopicItems;

            ViewBag.url = HttpContext.Session.GetString("url");
            ViewBag.Topics = topiclist;
            ViewBag.ArticlesTopics = _articleTopicRepository.ArticlesTopics.ToList();

            return View();
        }

        // POST: ArticleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public IActionResult Create(Article article, IFormFile img, List<int> checboxItems)
        {

            if (HttpContext.Session.GetString("guid") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            MatchCollection collection = Regex.Matches(article.Content, @"[\S]+");
            if (Convert.ToInt16(Math.Round(Convert.ToDecimal(collection.Count / 250))) <= 1)
            {
                article.ReadingTime = 1;
            }
            else
            {
                article.ReadingTime = Convert.ToInt16(Math.Round(Convert.ToDecimal(collection.Count / 250)));
            }


            article.Picture = _repository.UploadImage(img, _environment);
            string email = HttpContext.Session.GetString("getEmail");
            _repository.Insert(article);

            ViewBag.url = HttpContext.Session.GetString("url");

            _articleTopicRepository.AddTopicsToArticle(article.ID, checboxItems);

            string url = HttpContext.Session.GetString("getUrl");
            return Redirect(url);
        }



        #region UploadImage

        //[HttpPost]
        //public IActionResult UploadImage()
        //{
        //    foreach (var fileKey in Request.Files.AllKeys)
        //    {
        //        var file = Request.Files[fileKey];
        //        try
        //        {
        //            var fileName = Path.GetFileName(file?.FileName);
        //            if (fileName != null)
        //            {
        //                var path = Server.MapPath("~/Uploads/SummernoteImages");
        //                if (System.IO.File.Exists(path) == false)
        //                {
        //                    Directory.CreateDirectory(path);
        //                }

        //                path = Path.Combine(Server.MapPath("~/Uploads/SummernoteImages"), fileName);
        //                file.SaveAs(path);
        //                return Json(new { Url = Url.Content("~/Uploads/SummernoteImages/" + fileName) });
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return Json(new { Message = $"Error in saving file ({ex.Message})" });
        //        }
        //    }
        //    return Json(new { Message = "File saved" });
        //}

        #endregion

        // GET: ArticleController/Edit/5
        public IActionResult Edit(int id)
        {

            if (HttpContext.Session.GetString("guid") == null)
            {
                return RedirectToAction("Index", "Home");
            }


            _articleTopicRepository.RemoveTopicsFromArticle(id);

            List<Topic> topiclist = _topicRepository.Topics.ToList();


            CheckBoxTopicList checkboxitems = new CheckBoxTopicList();

            List<CheckBoxTopicModel> checkboxitem = new List<CheckBoxTopicModel>();

            foreach (var item in topiclist)
            {
                CheckBoxTopicModel cboxmodel = new CheckBoxTopicModel { ID = item.ID, Name = item.Name, IsChecked = false };
                checkboxitem.Add(cboxmodel);
            };

            checkboxitems.TopicItems = checkboxitem;

            ViewBag.Checkboxitems = checkboxitems.TopicItems;

            ViewBag.Topics = topiclist;


            string getUrl = HttpContext.Session.GetString("url");
            ViewBag.url = getUrl;
            ViewBag.UserID = _userRepository.Users.Where(x => x.URL == getUrl).Select(x => x.ID).FirstOrDefault();

            return View(_repository.GetById(id));
        }

        // POST: ArticleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public IActionResult Edit(Article article, IFormFile img, List<int> checboxItems)
        {


            _articleTopicRepository.AddTopicsToArticle(article.ID, checboxItems);

            if (HttpContext.Session.GetString("guid") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            MatchCollection collection = Regex.Matches(article.Content, @"[\S]+");
            if (Convert.ToInt16(Math.Round(Convert.ToDecimal(collection.Count / 250))) <= 1)
            {
                article.ReadingTime = 1;
            }
            else
            {
                article.ReadingTime = Convert.ToInt16(Math.Round(Convert.ToDecimal(collection.Count / 250)));
            }

            int userID = _userRepository.Users.Where(x => x.URL == HttpContext.Session.GetString("url")).Select(x => x.ID).FirstOrDefault();
            article.UserID = userID;

            if (img == null)
            {

                _repository.Update(article);
                return RedirectToAction("List", "Article");

            }
            _repository.DeleteImage(article);
            article.Picture = _repository.UploadImage(img, _environment);
            try
            {
                _repository.Update(article);
                return RedirectToAction("List", "Article");
            }


            catch
            {
                return View();
            }
        }

        // GET: ArticleController/Delete/5
        public IActionResult Delete(int id)
        {

            if (HttpContext.Session.GetString("guid") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            _repository.Delete(id);

            string url = string.Format("~/Article/List/@{0}", HttpContext.Session.GetString("url"));
            return Redirect(url);
        }

        // POST: ArticleController/Delete/5
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


        public IActionResult List(string sortOrder, string currentFilter, string searchString, int? page)
        {

            if (HttpContext.Session.GetString("guid") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            string email = HttpContext.Session.GetString("getEmail");
            ViewBag.Email = email;
            ViewBag.url = HttpContext.Session.GetString("url");
            int userID = _userRepository.Users.Where(x => x.Email == email).Select(x => x.ID).FirstOrDefault();

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            List<Article> articles = _repository.Articles.Where(x => x.UserID == userID).ToList();
            List<User> users = _userRepository.Users.ToList();
            List<Topic> topics = _topicRepository.Topics.ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                articles = articles.AsQueryable().Where(a => a.Title.Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    articles = articles.AsQueryable().OrderByDescending(a => a.Title).ToList();
                    //users = users.AsQueryable().OrderByDescending(u => u.FirstName).ToList();
                    //users = users.AsQueryable().OrderByDescending(u => u.LastName).ToList();
                    //users = users.AsQueryable().OrderByDescending(u => u.UserName).ToList();
                    //topics = topics.AsQueryable().OrderByDescending(t => t.Name).ToList();
                    break;
                case "Date":
                    articles = articles.AsQueryable().OrderBy(a => a.ReleaseDate).ToList();
                    break;
                case "date_desc":
                    articles = articles.AsQueryable().OrderByDescending(a => a.ReleaseDate).ToList();
                    break;
                default:
                    articles = articles.AsQueryable().OrderBy(a => a.Title).ToList();
                    //users = users.AsQueryable().OrderBy(u => u.FirstName).ToList();
                    //users = users.AsQueryable().OrderBy(u => u.LastName).ToList();
                    //users = users.AsQueryable().OrderBy(u => u.UserName).ToList();
                    //topics = topics.AsQueryable().OrderBy(t => t.Name).ToList()
                    ;
                    break;
            }
            int pageSize = articles.Count();
            int pageNumber = (page ?? 1);

            return View(articles.ToPagedList(pageNumber, pageSize));
        }


        public IActionResult ArticlesRelatedTopic(int id)
        {
            Topic topic = _topicRepository.Topics.Where(x => x.ID == id).FirstOrDefault();

            List<Article> articles = new List<Article>();
            List<User> users = _userRepository.Users.ToList();
            List<UsersArticles> usersArticles = _usersArticlesRepository.UsersArticles.ToList();

            int getUserID = _userRepository.Users.Where(x => x.URL == HttpContext.Session.GetString("url")).Select(x => x.ID).FirstOrDefault();

            ViewBag.getUserID = getUserID;
            ViewBag.url = HttpContext.Session.GetString("url");

            List<ArticlesTopics> articlesTopics = _articleTopicRepository.ArticlesTopics.Where(x => x.TopicID == id).ToList();


            foreach (var item in articlesTopics)
            {
                Article article = _repository.Articles.Where(x => x.ID == item.ArticleID).FirstOrDefault();
                articles.Add(article);
            }

            return View(Tuple.Create<List<Article>, List<User>, List<ArticlesTopics>, Topic, List<UsersArticles>>(articles, users, articlesTopics, topic, usersArticles));
        }
    }
}
