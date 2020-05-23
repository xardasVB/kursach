using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IAccountProvider
    {
        UserViewModel GetUsersByPage(int page, int items, string searchName);
        IEnumerable<UserItemViewModel> GetUsers();
        UserItemViewModel GetUserById(string id);
        StatusViewModel DeleteUser(string id);
        StatusViewModel CreateUser(UserInterfereViewModel User);
        StatusViewModel EditUser(UserInterfereViewModel User, string id);
    }
}
