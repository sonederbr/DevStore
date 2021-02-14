using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DevStore.Catalog.Data.Migrations
{
    public partial class UpdateColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("88e0b857-1482-43eb-ab85-78aa3fdfd5b1"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("b87d1c62-bd7f-4cfe-b20e-60f7b84bbd8f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("344723d0-73e8-4908-bbc1-91e973add6b1"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("60e5157c-be4a-43fc-ae04-165554577076"));

            migrationBuilder.DropColumn(
                name: "ClassSize",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "PlacesAvailable",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "EnrollimentLimit",
                table: "Courses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalOfEnrolled",
                table: "Courses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("1f5cc95e-d840-4fb0-aea4-cfe051b436ec"), 1, "Category 1" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("5c53372d-113e-47ca-acaa-b1e34226aa22"), 2, "Category 2" });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Description", "Enable", "EnrollimentLimit", "Image", "Name", "Price", "TotalOfEnrolled", "UpdatedDate", "Video", "EndDate", "StartDate", "NumberOfClasses", "Duration" },
                values: new object[] { new Guid("aa364074-93a0-4742-85b5-2aa996857c79"), new Guid("1f5cc95e-d840-4fb0-aea4-cfe051b436ec"), new DateTime(2021, 2, 14, 19, 30, 9, 490, DateTimeKind.Local).AddTicks(8482), "Description 1", true, 10, "image1.jpg", "Course 1", 10m, 0, null, "video1.mp4", new DateTime(2021, 3, 14, 19, 30, 9, 488, DateTimeKind.Local).AddTicks(1846), new DateTime(2021, 2, 14, 19, 30, 9, 484, DateTimeKind.Local).AddTicks(112), 10, 100 });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Description", "Enable", "EnrollimentLimit", "Image", "Name", "Price", "TotalOfEnrolled", "UpdatedDate", "Video", "EndDate", "StartDate", "NumberOfClasses", "Duration" },
                values: new object[] { new Guid("e0580a65-4e31-44bc-8bb5-7391892a4d56"), new Guid("5c53372d-113e-47ca-acaa-b1e34226aa22"), new DateTime(2021, 2, 14, 19, 30, 9, 492, DateTimeKind.Local).AddTicks(4075), "Description 2", true, 20, "image2.jpg", "Course 2", 20m, 0, null, "video1.mp4", new DateTime(2021, 4, 14, 19, 30, 9, 489, DateTimeKind.Local).AddTicks(7821), new DateTime(2021, 2, 14, 19, 30, 9, 489, DateTimeKind.Local).AddTicks(7790), 20, 200 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("aa364074-93a0-4742-85b5-2aa996857c79"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("e0580a65-4e31-44bc-8bb5-7391892a4d56"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1f5cc95e-d840-4fb0-aea4-cfe051b436ec"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5c53372d-113e-47ca-acaa-b1e34226aa22"));

            migrationBuilder.DropColumn(
                name: "EnrollimentLimit",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "TotalOfEnrolled",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "ClassSize",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlacesAvailable",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
        }
    }
}
