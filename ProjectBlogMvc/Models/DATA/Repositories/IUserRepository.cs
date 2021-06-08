using ProjectBlogMvc.Models.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBlogMvc.Models.DATA.Repositories
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }

        bool Insert(User user);

        bool Update(User user);

        bool Delete(int id);

        User GetById(int id);

        void SetFollower(int followerID, int followedID);

        List<FollowerFollowed> FollowedList(int followerID);

        void SendSignUpMail(string email);

        void SendSignInMail(string email);

        void Activate(string email);

       

        
    }
}
