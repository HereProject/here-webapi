using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace here_webapi.Migrations
{
    public partial class Yoklamav1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcilanDersler",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DersId = table.Column<int>(nullable: false),
                    SonGecerlilik = table.Column<DateTime>(nullable: false),
                    Key = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcilanDersler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcilanDersler_Dersler_DersId",
                        column: x => x.DersId,
                        principalTable: "Dersler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "YoklananOgrenciler",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DersId = table.Column<int>(nullable: false),
                    OgrenciId = table.Column<int>(nullable: false),
                    Key = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YoklananOgrenciler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YoklananOgrenciler_Dersler_DersId",
                        column: x => x.DersId,
                        principalTable: "Dersler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_YoklananOgrenciler_AspNetUsers_OgrenciId",
                        column: x => x.OgrenciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcilanDersler_DersId",
                table: "AcilanDersler",
                column: "DersId");

            migrationBuilder.CreateIndex(
                name: "IX_YoklananOgrenciler_DersId",
                table: "YoklananOgrenciler",
                column: "DersId");

            migrationBuilder.CreateIndex(
                name: "IX_YoklananOgrenciler_OgrenciId",
                table: "YoklananOgrenciler",
                column: "OgrenciId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcilanDersler");

            migrationBuilder.DropTable(
                name: "YoklananOgrenciler");
        }
    }
}
