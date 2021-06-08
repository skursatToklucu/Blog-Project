using ProjectBlogMvc.Models.DAL.Context;
using ProjectBlogMvc.Models.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ProjectBlogMvc.Models.DATA.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ProjectContext _context;

        public UserRepository(ProjectContext context)
        {
            _context = context;
        }

        public IQueryable<User> Users => _context.Users;



        public bool Delete(int id)
        {
            _context.Users.Remove(GetById(id));
            return _context.SaveChanges() > 0;
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public bool Insert(User user)
        {
            _context.Users.Add(user);
            return _context.SaveChanges() > 0;
        }

        public bool Update(User user)
        {

            _context.Users.Update(user);
            return _context.SaveChanges() > 0;
        }

        public void SetFollower(int followerID, int followedID)
        {
            var Follower = _context.Users.Find(followerID);
            var Followed = _context.Users.Find(followedID);
        }

        public List<FollowerFollowed> FollowedList(int followerID)
        {
            throw new NotImplementedException();
        }

        public void SendSignUpMail(string email)
        {
            MailAddress mailAddress = new MailAddress(email);
            string url = mailAddress.User;
            
            User user = new User()
            {
                //accouttan çekilecek bilgiler
                UserName = "User",
                FirstName = "Ad",
                LastName = "Soyad",
                Email = email,
                Bio = "bio",
                URL = url,
                IsActive = false,
                HitRate = 0,
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            using (MailMessage mm = new MailMessage("babooblogactivation@gmail.com", user.Email))
            {
                //string url = _context.Users.Where(x => x.Email == user.Email).Select(x => x.URL).FirstOrDefault();
                //string url = string.Format("/Home/Index?email={0}", user.Email);

                mm.Subject = "Hesap Aktivasyonu";
                string body = "Merhaba" + user.FirstName + ",";
                body += "Aktivasyon için tıklayın<br /><a href = '" + string.Format("https://localhost:44364/Home/Activation/@{0}/", user.URL) + "'>Hesabınızı aktifleştirmek için tıklayınız.</a>";

                body += "<br /><br />Teşekkürler";
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                NetworkCredential NetworkCred = new NetworkCredential("babooblogactivation@gmail.com", "6strokeRoll.");
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
                mm.Dispose();
            }
        }
        public void Activate(string email)
        {
            User user = _context.Users.Where(x => x.Email == email).FirstOrDefault();

            if (user != null && user.IsActive == false)
            {
                user.IsActive = true;
                _context.Users.Update(user);
                _context.SaveChanges();

            }
        }


        public void SendSignInMail(string email)
        {
            User user = _context.Users.Where(x => x.Email == email).FirstOrDefault();
            Guid guid = new Guid();
            guid = Guid.NewGuid();


            using (MailMessage mm = new MailMessage("babooblogactivation@gmail.com", user.Email))
            {
                //string url = string.Format("/Home/Index?email={0}", user.Email);

                mm.Subject = "Giriş Linki";
                string body = "Merhaba " + user.FirstName + ",";
                body += " Şimdi Yazmaya başla :) <br /><a href = '" + string.Format("https://localhost:44364/User/Session/@{0}/{1}", user.URL, guid + "'>Giriş için tıklayınız!</a>");
                body += "<br /><br />Teşekkürler";
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                NetworkCredential NetworkCred = new NetworkCredential("babooblogactivation@gmail.com", "6strokeRoll.");
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
                mm.Dispose();
            }
        }



        public void setURL(int id, string url)
        {
            User user = _context.Users.Where(x => x.ID == id).FirstOrDefault();

            user.URL = url;

            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
