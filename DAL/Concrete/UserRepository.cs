using DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Concrete
{
    public class UserRepository : SqlRepository, IUserRepository 
    {
        public UserRepository(IEFContext context)
            : base(context)
        {

        }

        public UserProfile Add(UserProfile userProfile)
        {
            Insert(userProfile);
            SaveChanges();
            return userProfile;
        }

        public void DeleteUserProfile(UserProfile user)
        {
            _context.Set<UserProfile>().Remove(user);
            SaveChanges();
        }

        public UserProfile GetUserProfileById(string id)
        {
            return _context.Set<UserProfile>().SingleOrDefault(u => u.Id == id);
        }

        public IQueryable<UserProfile> GetUserProfiles()
        {
            return _context.Set<UserProfile>();
        }

        public void UpdateUserProfile(UserProfile userProfile)
        {
            _context.Entry(_context.Set<UserProfile>().SingleOrDefault(t => t.Id == userProfile.Id)).CurrentValues.SetValues(userProfile);
        }
    }
}
