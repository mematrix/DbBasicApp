using DbBasicApp.Models;

namespace DbBasicApp.Services
{
    /// <summary>
    /// 注册结果
    /// </summary>
    public class RegisterResult : AccountResult
    {
        /// <summary>
        /// 注册成功后，存储用户注册信息
        /// </summary>
        public LoginInfo User { get; set; }
    }
}