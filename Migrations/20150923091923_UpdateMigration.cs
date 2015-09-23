using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace DbBasicApp.Migrations
{
    public partial class UpdateMigration : Migration
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
                    reg_time = table.Column<DateTime>(isNullable: false),
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
                name: "login_info",
                columns: table => new
                {
                    name = table.Column<string>(isNullable: false),
                    level = table.Column<int>(isNullable: false),
                    password = table.Column<string>(isNullable: false),
                    user_id = table.Column<int>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginInfo", x => x.name);
                    table.ForeignKey(
                        name: "FK_LoginInfo_UserInfo_UserId",
                        column: x => x.user_id,
                        principalTable: "user_info",
                        principalColumn: "id");
                });
            migrationBuilder.CreateTable(
                name: "msg_record",
                columns: table => new
                {
                    id = table.Column<int>(isNullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    msg = table.Column<string>(isNullable: false),
                    recv_name = table.Column<string>(isNullable: false),
                    sender_name = table.Column<string>(isNullable: false),
                    time = table.Column<DateTime>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsgRecord", x => x.id);
                    table.ForeignKey(
                        name: "FK_MsgRecord_LoginInfo_ReceiverName",
                        column: x => x.recv_name,
                        principalTable: "login_info",
                        principalColumn: "name");
                    table.ForeignKey(
                        name: "FK_MsgRecord_LoginInfo_SenderName",
                        column: x => x.sender_name,
                        principalTable: "login_info",
                        principalColumn: "name");
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
                        name: "FK_PaymentRecord_LoginInfo_CashierName",
                        column: x => x.cashier_name,
                        principalTable: "login_info",
                        principalColumn: "name");
                    table.ForeignKey(
                        name: "FK_PaymentRecord_LoginInfo_UserName",
                        column: x => x.user_name,
                        principalTable: "login_info",
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
                    time = table.Column<DateTime>(isNullable: false),
                    user_name = table.Column<string>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingRecord", x => x.id);
                    table.ForeignKey(
                        name: "FK_RatingRecord_LoginInfo_SupporterName",
                        column: x => x.supporter_name,
                        principalTable: "login_info",
                        principalColumn: "name");
                    table.ForeignKey(
                        name: "FK_RatingRecord_LoginInfo_UserName",
                        column: x => x.user_name,
                        principalTable: "login_info",
                        principalColumn: "name");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("msg_record");
            migrationBuilder.DropTable("payment_record");
            migrationBuilder.DropTable("rating_record");
            migrationBuilder.DropTable("login_info");
            migrationBuilder.DropTable("user_info");
            migrationBuilder.DropTable("telecom_pkg");
        }
    }
}
