using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_odev_24.Migrations
{
    /// <inheritdoc />
    public partial class Berber24 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adminler",
                columns: table => new
                {
                    adminID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    admin_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    admin_sifre = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adminler", x => x.adminID);
                });

            migrationBuilder.CreateTable(
                name: "Calisanlar",
                columns: table => new
                {
                    calisanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    calisan_ad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    calisan_soyad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calisanlar", x => x.calisanID);
                });

            migrationBuilder.CreateTable(
                name: "Musteriler",
                columns: table => new
                {
                    musteriID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    musteri_ad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    musteri_soyad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    musteri_telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    musteri_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    musteri_sifre = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musteriler", x => x.musteriID);
                });

            migrationBuilder.CreateTable(
                name: "Islemler",
                columns: table => new
                {
                    islemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    islem_ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    islem_ucret = table.Column<int>(type: "int", nullable: false),
                    calisanID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Islemler", x => x.islemID);
                    table.ForeignKey(
                        name: "FK_Islemler_Calisanlar_calisanID",
                        column: x => x.calisanID,
                        principalTable: "Calisanlar",
                        principalColumn: "calisanID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Islemler_calisanID",
                table: "Islemler",
                column: "calisanID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adminler");

            migrationBuilder.DropTable(
                name: "Islemler");

            migrationBuilder.DropTable(
                name: "Musteriler");

            migrationBuilder.DropTable(
                name: "Calisanlar");
        }
    }
}
