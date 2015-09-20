using Microsoft.Data.Entity;
using Microsoft.Dnx.Runtime;
using Microsoft.Dnx.Runtime.Infrastructure;
using Microsoft.Framework.DependencyInjection;

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var appEvn = CallContextServiceLocator.Locator.ServiceProvider.GetRequiredService<IApplicationEnvironment>();
            optionsBuilder.UseSqlite($"Data Source = { appEvn.ApplicationBasePath }/app.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserInfo>().Reference(u => u.TelPackage)
                .InverseCollection(t => t.UserInfos)
                .ForeignKey(u => u.PackageID)
                .Required(false);
            modelBuilder.Entity<UserLoginInfo>().Reference(u => u.UserInfo)
                .InverseReference()
                .ForeignKey((UserLoginInfo u) => u.UserId)
                .Required(true);
            modelBuilder.Entity<CashierLoginInfo>().Reference(u => u.UserInfo)
                .InverseReference()
                .ForeignKey((CashierLoginInfo u) => u.UserId)
                .Required(true);
            modelBuilder.Entity<SupporterLoginInfo>().Reference(u => u.UserInfo)
                .InverseReference()
                .ForeignKey((SupporterLoginInfo u) => u.UserId)
                .Required(true);
            modelBuilder.Entity<PaymentRecord>().Reference(p => p.UserLoginInfo)
                .InverseReference()
                .ForeignKey((PaymentRecord p) => p.UserName)
                .Required(true);
            modelBuilder.Entity<PaymentRecord>().Reference(p => p.CashierInfo)
                .InverseCollection(c => c.PaymentRecords)
                .ForeignKey(p => p.CashierName)
                .Required(false);
            modelBuilder.Entity<RatingRecord>().Reference(r => r.UserLoginInfo)
                .InverseCollection(u => u.RatingRecords)
                .ForeignKey(r => r.UserName)
                .Required(true);
            modelBuilder.Entity<RatingRecord>().Reference(r => r.SupporterInfo)
                .InverseCollection(s => s.RatingRecords)
                .ForeignKey(r => r.SupporterName)
                .Required(true);

            modelBuilder.Entity<UserInfo>().AlternateKey(u => u.CardID);
        }
    }
}