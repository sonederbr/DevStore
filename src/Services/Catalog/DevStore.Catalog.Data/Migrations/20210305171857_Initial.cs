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
                    EnrollimentLimit = table.Column<int>(nullable: false),
                    TotalOfEnrolled = table.Column<int>(nullable: false),
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
                values: new object[] { new Guid("21a9c82a-b1cb-410f-acde-3008a6d975a4"), 1, "Category 1" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("6fd53c98-2394-458d-adaa-a3ea7f7fa66c"), 2, "Category 2" });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Description", "Enable", "EnrollimentLimit", "Image", "Name", "Price", "TotalOfEnrolled", "UpdatedDate", "Video", "EndDate", "StartDate", "NumberOfClasses", "Duration" },
                values: new object[] { new Guid("789290ce-8384-4004-bf6c-8861efbc4875"), new Guid("21a9c82a-b1cb-410f-acde-3008a6d975a4"), new DateTime(2021, 3, 5, 17, 18, 56, 588, DateTimeKind.Local).AddTicks(5225), "Description 1", true, 10, "image1.jpg", "Course 1", 10m, 0, null, "video1.mp4", new DateTime(2021, 4, 5, 17, 18, 56, 584, DateTimeKind.Local).AddTicks(7801), new DateTime(2021, 3, 5, 17, 18, 56, 578, DateTimeKind.Local).AddTicks(2984), 10, 100 });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Description", "Enable", "EnrollimentLimit", "Image", "Name", "Price", "TotalOfEnrolled", "UpdatedDate", "Video", "EndDate", "StartDate", "NumberOfClasses", "Duration" },
                values: new object[] { new Guid("f0a5326c-331d-45f6-be38-69801e9649ab"), new Guid("6fd53c98-2394-458d-adaa-a3ea7f7fa66c"), new DateTime(2021, 3, 5, 17, 18, 56, 590, DateTimeKind.Local).AddTicks(1089), "Description 2", true, 20, "image2.jpg", "Course 2", 20m, 0, null, "video1.mp4", new DateTime(2021, 5, 5, 17, 18, 56, 587, DateTimeKind.Local).AddTicks(3998), new DateTime(2021, 3, 5, 17, 18, 56, 587, DateTimeKind.Local).AddTicks(3962), 20, 200 });

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
