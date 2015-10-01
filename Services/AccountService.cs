using System;
using System.Linq;
using System.Threading.Tasks;
using DbBasicApp.Models;
using Microsoft.AspNet.Http;

namespace DbBasicApp.Services
{
    public class AccountService<T> where T : ISignInInfo
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
            var user = await Task.Run(() => _dbContext.LoginInfos.FirstOrDefault(l => l.UserName == userName));
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

        public async Task SignInAsync(T user, bool isPersistent = false)
        {
            await SignInAsync(user.UserName, user.Password, isPersistent);
        }
    }
}