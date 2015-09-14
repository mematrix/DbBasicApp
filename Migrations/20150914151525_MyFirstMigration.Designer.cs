using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using DbBasicApp.Models;

namespace DbBasicApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class MyFirstMigration
    {
        public override string Id
        {
            get { return "20150914151525_MyFirstMigration"; }
        }

        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta7-15540");

            modelBuilder.Entity("DbBasicApp.Models.CashierLoginInfo", b =>
                {
                    b.Property<string>("Name")
                        .Annotation("Relational:ColumnName", "name");

                    b.Property<string>("Password")
                        .Required()
                        .Annotation("Relational:ColumnName", "password");

                    b.Property<int>("UserId")
                        .Annotation("Relational:ColumnName", "user_id");

                    b.Key("Name");

                    b.Annotation("Relational:TableName", "cashier_login_info");
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

                    b.Property<string>("UserName")
                        .Required()
                        .Annotation("Relational:ColumnName", "user_name");

                    b.Key("ID");

                    b.Annotation("Relational:TableName", "rating_record");
                });

            modelBuilder.Entity("DbBasicApp.Models.SupporterLoginInfo", b =>
                {
                    b.Property<string>("Name")
                        .Annotation("Relational:ColumnName", "name");

                    b.Property<string>("Password")
                        .Required()
                        .Annotation("Relational:ColumnName", "password");

                    b.Property<int>("UserId")
                        .Annotation("Relational:ColumnName", "user_id");

                    b.Key("Name");

                    b.Annotation("Relational:TableName", "supporter_login_info");
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

                    b.Property<int>("PackageID")
                        .Annotation("Relational:ColumnName", "pkg_id");

                    b.Property<bool?>("Sex")
                        .Annotation("Relational:ColumnName", "sex");

                    b.Key("ID");

                    b.Annotation("Relational:TableName", "user_info");
                });

            modelBuilder.Entity("DbBasicApp.Models.UserLoginInfo", b =>
                {
                    b.Property<string>("Name")
                        .Annotation("Relational:ColumnName", "name");

                    b.Property<string>("Password")
                        .Required()
                        .Annotation("Relational:ColumnName", "password");

                    b.Property<int>("UserId")
                        .Annotation("Relational:ColumnName", "user_id");

                    b.Key("Name");

                    b.Annotation("Relational:TableName", "user_login_info");
                });

            modelBuilder.Entity("DbBasicApp.Models.CashierLoginInfo", b =>
                {
                    b.Reference("DbBasicApp.Models.UserInfo")
                        .InverseCollection()
                        .ForeignKey("UserId");
                });

            modelBuilder.Entity("DbBasicApp.Models.PaymentRecord", b =>
                {
                    b.Reference("DbBasicApp.Models.CashierLoginInfo")
                        .InverseCollection()
                        .ForeignKey("CashierName");

                    b.Reference("DbBasicApp.Models.UserLoginInfo")
                        .InverseCollection()
                        .ForeignKey("UserName");
                });

            modelBuilder.Entity("DbBasicApp.Models.RatingRecord", b =>
                {
                    b.Reference("DbBasicApp.Models.SupporterLoginInfo")
                        .InverseCollection()
                        .ForeignKey("SupporterName");

                    b.Reference("DbBasicApp.Models.UserLoginInfo")
                        .InverseCollection()
                        .ForeignKey("UserName");
                });

            modelBuilder.Entity("DbBasicApp.Models.SupporterLoginInfo", b =>
                {
                    b.Reference("DbBasicApp.Models.UserInfo")
                        .InverseCollection()
                        .ForeignKey("UserId");
                });

            modelBuilder.Entity("DbBasicApp.Models.UserInfo", b =>
                {
                    b.Reference("DbBasicApp.Models.TelecomPackage")
                        .InverseCollection()
                        .ForeignKey("PackageID");
                });

            modelBuilder.Entity("DbBasicApp.Models.UserLoginInfo", b =>
                {
                    b.Reference("DbBasicApp.Models.UserInfo")
                        .InverseCollection()
                        .ForeignKey("UserId");
                });
        }
    }
}
