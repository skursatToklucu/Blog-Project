using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectBlogMvc.Models;
using ProjectBlogMvc.Models.DATA.Entities;
using ProjectBlogMvc.Models.DATA.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace ProjectBlogMvc.Controllers
{
    public class HomeController : Controller
    {
        private IUserRepository _repository;
        private IArticleRepository _articleRepository;
        private ITopicRepository _topicRepository;
        private IArticleTopicRepository _articleTopicRepository;

        public HomeController(IUserRepository repository, IArticleRepository articleRepository, ITopicRepository topicRepository, IArticleTopicRepository articleTopicRepository)
        {
            _topicRepository = topicRepository;
            _articleRepository = articleRepository;
            _repository = repository;
            _articleTopicRepository = articleTopicRepository;
        }
        public IActionResult Index()
        {
            List<User> users = _repository.Users.ToList();
            List<Article> articles = _articleRepository.Articles.ToList();
            List<Topic> topics = _topicRepository.Topics.ToList();


            return View(Tuple.Create<List<User>, List<Article>, List<Topic>>(users, articles, topics));
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult Sign() => View();

        [HttpPost]
        public IActionResult Sign(string email)
        {

            User user = _repository.Users.Where(x => x.Email == email).FirstOrDefault();

            if (user != null)
            {
                TempData["Msg"] = "Bu mail adresi zaten kayıtlıdır";
                return View();
            }

            _repository.SendSignUpMail(email);



            return RedirectToAction("Index", "Home");
        }



        public IActionResult SignIn()
        {

            return View();
        }



        [HttpPost]
        public IActionResult SignIn(string email)
        {

            _repository.SendSignInMail(email);

            User user = _repository.Users.Where(x => x.Email == email).FirstOrDefault();
            //var login = _repository.Users.Where(x => x.Email == email).FirstOrDefault();

            return RedirectToAction("Index", "Home");
        }

        [Route("~/Home/Activation/@{url}")]
        public IActionResult Activation(string url)
        {
            ViewBag.url = url;

            string email = _repository.Users.Where(x => x.URL == url).Select(x => x.Email).FirstOrDefault();

            _repository.Activate(email);

            return View();
        }

        #region GetProfile
        //[AllowAnonymous]
        //public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        //{
        //    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
        //    if (loginInfo == null)
        //    {
        //        return RedirectToAction("Login");
        //    }

        //    if (loginInfo.Login.LoginProvider == "Google")
        //    {
        //        var externalIdentity = AuthenticationManager.GetExternalIdentityAsync(DefaultAuthenticationTypes.ExternalCookie);
        //        var emailClaim = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
        //        var lastNameClaim = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname);
        //        var givenNameClaim = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName);

        //        var email = emailClaim.Value;
        //        var firstName = givenNameClaim.Value;
        //        var lastname = lastNameClaim.Value;
        //    }

        //    return View();
        //} 
        #endregion

        public IActionResult HomePost(int id)
        {
            Article article = _articleRepository.Articles.Where(a => a.ID == id).FirstOrDefault();
            int getUserID = _articleRepository.Articles.Where(a => a.ID == id).Select(u => u.UserID).FirstOrDefault();
            User user = _repository.Users.Where(u => u.ID == getUserID).FirstOrDefault();
            int getTopicID = _articleTopicRepository.ArticlesTopics.Where(a => a.ArticleID == id).Select(t => t.TopicID).FirstOrDefault();
            List<Topic> topics = _topicRepository.Topics.Where(t => t.ID == getTopicID).ToList();

            List<Article> articleAll = _articleRepository.Articles.ToList();
            List<Topic> topicAll = _topicRepository.Topics.ToList();

            return View(Tuple.Create<Article, User, List<Topic>, List<Article>, List<Topic>>(article, user, topics, articleAll, topicAll));
        }



    }
}
