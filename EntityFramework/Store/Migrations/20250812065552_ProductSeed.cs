using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Store.Migrations
{
    /// <inheritdoc />
    public partial class ProductSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, "Beras Sania 5kg", 68000m, 50 },
                    { 2, "Minyak Goreng Bimoli 2L", 35000m, 80 },
                    { 3, "Indomie Goreng", 3000m, 500 },
                    { 4, "Telur Ayam 1kg", 27000m, 40 },
                    { 5, "Gula Pasir Gulaku 1kg", 16000m, 100 },
                    { 6, "Susu UHT Ultra Milk Coklat 1L", 18000m, 60 },
                    { 7, "Kopi Kapal Api Special 165g", 15000m, 120 },
                    { 8, "Teh Celup Sariwangi", 8000m, 150 },
                    { 9, "Saus Sambal ABC", 12000m, 90 },
                    { 10, "Kecap Manis Bango", 22000m, 70 },
                    { 11, "Buku Tulis Sinar Dunia 38 Lbr", 5000m, 250 },
                    { 12, "Pensil 2B Faber-Castell", 3000m, 300 },
                    { 13, "Pulpen Pilot G-2", 25000m, 100 },
                    { 14, "Penghapus Staedtler", 4000m, 180 },
                    { 15, "Kertas HVS A4 70gr Rim", 55000m, 60 },
                    { 16, "Stabilo Boss Original", 11000m, 80 },
                    { 17, "Tipe-X Cair Kenko", 7000m, 120 },
                    { 18, "Sabun Mandi Lifebuoy", 4500m, 200 },
                    { 19, "Shampo Pantene Anti Dandruff", 28000m, 90 },
                    { 20, "Pasta Gigi Pepsodent", 13000m, 150 },
                    { 21, "Deterjen Rinso Anti Noda 1.8kg", 45000m, 70 },
                    { 22, "Pewangi Pakaian Molto", 15000m, 110 },
                    { 23, "Pembersih Lantai Super Pell", 14000m, 100 },
                    { 24, "Sabun Cuci Piring Sunlight", 17000m, 130 },
                    { 25, "Tisu Wajah Paseo", 19000m, 80 },
                    { 26, "Pengharum Ruangan Glade", 25000m, 60 },
                    { 27, "Lampu LED Philips 10W", 30000m, 100 },
                    { 28, "Baterai ABC Alkaline AA", 15000m, 200 },
                    { 29, "Kabel USB-C Anker Powerline", 150000m, 40 },
                    { 30, "Mouse Logitech M185", 180000m, 30 },
                    { 31, "Headset Rexus F22", 120000m, 50 },
                    { 32, "Stop Kontak Uticon 4 Lubang", 65000m, 70 },
                    { 33, "Coca-Cola Kaleng", 7000m, 300 },
                    { 34, "Air Mineral Aqua 600ml", 3500m, 400 },
                    { 35, "Chitato Sapi Panggang", 11000m, 250 },
                    { 36, "Oreo Original", 9000m, 180 },
                    { 37, "Silverqueen Cashew", 14000m, 150 },
                    { 38, "Pocari Sweat 500ml", 8000m, 220 },
                    { 39, "Masker Medis Sensi", 25000m, 100 },
                    { 40, "Hand Sanitizer Dettol", 20000m, 120 },
                    { 41, "Tolak Angin Cair Sido Muncul", 4000m, 300 },
                    { 42, "Vitamin C IPI", 10000m, 200 },
                    { 43, "Plester Hansaplast", 8000m, 150 },
                    { 44, "Payung Lipat", 75000m, 50 },
                    { 45, "Jas Hujan Axio", 250000m, 30 },
                    { 46, "Sandal Jepit Swallow", 15000m, 180 },
                    { 47, "Gembok Solid", 50000m, 80 },
                    { 48, "Lem Super Glue Alteco", 7000m, 140 },
                    { 49, "Korek Api Gas", 3000m, 400 },
                    { 50, "Gas LPG 3kg", 22000m, 60 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
