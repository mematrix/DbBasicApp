using System;
using System.Linq;
using System.Threading.Tasks;
using DbBasicApp.Models;
using Microsoft.AspNet.Http;

namespace DbBasicApp.Services
{
    public class AccountService<T> where T : class, ISignInInfo
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

        public async Task<SignInResult> SignInAsync(T user, bool isPersistent = false)
        {
            return await SignInAsync(user.UserName, user.Password, isPersistent);
        }

        public async Task<AccountResult> RegisterAsync(T user)
        {
            try
            {
                _dbContext.Add(user);
                await _dbContext.SaveChangesAsync();
                return new AccountResult { IsSucceeded = true };
            }
            catch (System.Exception e)
            {
                return new AccountResult { IsSucceeded = false, ErrorMsg = e.Message };
            }
        }
        
        /// <summary>
        /// 退出登录
        /// </summary>
        public async Task SignOutAsync()
        {
            await Task.Run(()=>_httpContext.Session.Remove("signin-user"));
        }

        public async Task<ISignInInfo> FindUserByNameAsync(string userName)
        {
            return await Task.Run(() => _dbContext.LoginInfos.FirstOrDefault(l => l.UserName == userName));
        }
        
        /// <summary>
        /// 获取当前登录用户
        /// </summary>
        public async Task<T> GetCurrentUserAsync()
        {
            var userName=_httpContext.Session.GetString("signin-user");
            var user = await FindUserByNameAsync(userName);
            return user as T;
        }
    }
}