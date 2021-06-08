using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectBlogMvc.Models.DAL.Context;
using ProjectBlogMvc.Models.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBlogMvc.Models
{
    public enum TopicType { Programlama, Müzik, Kitap, Bilim, Oyun, Sanat , Tasarım, Yemek, Spor, Ekonomi, Medya, Tarih, Teknoloji}
    public class SeedData
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                ProjectContext context = serviceScope.ServiceProvider.GetService<ProjectContext>();

                context.Database.Migrate();

                if (!context.Topics.Any())
                {
                    foreach (var item in Enum.GetValues(typeof(TopicType)))
                    {
                        context.Topics.AddRange(new Topic() { Name = item.ToString() });
                    }
                }
                context.SaveChanges();
            }
        }

    }
}
