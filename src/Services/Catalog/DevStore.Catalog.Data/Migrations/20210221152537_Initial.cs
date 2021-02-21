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
                values: new object[] { new Guid("14f864db-935e-4fb1-9312-8d10de05f1c0"), 1, "Category 1" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("d48f9583-08c2-4a74-b39c-4327893268a6"), 2, "Category 2" });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Description", "Enable", "EnrollimentLimit", "Image", "Name", "Price", "TotalOfEnrolled", "UpdatedDate", "Video", "EndDate", "StartDate", "NumberOfClasses", "Duration" },
                values: new object[] { new Guid("6d36cf8f-d367-4cef-83a1-277d9165cc6f"), new Guid("14f864db-935e-4fb1-9312-8d10de05f1c0"), new DateTime(2021, 2, 21, 15, 25, 37, 89, DateTimeKind.Local).AddTicks(1636), "Description 1", true, 10, "image1.jpg", "Course 1", 10m, 0, null, "video1.mp4", new DateTime(2021, 3, 21, 15, 25, 37, 86, DateTimeKind.Local).AddTicks(3237), new DateTime(2021, 2, 21, 15, 25, 37, 80, DateTimeKind.Local).AddTicks(3932), 10, 100 });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Description", "Enable", "EnrollimentLimit", "Image", "Name", "Price", "TotalOfEnrolled", "UpdatedDate", "Video", "EndDate", "StartDate", "NumberOfClasses", "Duration" },
                values: new object[] { new Guid("731b46c1-b3d2-4ac8-9151-32fc58fa3863"), new Guid("d48f9583-08c2-4a74-b39c-4327893268a6"), new DateTime(2021, 2, 21, 15, 25, 37, 92, DateTimeKind.Local).AddTicks(4661), "Description 2", true, 20, "image2.jpg", "Course 2", 20m, 0, null, "video1.mp4", new DateTime(2021, 4, 21, 15, 25, 37, 88, DateTimeKind.Local).AddTicks(328), new DateTime(2021, 2, 21, 15, 25, 37, 88, DateTimeKind.Local).AddTicks(270), 20, 200 });

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
