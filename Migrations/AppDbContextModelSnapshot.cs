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
                .Annotation("ProductVersion", "7.0.0-beta7-15540");

            modelBuilder.Entity("DbBasicApp.Models.LoginInfo", b =>
                {
                    b.Property<string>("UserName")
                        .Annotation("Relational:ColumnName", "user_name");

                    b.Property<int>("Level")
                        .Annotation("Relational:ColumnName", "level");

                    b.Property<string>("Password")
                        .Required()
                        .Annotation("Relational:ColumnName", "password");

                    b.Property<int>("UserId")
                        .Annotation("Relational:ColumnName", "user_id");

                    b.Key("UserName");

                    b.Annotation("Relational:TableName", "login_info");
                });

            modelBuilder.Entity("DbBasicApp.Models.MsgRecord", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "id");

                    b.Property<string>("Msg")
                        .Required()
                        .Annotation("Relational:ColumnName", "msg");

                    b.Property<string>("ReceiverName")
                        .Required()
                        .Annotation("Relational:ColumnName", "recv_name");

                    b.Property<string>("SenderName")
                        .Required()
                        .Annotation("Relational:ColumnName", "sender_name");

                    b.Property<DateTime>("Time")
                        .Annotation("Relational:ColumnName", "time");

                    b.Key("ID");

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

                    b.Property<int>("PayOut")
                        .Annotation("Relational:ColumnName", "pay_out");

                    b.Property<DateTime>("Time")
                        .Annotation("Relational:ColumnName", "time");

                    b.Property<string>("UserName")
                        .Required()
                        .Annotation("Relational:ColumnName", "user_name");

                    b.Key("ID");

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
                        .Required()
                        .Annotation("Relational:ColumnName", "supporter_name");

                    b.Property<DateTime>("Time")
                        .Annotation("Relational:ColumnName", "time");

                    b.Property<string>("UserName")
                        .Required()
                        .Annotation("Relational:ColumnName", "user_name");

                    b.Key("ID");

                    b.Annotation("Relational:TableName", "rating_record");
                });

            modelBuilder.Entity("DbBasicApp.Models.TelecomPackage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "id");

                    b.Property<int>("BaseUsage")
                        .Annotation("Relational:ColumnName", "base_usage");

                    b.Property<string>("Name")
                        .Required()
                        .Annotation("MaxLength", 50)
                        .Annotation("Relational:ColumnName", "name");

                    b.Property<int>("OutPrice")
                        .Annotation("Relational:ColumnName", "out_price");

                    b.Property<int>("Price")
                        .Annotation("Relational:ColumnName", "price");

                    b.Key("ID");

                    b.Annotation("Relational:TableName", "telecom_pkg");
                });

            modelBuilder.Entity("DbBasicApp.Models.UserInfo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "id");

                    b.Property<int>("Balance")
                        .Annotation("Relational:ColumnName", "balance");

                    b.Property<DateTime?>("Birthday")
                        .Annotation("Relational:ColumnName", "birthday");

                    b.Property<string>("CardID")
                        .Annotation("Relational:ColumnName", "card_id");

                    b.Property<int>("CurrentUsage")
                        .Annotation("Relational:ColumnName", "current_usage");

                    b.Property<int>("LastUsage")
                        .Annotation("Relational:ColumnName", "last_usage");

                    b.Property<string>("Name")
                        .Required()
                        .Annotation("Relational:ColumnName", "name");

                    b.Property<int?>("PackageID")
                        .Annotation("Relational:ColumnName", "pkg_id");

                    b.Property<DateTime>("RegisterTime")
                        .Annotation("Relational:ColumnName", "reg_time");

                    b.Property<bool?>("Sex")
                        .Annotation("Relational:ColumnName", "sex");

                    b.Key("ID");

                    b.AlternateKey("CardID");

                    b.Annotation("Relational:TableName", "user_info");
                });

            modelBuilder.Entity("DbBasicApp.Models.LoginInfo", b =>
                {
                    b.Reference("DbBasicApp.Models.UserInfo")
                        .InverseReference()
                        .ForeignKey("DbBasicApp.Models.LoginInfo", "UserId");
                });

            modelBuilder.Entity("DbBasicApp.Models.MsgRecord", b =>
                {
                    b.Reference("DbBasicApp.Models.LoginInfo")
                        .InverseCollection()
                        .ForeignKey("ReceiverName");

                    b.Reference("DbBasicApp.Models.LoginInfo")
                        .InverseCollection()
                        .ForeignKey("SenderName");
                });

            modelBuilder.Entity("DbBasicApp.Models.PaymentRecord", b =>
                {
                    b.Reference("DbBasicApp.Models.LoginInfo")
                        .InverseCollection()
                        .ForeignKey("CashierName");

                    b.Reference("DbBasicApp.Models.LoginInfo")
                        .InverseCollection()
                        .ForeignKey("UserName");
                });

            modelBuilder.Entity("DbBasicApp.Models.RatingRecord", b =>
                {
                    b.Reference("DbBasicApp.Models.LoginInfo")
                        .InverseCollection()
                        .ForeignKey("SupporterName");

                    b.Reference("DbBasicApp.Models.LoginInfo")
                        .InverseCollection()
                        .ForeignKey("UserName");
                });

            modelBuilder.Entity("DbBasicApp.Models.UserInfo", b =>
                {
                    b.Reference("DbBasicApp.Models.TelecomPackage")
                        .InverseCollection()
                        .ForeignKey("PackageID");
                });
        }
    }
}
