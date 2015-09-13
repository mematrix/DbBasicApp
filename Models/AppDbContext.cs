using Microsoft.Data.Entity;

namespace DbBasicApp.Models
{
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// 套餐表
        /// </summary>
        public DbSet<TelecomPackage> TelePackages { get; set; }

        /// <summary>
        /// 用户详细信息
        /// </summary>
        public DbSet<UserInfo> UserInfos { get; set; }

        /// <summary>
        /// 普通用户登录信息
        /// </summary>
        public DbSet<UserLoginInfo> UserLoginInfos { get; set; }

        /// <summary>
        /// 收款员登录信息
        /// </summary>
        public DbSet<CashierLoginInfo> CashierLoginInfos { get; set; }

        /// <summary>
        /// 客服人员登录信息
        /// </summary>
        public DbSet<SupporterLoginInfo> SupporterLoginInfo { get; set; }

        /// <summary>
        /// 缴费收费纪录表
        /// </summary>
        public DbSet<PaymentRecord> PaymentRecords { get; set; }

        /// <summary>
        /// 评价信息表
        /// </summary>
        public DbSet<RatingRecord> RatingRecords { get; set; }
    }
}