using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace DbBasicApp.Migrations
{
    public partial class Beta8Migra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "telecom_pkg",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    base_usage = table.Column<double>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    out_price = table.Column<double>(nullable: false),
                    price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelecomPackage", x => x.id);
                });
            migrationBuilder.CreateTable(
                name: "user_info",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    balance = table.Column<double>(nullable: false),
                    birthday = table.Column<DateTime>(nullable: true),
                    card_id = table.Column<string>(nullable: false),
                    current_usage = table.Column<double>(nullable: false),
                    last_usage = table.Column<double>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    pkg_id = table.Column<int>(nullable: true),
                    reg_time = table.Column<DateTime>(nullable: false),
                    sex = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.id);
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
                    user_name = table.Column<string>(nullable: false),
                    level = table.Column<int>(nullable: false),
                    password = table.Column<string>(nullable: false),
                    user_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginInfo", x => x.user_name);
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
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    msg = table.Column<string>(nullable: false),
                    recv_name = table.Column<string>(nullable: false),
                    sender_name = table.Column<string>(nullable: false),
                    time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsgRecord", x => x.id);
                    table.ForeignKey(
                        name: "FK_MsgRecord_LoginInfo_ReceiverName",
                        column: x => x.recv_name,
                        principalTable: "login_info",
                        principalColumn: "user_name");
                    table.ForeignKey(
                        name: "FK_MsgRecord_LoginInfo_SenderName",
                        column: x => x.sender_name,
                        principalTable: "login_info",
                        principalColumn: "user_name");
                });
            migrationBuilder.CreateTable(
                name: "payment_record",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    cashier_name = table.Column<string>(nullable: true),
                    msg = table.Column<string>(nullable: true),
                    pay_out = table.Column<double>(nullable: false),
                    time = table.Column<DateTime>(nullable: false),
                    user_name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRecord", x => x.id);
                    table.ForeignKey(
                        name: "FK_PaymentRecord_LoginInfo_CashierName",
                        column: x => x.cashier_name,
                        principalTable: "login_info",
                        principalColumn: "user_name");
                    table.ForeignKey(
                        name: "FK_PaymentRecord_LoginInfo_UserName",
                        column: x => x.user_name,
                        principalTable: "login_info",
                        principalColumn: "user_name");
                });
            migrationBuilder.CreateTable(
                name: "rating_record",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    rating = table.Column<int>(nullable: false),
                    rating_msg = table.Column<string>(nullable: true),
                    supporter_name = table.Column<string>(nullable: false),
                    time = table.Column<DateTime>(nullable: false),
                    user_name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingRecord", x => x.id);
                    table.ForeignKey(
                        name: "FK_RatingRecord_LoginInfo_SupporterName",
                        column: x => x.supporter_name,
                        principalTable: "login_info",
                        principalColumn: "user_name");
                    table.ForeignKey(
                        name: "FK_RatingRecord_LoginInfo_UserName",
                        column: x => x.user_name,
                        principalTable: "login_info",
                        principalColumn: "user_name");
                });
            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_CardID",
                table: "user_info",
                column: "card_id",
                unique: true);
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
