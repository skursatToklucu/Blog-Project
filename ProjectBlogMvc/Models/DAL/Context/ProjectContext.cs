using Microsoft.EntityFrameworkCore;
using ProjectBlogMvc.Models.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectBlogMvc.Models.ViewModels;

namespace ProjectBlogMvc.Models.DAL.Context
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Topic> Topics { get; set; }

        public DbSet<UsersTopics> UsersTopics { get; set; }

        public DbSet<ArticlesTopics> ArticlesTopics { get; set; }

        public DbSet<UsersArticles> UsersArticles { get; set; }

        public DbSet<FollowerFollowed> FollowerFolloweds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.ID);
            modelBuilder.Entity<Article>().HasKey(a => a.ID);
            modelBuilder.Entity<Topic>().HasKey(t => t.ID);


            modelBuilder.Entity<FollowerFollowed>()
           .HasKey(ff => new { ff.FollowedID, ff.FollowerID });

            modelBuilder.Entity<UsersArticles>().HasKey(ua => new { ua.UserID, ua.ArticleID });


            modelBuilder.Entity<UsersTopics>().HasKey(ut => new { ut.UserID, ut.TopicID });
            modelBuilder.Entity<UsersTopics>()
                .HasOne(ut => ut.User)
                .WithMany(u => u.UsersTopics)
                .HasForeignKey(ut => ut.UserID);
            modelBuilder.Entity<UsersTopics>()
                .HasOne(ut => ut.Topic)
                .WithMany(t => t.UsersTopics)
                .HasForeignKey(ut => ut.TopicID);


            modelBuilder.Entity<ArticlesTopics>().HasKey(at => new { at.ArticleID, at.TopicID });
            modelBuilder.Entity<ArticlesTopics>()
                .HasOne(at => at.Article)
                .WithMany(a => a.ArticlesTopics)
                .HasForeignKey(at => at.ArticleID);
            modelBuilder.Entity<ArticlesTopics>()
                .HasOne(at => at.Topic)
                .WithMany(t => t.ArticlesTopics)
                .HasForeignKey(at => at.TopicID);

            base.OnModelCreating(modelBuilder);
        }


    }
}
