using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetBanking.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    InitialAmmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsMainAccount = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    Limit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AmountAvailable = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Loan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    LoanUser = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    BalanceLoan = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Beneficiary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficiary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beneficiary_Account_AccountNumber",
                        column: x => x.AccountNumber,
                        principalTable: "Account",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PayExpress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IdAccountPaid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayExpress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayExpress_Account_AccountNumber",
                        column: x => x.AccountNumber,
                        principalTable: "Account",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PayExpress_Account_IdAccountPaid",
                        column: x => x.IdAccountPaid,
                        principalTable: "Account",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardPay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCard = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IdAccountPaid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardPay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardPay_Account_IdAccountPaid",
                        column: x => x.IdAccountPaid,
                        principalTable: "Account",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardPay_Card_IdCard",
                        column: x => x.IdCard,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanPay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdLoan = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IdAccountPaid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountPaidCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanPay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanPay_Account_AccountPaidCode",
                        column: x => x.AccountPaidCode,
                        principalTable: "Account",
                        principalColumn: "Code");
                    table.ForeignKey(
                        name: "FK_LoanPay_Loan_IdLoan",
                        column: x => x.IdLoan,
                        principalTable: "Loan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiary_AccountNumber",
                table: "Beneficiary",
                column: "AccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_CardPay_IdAccountPaid",
                table: "CardPay",
                column: "IdAccountPaid");

            migrationBuilder.CreateIndex(
                name: "IX_CardPay_IdCard",
                table: "CardPay",
                column: "IdCard");

            migrationBuilder.CreateIndex(
                name: "IX_LoanPay_AccountPaidCode",
                table: "LoanPay",
                column: "AccountPaidCode");

            migrationBuilder.CreateIndex(
                name: "IX_LoanPay_IdLoan",
                table: "LoanPay",
                column: "IdLoan");

            migrationBuilder.CreateIndex(
                name: "IX_PayExpress_AccountNumber",
                table: "PayExpress",
                column: "AccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_PayExpress_IdAccountPaid",
                table: "PayExpress",
                column: "IdAccountPaid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Beneficiary");

            migrationBuilder.DropTable(
                name: "CardPay");

            migrationBuilder.DropTable(
                name: "LoanPay");

            migrationBuilder.DropTable(
                name: "PayExpress");

            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropTable(
                name: "Loan");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
