using BLL.Abstract;
using BLL.Models;
using BLL.Services.Identity;
using DAL.Abstract;
using DAL.Entities;
using DAL.Entities.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class AccountProvider : IAccountProvider
    {
        private readonly AppUserManager _userManager;
        private readonly AppSignInManager _signInManager;
        private readonly IAuthenticationManager _authManager;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AccountProvider(AppUserManager userManager,
            AppSignInManager signInManager,
            IAuthenticationManager authManager,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authManager = authManager;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public AppSignInManager SignInManager
        {
            get
            {
                return _signInManager;
            }
        }

        public AppUserManager UserManager
        {
            get
            {
                return _userManager;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return _authManager;
            }
        }

        public async Task<SignInStatus> Login(LoginViewModel model)
        {
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            return result;
        }

        public void LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        public StatusViewModel Register(RegisterViewModel model)
        {
            try
            {
                using (var uof = _unitOfWork)
                {
                    uof.StartTransaction();
                    var user = new AppUser
                    {
                        UserName = model.Email,
                        Email = model.Email
                    };

                    var result = UserManager.Create(user, model.Password);

                    if (result.Succeeded)
                    {
                        Guid guidImage = Guid.NewGuid();
                        UserProfile userProfile = new UserProfile
                        {
                            Id = user.Id,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Image = model.Image
                        };
                        _userRepository.Add(userProfile);
                        //throw new Exception();
                        uof.CommitTransaction();
                        return StatusViewModel.Success;
                    }
                }
            }
            catch
            {
            }

            return StatusViewModel.Error;
        }
        public StatusViewModel CreateLogin(string email)
        {
            var info = _authManager.GetExternalLoginInfo();
            var user = new AppUser
            {
                UserName = email,
                Email = email
            };

            var result = UserManager.Create(user);

            if (result.Succeeded)
            {
                result = UserManager.AddLogin(user.Id, info.Login);

                if (result.Succeeded)
                {
                    UserProfile userProfile = new UserProfile();
                    userProfile.Id = user.Id;
                    _userRepository.Add(userProfile);
                    _userRepository.SaveChanges();
                    //UserManager.AddToRole(user.Id, "User");
                    SignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                    return StatusViewModel.Success;
                }
            }

            return StatusViewModel.Error;
        }

        public async Task<StatusViewModel> RegisterAsync(RegisterViewModel model)
        {
            return await Task.Run(() => this.Register(model));
        }

        public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
        {
            return await _authManager.GetExternalLoginInfoAsync();
        }

        public async Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo)
        {
            return await _signInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
        }

        public UserItemViewModel GetUserById(string id)
        {
            var user = _userRepository.GetUserProfileById(id);
            return new UserItemViewModel
            {
                Id = user.Id,
                Email = user.AppUserOf.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Image = user.Image,
                Address = user.Address,
                Banned = user.Banned
            };
        }

        public StatusViewModel CreateUser(UserInterfereViewModel User)
        {
            IEnumerable<string> UserEmails = GetUsers()
                .Select(m => m.Email);

            if (UserEmails.Contains(User.Email))
                return StatusViewModel.Dublication;

            _userRepository.Insert<UserProfile>(new UserProfile
            {
                FirstName = User.FirstName,
                LastName = User.Image,
                Image = User.Image,
                Address = User.Address,
                Banned = false
            });
            _userRepository.SaveChanges();

            return StatusViewModel.Success;
        }

        public StatusViewModel DeleteUser(string id)
        {
            UserProfile User = _userRepository.GetUserProfileById(id);
            _userRepository.DeleteUserProfile(User);
            _userRepository.SaveChanges();

            return StatusViewModel.Success;
        }

        public StatusViewModel EditUser(UserInterfereViewModel User, string id)
        {
            _userRepository.UpdateUserProfile(new UserProfile
            {
                FirstName = User.FirstName,
                LastName = User.LastName,
                Image = User.Image,
                Id = id,
                Address = User.Address,
                Banned = User.Banned
            });
            _userRepository.SaveChanges();
            return StatusViewModel.Success;
        }

        public IEnumerable<UserItemViewModel> GetUsers()
        {
            return _userRepository
                .GetUserProfiles()
                .Select(m => new UserItemViewModel
                {
                    Id = m.Id,
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                    Email = m.AppUserOf.Email,
                    Image = m.Image,
                    Address = m.Address,
                    Banned = m.Banned
                });
        }

        public UserViewModel GetUsersByPage(int page, int items, string searchName)
        {
            IEnumerable<UserProfile> query = _userRepository.GetUserProfiles();
            UserViewModel model = new UserViewModel();

            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(m => m.AppUserOf.Email.ToLower().Contains(searchName.ToLower()));
            }

            model.Users = query
                .Skip((page - 1) * items)
                .Take(items)
                .Select(m => new UserItemViewModel
                {
                    Id = m.Id,
                    Email = m.AppUserOf.Email,
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                    Image = m.Image,
                    Address = m.Address,
                    Banned = m.Banned
                }).ToList();
            model.TotalPages = (int)Math.Ceiling((double)query.Count() / items);
            model.CurrentPage = page;
            model.SearchName = searchName;
            model.Items = items;

            return model;
        }
    }
}
