namespace DbBasicApp.Services
{
    /// <summary>
    /// 登录信息接口定义
    /// </summary>
    public interface ISignInInfo
    {
        /// <summary>
        /// 用户名
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        string Password { get; set; }
    }
}