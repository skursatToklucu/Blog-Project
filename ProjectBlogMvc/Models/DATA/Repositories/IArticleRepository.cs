using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ProjectBlogMvc.Models.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBlogMvc.Models.DATA.Repositories
{
    public interface IArticleRepository
    {

        IQueryable<Article> Articles { get; }

        bool Insert(Article article);

        bool Update(Article article);

        bool Delete(int id);

        [Obsolete]
        string UploadImage(IFormFile file, IHostingEnvironment _env);

        void DeleteImage(Article article);

        Article GetById(int id);
    }
}
