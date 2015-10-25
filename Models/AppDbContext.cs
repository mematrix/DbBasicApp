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
        /// 用户登录信息表
        /// </summary>
        public DbSet<LoginInfo> LoginInfos { get; set; }

        /// <summary>
        /// 缴费收费纪录表
        /// </summary>
        public DbSet<PaymentRecord> PaymentRecords { get; set; }

        /// <summary>
        /// 评价信息表
        /// </summary>
        public DbSet<RatingRecord> RatingRecords { get; set; }

        /// <summary>
        /// 服务消息表
        /// </summary>
        public DbSet<MsgRecord> MsgRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var appEvn = CallContextServiceLocator.Locator.ServiceProvider.GetRequiredService<IApplicationEnvironment>();
            optionsBuilder.UseSqlite($"Data Source = { appEvn.ApplicationBasePath }/app.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserInfo>().HasOne(u => u.TelPackage)
                .WithMany()
                .ForeignKey(u => u.PackageID)
                .Required(false);

            modelBuilder.Entity<LoginInfo>().HasOne(l => l.UserInfo)
                .WithMany()
                .ForeignKey(l => l.UserId)
                .Required(true);

            modelBuilder.Entity<PaymentRecord>().HasOne(p => p.UserLoginInfo)
                .WithMany()
                .ForeignKey(p => p.UserName)
                .Required(true);
            modelBuilder.Entity<PaymentRecord>().HasOne(p => p.CashierInfo)
                .WithMany()
                .ForeignKey(p => p.CashierName)
                .Required(false);

            modelBuilder.Entity<RatingRecord>().HasOne(r => r.UserLoginInfo)
                .WithMany()
                .ForeignKey(r => r.UserName)
                .Required(true);
            modelBuilder.Entity<RatingRecord>().HasOne(r => r.SupporterInfo)
                .WithMany()
                .ForeignKey(r => r.SupporterName)
                .Required(true);

            modelBuilder.Entity<MsgRecord>().HasOne(m => m.SenderLoginInfo)
                .WithMany()
                .ForeignKey(m => m.SenderName)
                .Required(true);
            modelBuilder.Entity<MsgRecord>().HasOne(m => m.RecvLoginInfo)
                .WithMany()
                .ForeignKey(m => m.ReceiverName)
                .Required(true);

            modelBuilder.Entity<UserInfo>().Index(u => u.CardID).Unique();
        }
    }
}