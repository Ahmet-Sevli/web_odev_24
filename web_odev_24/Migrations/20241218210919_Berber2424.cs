using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_odev_24.Migrations
{
    /// <inheritdoc />
    public partial class Berber2424 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Randevular",
                columns: table => new
                {
                    randevuID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    randevu_tarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    randevu_saati = table.Column<TimeSpan>(type: "time", nullable: false),
                    islemID = table.Column<int>(type: "int", nullable: false),
                    calisanID = table.Column<int>(type: "int", nullable: false),
                    musteriID = table.Column<int>(type: "int", nullable: false),
                    onay_durumu = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevular", x => x.randevuID);
                    table.ForeignKey(
                        name: "FK_Randevular_Calisanlar_calisanID",
                        column: x => x.calisanID,
                        principalTable: "Calisanlar",
                        principalColumn: "calisanID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Randevular_Islemler_islemID",
                        column: x => x.islemID,
                        principalTable: "Islemler",
                        principalColumn: "islemID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Randevular_Musteriler_musteriID",
                        column: x => x.musteriID,
                        principalTable: "Musteriler",
                        principalColumn: "musteriID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_calisanID",
                table: "Randevular",
                column: "calisanID");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_islemID",
                table: "Randevular",
                column: "islemID");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_musteriID",
                table: "Randevular",
                column: "musteriID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Randevular");
        }
    }
}
