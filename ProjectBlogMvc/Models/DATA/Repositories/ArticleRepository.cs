using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ProjectBlogMvc.Models.DAL.Context;
using ProjectBlogMvc.Models.DATA.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectBlogMvc.Models.DATA.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private ProjectContext _context;

        public ArticleRepository(ProjectContext context)
        {
            _context = context;
        }


        public IQueryable<Article> Articles => _context.Articles;

        public bool Delete(int id)
        {
            _context.Articles.Remove(GetById(id));
            return _context.SaveChanges() > 0;
        }

        public Article GetById(int id)
        {
            return _context.Articles.Find(id);
        }

        public bool Insert(Article article)
        {
           
            article.ReleaseDate = DateTime.Now;
            _context.Articles.Add(article);
            return _context.SaveChanges() > 0;
        }

        public bool Update(Article article)
        {

            _context.Articles.Update(article);
            return _context.SaveChanges() > 0;
        }

        [Obsolete]
        public string UploadImage(IFormFile file, IHostingEnvironment _env)
        {
            var uploads = Path.Combine(_env.WebRootPath, "uploads");

            if (file.ContentType.Contains("image"))
            {
                if (file.Length <= 2097152)
                {
                    string uniqueName = $"{Guid.NewGuid().ToString().Replace("-", "_").ToLower()}.{file.ContentType.Split('/')[1]}";

                    var filePath = Path.Combine(uploads, uniqueName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                        
                        return filePath.Substring(filePath.IndexOf("\\uploads\\"));
                        //\uploads\fileName.jpg
                    }
                }

                else
                {
                    return $"2 MB'tan büyük boyutta resim yükleyemezsiniz";
                }
            }
            else
            {
                return $"Lütfen sadece resim dosyası yükleyiniz!";
            }
        }
        public void DeleteImage(Article article) 
        {
            string picturePath = article.Picture;
            article.Picture.Replace($"{picturePath}", "");
            _context.Articles.Update(article);
            _context.SaveChanges();
        }
    }
}
