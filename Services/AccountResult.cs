namespace DbBasicApp.Services
{
    /// <summary>
    /// 账户操作结果信息
    /// </summary>
    public class AccountResult
    {
        /// <summary>
        /// 指示操作是否成功
        /// </summary>
        public bool IsSucceeded { get; set; }

        /// <summary>
        /// 操作失败时，错误信息
        /// </summary>
        public string ErrorMsg { get; set; }
    }
}