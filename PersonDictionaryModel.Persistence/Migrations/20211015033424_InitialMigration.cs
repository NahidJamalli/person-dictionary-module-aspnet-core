using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonDictionaryModel.Persistence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    PersonalNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    TargetUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                    table.ForeignKey(
                        name: "FK_People_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneNumbers_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RelatedPeople",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RelativeType = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedPeople", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelatedPeople_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "London" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Budapest" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "California" });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "BirthDate", "CityId", "FirstName", "Gender", "LastName", "PersonalNumber", "TargetUrl" },
                values: new object[] { 1, new DateTime(2000, 10, 15, 7, 34, 23, 853, DateTimeKind.Local).AddTicks(9952), 1, "John", 0, "Doe", "xU7u4nqmeAN", null });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "BirthDate", "CityId", "FirstName", "Gender", "LastName", "PersonalNumber", "TargetUrl" },
                values: new object[] { 2, new DateTime(2006, 10, 15, 7, 34, 23, 854, DateTimeKind.Local).AddTicks(6927), 2, "Ella", 1, "Doe", "6TZ5UwQw5Rt", null });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "BirthDate", "CityId", "FirstName", "Gender", "LastName", "PersonalNumber", "TargetUrl" },
                values: new object[] { 3, new DateTime(1989, 10, 15, 7, 34, 23, 854, DateTimeKind.Local).AddTicks(6947), 3, "Marta", 1, "Bella", "cpNb9xJNR8g", null });

            migrationBuilder.InsertData(
                table: "PhoneNumbers",
                columns: new[] { "Id", "Number", "PersonId", "Type" },
                values: new object[,]
                {
                    { 1, "055", 1, 0 },
                    { 2, "051", 1, 0 },
                    { 3, "077", 2, 0 },
                    { 4, "050", 3, 0 }
                });

            migrationBuilder.InsertData(
                table: "RelatedPeople",
                columns: new[] { "Id", "PersonId", "RelativeType" },
                values: new object[,]
                {
                    { 1, 1, 0 },
                    { 2, 1, 2 },
                    { 3, 2, 3 },
                    { 4, 3, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_People_CityId",
                table: "People",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_PersonId",
                table: "PhoneNumbers",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedPeople_PersonId",
                table: "RelatedPeople",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhoneNumbers");

            migrationBuilder.DropTable(
                name: "RelatedPeople");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
