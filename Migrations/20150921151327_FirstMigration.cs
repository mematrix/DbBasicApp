using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace DbBasicApp.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "telecom_pkg",
                columns: table => new
                {
                    id = table.Column<int>(isNullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    base_usage = table.Column<int>(isNullable: false),
                    name = table.Column<string>(isNullable: false),
                    out_price = table.Column<int>(isNullable: false),
                    price = table.Column<int>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelecomPackage", x => x.id);
                });
            migrationBuilder.CreateTable(
                name: "user_info",
                columns: table => new
                {
                    id = table.Column<int>(isNullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    balance = table.Column<int>(isNullable: false),
                    birthday = table.Column<DateTime>(isNullable: true),
                    card_id = table.Column<string>(isNullable: true),
                    current_usage = table.Column<int>(isNullable: false),
                    last_usage = table.Column<int>(isNullable: false),
                    name = table.Column<string>(isNullable: false),
                    pkg_id = table.Column<int>(isNullable: true),
                    sex = table.Column<bool>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.id);
                    table.UniqueConstraint("AK_UserInfo_CardID", x => x.card_id);
                    table.ForeignKey(
                        name: "FK_UserInfo_TelecomPackage_PackageID",
                        column: x => x.pkg_id,
                        principalTable: "telecom_pkg",
                        principalColumn: "id");
                });
            migrationBuilder.CreateTable(
                name: "cashier_login_info",
                columns: table => new
                {
                    name = table.Column<string>(isNullable: false),
                    password = table.Column<string>(isNullable: false),
                    user_id = table.Column<int>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashierLoginInfo", x => x.name);
                    table.ForeignKey(
                        name: "FK_CashierLoginInfo_UserInfo_UserId",
                        column: x => x.user_id,
                        principalTable: "user_info",
                        principalColumn: "id");
                });
            migrationBuilder.CreateTable(
                name: "supporter_login_info",
                columns: table => new
                {
                    name = table.Column<string>(isNullable: false),
                    password = table.Column<string>(isNullable: false),
                    user_id = table.Column<int>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupporterLoginInfo", x => x.name);
                    table.ForeignKey(
                        name: "FK_SupporterLoginInfo_UserInfo_UserId",
                        column: x => x.user_id,
                        principalTable: "user_info",
                        principalColumn: "id");
                });
            migrationBuilder.CreateTable(
                name: "user_login_info",
                columns: table => new
                {
                    name = table.Column<string>(isNullable: false),
                    password = table.Column<string>(isNullable: false),
                    user_id = table.Column<int>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLoginInfo", x => x.name);
                    table.ForeignKey(
                        name: "FK_UserLoginInfo_UserInfo_UserId",
                        column: x => x.user_id,
                        principalTable: "user_info",
                        principalColumn: "id");
                });
            migrationBuilder.CreateTable(
                name: "payment_record",
                columns: table => new
                {
                    id = table.Column<int>(isNullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    cashier_name = table.Column<string>(isNullable: true),
                    msg = table.Column<string>(isNullable: true),
                    pay_out = table.Column<int>(isNullable: false),
                    time = table.Column<DateTime>(isNullable: false),
                    user_name = table.Column<string>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRecord", x => x.id);
                    table.ForeignKey(
                        name: "FK_PaymentRecord_CashierLoginInfo_CashierName",
                        column: x => x.cashier_name,
                        principalTable: "cashier_login_info",
                        principalColumn: "name");
                    table.ForeignKey(
                        name: "FK_PaymentRecord_UserLoginInfo_UserName",
                        column: x => x.user_name,
                        principalTable: "user_login_info",
                        principalColumn: "name");
                });
            migrationBuilder.CreateTable(
                name: "rating_record",
                columns: table => new
                {
                    id = table.Column<int>(isNullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    rating = table.Column<int>(isNullable: false),
                    rating_msg = table.Column<string>(isNullable: true),
                    supporter_name = table.Column<string>(isNullable: false),
                    user_name = table.Column<string>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingRecord", x => x.id);
                    table.ForeignKey(
                        name: "FK_RatingRecord_SupporterLoginInfo_SupporterName",
                        column: x => x.supporter_name,
                        principalTable: "supporter_login_info",
                        principalColumn: "name");
                    table.ForeignKey(
                        name: "FK_RatingRecord_UserLoginInfo_UserName",
                        column: x => x.user_name,
                        principalTable: "user_login_info",
                        principalColumn: "name");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("payment_record");
            migrationBuilder.DropTable("rating_record");
            migrationBuilder.DropTable("cashier_login_info");
            migrationBuilder.DropTable("supporter_login_info");
            migrationBuilder.DropTable("user_login_info");
            migrationBuilder.DropTable("user_info");
            migrationBuilder.DropTable("telecom_pkg");
        }
    }
}
