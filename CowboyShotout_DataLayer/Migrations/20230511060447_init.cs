using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CowboyShotout_DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GunModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GunName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxBullets = table.Column<int>(type: "int", nullable: false),
                    BulletsLeft = table.Column<int>(type: "int", nullable: false),
                    IsValid = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GunModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    X = table.Column<double>(type: "float", nullable: false),
                    Y = table.Column<double>(type: "float", nullable: false),
                    Z = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "CowboyModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsValid = table.Column<byte>(type: "tinyint", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: true),
                    ChangedByUserId = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Hair = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Health = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Speed = table.Column<double>(type: "float", nullable: false),
                    HitRate = table.Column<double>(type: "float", nullable: false),
                    GunId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CowboyModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CowboyModels_GunModel_GunId",
                        column: x => x.GunId,
                        principalTable: "GunModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CowboyModels_GunId",
                table: "CowboyModels",
                column: "GunId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CowboyModels");

            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropTable(
                name: "GunModel");
        }
    }
}
