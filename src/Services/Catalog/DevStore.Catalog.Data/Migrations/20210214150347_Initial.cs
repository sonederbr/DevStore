using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DevStore.Catalog.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "varchar(250)", nullable: false),
                    Code = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "varchar(250)", nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", nullable: false),
                    ClassSize = table.Column<int>(nullable: false),
                    PlacesAvailable = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Image = table.Column<string>(type: "varchar(250)", nullable: false),
                    Video = table.Column<string>(type: "varchar(500)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    NumberOfClasses = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    Enable = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("60e5157c-be4a-43fc-ae04-165554577076"), 1, "Category 1" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("344723d0-73e8-4908-bbc1-91e973add6b1"), 2, "Category 2" });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "ClassSize", "CreatedDate", "Description", "Enable", "Image", "Name", "PlacesAvailable", "Price", "UpdatedDate", "Video", "EndDate", "StartDate", "NumberOfClasses", "Duration" },
                values: new object[] { new Guid("88e0b857-1482-43eb-ab85-78aa3fdfd5b1"), new Guid("60e5157c-be4a-43fc-ae04-165554577076"), 10, new DateTime(2021, 2, 14, 15, 3, 46, 829, DateTimeKind.Local).AddTicks(6634), "Description 1", true, "image1.jpg", "Course 1", 10, 10m, null, "video1.mp4", new DateTime(2021, 3, 14, 15, 3, 46, 820, DateTimeKind.Local).AddTicks(5822), new DateTime(2021, 2, 14, 15, 3, 46, 816, DateTimeKind.Local).AddTicks(2557), 10, 100 });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "ClassSize", "CreatedDate", "Description", "Enable", "Image", "Name", "PlacesAvailable", "Price", "UpdatedDate", "Video", "EndDate", "StartDate", "NumberOfClasses", "Duration" },
                values: new object[] { new Guid("b87d1c62-bd7f-4cfe-b20e-60f7b84bbd8f"), new Guid("344723d0-73e8-4908-bbc1-91e973add6b1"), 20, new DateTime(2021, 2, 14, 15, 3, 46, 831, DateTimeKind.Local).AddTicks(3428), "Description 2", true, "image2.jpg", "Course 2", 10, 20m, null, "video1.mp4", new DateTime(2021, 4, 14, 15, 3, 46, 828, DateTimeKind.Local).AddTicks(3980), new DateTime(2021, 2, 14, 15, 3, 46, 828, DateTimeKind.Local).AddTicks(3916), 20, 200 });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CategoryId",
                table: "Courses",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
