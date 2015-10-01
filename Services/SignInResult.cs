namespace DbBasicApp.Services
{
    /// <summary>
    /// 登录结果
    /// </summary>
    public class SignInResult : AccountResult
    {
        /// <summary>
        /// 指示是否需要验证信息
        /// </summary>
        public bool RequiresVerify { get; set; }

        /// <summary>
        /// 指示账号是否被锁定
        /// </summary>
        public bool IsLockedOut { get; set; }
    }
}