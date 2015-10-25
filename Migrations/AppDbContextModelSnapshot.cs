using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using DbBasicApp.Models;

namespace DbBasicApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta8-15964");

            modelBuilder.Entity("DbBasicApp.Models.LoginInfo", b =>
                {
                    b.Property<string>("UserName")
                        .Annotation("Relational:ColumnName", "user_name");

                    b.Property<int>("Level")
                        .Annotation("Relational:ColumnName", "level");

                    b.Property<string>("Password")
                        .IsRequired()
                        .Annotation("Relational:ColumnName", "password");

                    b.Property<int>("UserId")
                        .Annotation("Relational:ColumnName", "user_id");

                    b.HasKey("UserName");

                    b.Annotation("Relational:TableName", "login_info");
                });

            modelBuilder.Entity("DbBasicApp.Models.MsgRecord", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "id");

                    b.Property<string>("Msg")
                        .IsRequired()
                        .Annotation("Relational:ColumnName", "msg");

                    b.Property<string>("ReceiverName")
                        .IsRequired()
                        .Annotation("Relational:ColumnName", "recv_name");

                    b.Property<string>("SenderName")
                        .IsRequired()
                        .Annotation("Relational:ColumnName", "sender_name");

                    b.Property<DateTime>("Time")
                        .Annotation("Relational:ColumnName", "time");

                    b.HasKey("ID");

                    b.Annotation("Relational:TableName", "msg_record");
                });

            modelBuilder.Entity("DbBasicApp.Models.PaymentRecord", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "id");

                    b.Property<string>("CashierName")
                        .Annotation("Relational:ColumnName", "cashier_name");

                    b.Property<string>("Msg")
                        .Annotation("Relational:ColumnName", "msg");

                    b.Property<double>("PayOut")
                        .Annotation("Relational:ColumnName", "pay_out");

                    b.Property<DateTime>("Time")
                        .Annotation("Relational:ColumnName", "time");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .Annotation("Relational:ColumnName", "user_name");

                    b.HasKey("ID");

                    b.Annotation("Relational:TableName", "payment_record");
                });

            modelBuilder.Entity("DbBasicApp.Models.RatingRecord", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "id");

                    b.Property<int>("Rating")
                        .Annotation("Relational:ColumnName", "rating");

                    b.Property<string>("RatingMsg")
                        .Annotation("Relational:ColumnName", "rating_msg");

                    b.Property<string>("SupporterName")
                        .IsRequired()
                        .Annotation("Relational:ColumnName", "supporter_name");

                    b.Property<DateTime>("Time")
                        .Annotation("Relational:ColumnName", "time");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .Annotation("Relational:ColumnName", "user_name");

                    b.HasKey("ID");

                    b.Annotation("Relational:TableName", "rating_record");
                });

            modelBuilder.Entity("DbBasicApp.Models.TelecomPackage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "id");

                    b.Property<double>("BaseUsage")
                        .Annotation("Relational:ColumnName", "base_usage");

                    b.Property<string>("Name")
                        .IsRequired()
                        .Annotation("MaxLength", 50)
                        .Annotation("Relational:ColumnName", "name");

                    b.Property<double>("OutPrice")
                        .Annotation("Relational:ColumnName", "out_price");

                    b.Property<double>("Price")
                        .Annotation("Relational:ColumnName", "price");

                    b.HasKey("ID");

                    b.Annotation("Relational:TableName", "telecom_pkg");
                });

            modelBuilder.Entity("DbBasicApp.Models.UserInfo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "id");

                    b.Property<double>("Balance")
                        .Annotation("Relational:ColumnName", "balance");

                    b.Property<DateTime?>("Birthday")
                        .Annotation("Relational:ColumnName", "birthday");

                    b.Property<string>("CardID")
                        .IsRequired()
                        .Annotation("Relational:ColumnName", "card_id");

                    b.Property<double>("CurrentUsage")
                        .Annotation("Relational:ColumnName", "current_usage");

                    b.Property<double>("LastUsage")
                        .Annotation("Relational:ColumnName", "last_usage");

                    b.Property<string>("Name")
                        .IsRequired()
                        .Annotation("Relational:ColumnName", "name");

                    b.Property<int?>("PackageID")
                        .Annotation("Relational:ColumnName", "pkg_id");

                    b.Property<DateTime>("RegisterTime")
                        .Annotation("Relational:ColumnName", "reg_time");

                    b.Property<bool?>("Sex")
                        .Annotation("Relational:ColumnName", "sex");

                    b.HasKey("ID");

                    b.Index("CardID")
                        .Unique();

                    b.Annotation("Relational:TableName", "user_info");
                });

            modelBuilder.Entity("DbBasicApp.Models.LoginInfo", b =>
                {
                    b.HasOne("DbBasicApp.Models.UserInfo")
                        .WithMany()
                        .ForeignKey("UserId");
                });

            modelBuilder.Entity("DbBasicApp.Models.MsgRecord", b =>
                {
                    b.HasOne("DbBasicApp.Models.LoginInfo")
                        .WithMany()
                        .ForeignKey("ReceiverName");

                    b.HasOne("DbBasicApp.Models.LoginInfo")
                        .WithMany()
                        .ForeignKey("SenderName");
                });

            modelBuilder.Entity("DbBasicApp.Models.PaymentRecord", b =>
                {
                    b.HasOne("DbBasicApp.Models.LoginInfo")
                        .WithMany()
                        .ForeignKey("CashierName");

                    b.HasOne("DbBasicApp.Models.LoginInfo")
                        .WithMany()
                        .ForeignKey("UserName");
                });

            modelBuilder.Entity("DbBasicApp.Models.RatingRecord", b =>
                {
                    b.HasOne("DbBasicApp.Models.LoginInfo")
                        .WithMany()
                        .ForeignKey("SupporterName");

                    b.HasOne("DbBasicApp.Models.LoginInfo")
                        .WithMany()
                        .ForeignKey("UserName");
                });

            modelBuilder.Entity("DbBasicApp.Models.UserInfo", b =>
                {
                    b.HasOne("DbBasicApp.Models.TelecomPackage")
                        .WithMany()
                        .ForeignKey("PackageID");
                });
        }
    }
}
