using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IUserRepository : ISqlRepository
    {
        UserProfile Add(UserProfile userProfile);
        UserProfile GetUserProfileById(string id);
        IQueryable<UserProfile> GetUserProfiles();
        void DeleteUserProfile(UserProfile user);
        void UpdateUserProfile(UserProfile userProfile);
    }
}
