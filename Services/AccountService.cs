using System;
using System.Threading.Tasks;
using DbBasicApp.Models;
using DbBasicApp.ViewModels;
using Microsoft.AspNet.Http;
using Microsoft.Data.Entity;

namespace DbBasicApp.Services
{
    public class AccountService
    {
        private AppDbContext _dbContext;
        private HttpContext _httpContext;

        public AccountService(AppDbContext context, IHttpContextAccessor accessor)
        {
            _dbContext = context;
            _httpContext = accessor.HttpContext;
        }

        /// <summary>
        /// 使用账号和密码登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">登录密码</param>
        /// <param name="isPersistent">当浏览器退出时登录cookie是否应该保存</param>
        /// <returns>登录结果</returns>
        public async Task<SignInResult> SignInAsync(string userName, string password, bool isPersistent)
        {
            var user = await FindUserByNameAsync(userName);
            if (user == null)
            {
                return new SignInResult { IsSucceeded = false, ErrorMsg = "用户不存在" };
            }
            if (user.Password != password)
            {
                return new SignInResult { IsSucceeded = false, ErrorMsg = "登录密码不正确" };
            }
            if (isPersistent)
            {
                _httpContext.Response.Cookies.Append("user", userName,
                    new CookieOptions { Expires = DateTime.Now.AddMonths(1), HttpOnly = true });
            }
            else
            {
                _httpContext.Response.Cookies.Delete("user");
            }
            _httpContext.Session.SetString("signin-user", userName);
            return new SignInResult { IsSucceeded = true };
        }

        public async Task<SignInResult> SignInAsync(LoginInfo user, bool isPersistent = false)
        {
            return await SignInAsync(user.UserName, user.Password, isPersistent);
        }

        public async Task<RegisterResult> RegisterAsync(LoginInfo user)
        {
            try
            {
                var entity = _dbContext.UserInfos.Add(user.UserInfo);
                user.UserId = entity.Entity.ID;
                // user.UserInfo = null;
                _dbContext.LoginInfos.Add(user);
                await _dbContext.SaveChangesAsync();
                return new RegisterResult { IsSucceeded = true, User = user };
            }
            catch (System.Exception e)
            {
                return new RegisterResult { IsSucceeded = false, ErrorMsg = e.Message };
            }
        }

        public async Task<RegisterResult> RegisterAsync(RegisterViewModel model)
        {
            bool? sex = null;
            if (model.Sex == 1)
            {
                sex = true;
            }
            else if (model.Sex == 2)
            {
                sex = false;
            }

            var userInfo = new UserInfo
            {
                Name = model.Name,
                Sex = sex,
                Birthday = model.Birthday,
                CardID = model.CardID,
                LastUsage = 0,
                CurrentUsage = 0,
                Balance = 0,
                RegisterTime = DateTime.Now,
                TelPackage = null
            };
            var user = new LoginInfo
            {
                UserName = model.UserName,
                Password = model.Password,
                Level = 0,
                UserInfo = userInfo
            };

            return await RegisterAsync(user);
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        public async Task SignOutAsync()
        {
            await Task.Run(() => _httpContext.Session.Remove("signin-user"));
        }

        public async Task<AccountResult> UpdateInfoAsync(UserInfo userInfo)
        {
            try
            {
                _dbContext.UserInfos.Update(userInfo);
                await _dbContext.SaveChangesAsync();
                return new AccountResult { IsSucceeded = true };
            }
            catch (Exception e)
            {
                return new AccountResult { IsSucceeded = false, ErrorMsg = e.Message };
            }
        }

        public async Task<LoginInfo> FindUserByNameAsync(string userName)
        {
            return await _dbContext.LoginInfos.Include(l => l.UserInfo).FirstOrDefaultAsync(l => l.UserName == userName);
        }

        /// <summary>
        /// 获取当前登录用户
        /// </summary>
        public async Task<LoginInfo> GetCurrentUserAsync()
        {
            var userName = _httpContext.Session.GetString("signin-user");
            return await FindUserByNameAsync(userName);
        }
    }
}