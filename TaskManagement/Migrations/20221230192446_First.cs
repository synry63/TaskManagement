using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UnitMeasure",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitMeasure", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    Partnumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManufacturerCode = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    UnitOfIssueId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materials_UnitMeasure_UnitOfIssueId",
                        column: x => x.UnitOfIssueId,
                        principalTable: "UnitMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<int>(type: "int", nullable: false),
                    TotalDuration = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskMaterialUsage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    UnitOfMeasurementId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskMaterialUsage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskMaterialUsage_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskMaterialUsage_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskMaterialUsage_UnitMeasure_UnitOfMeasurementId",
                        column: x => x.UnitOfMeasurementId,
                        principalTable: "UnitMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "UnitMeasure",
                columns: new[] { "Id", "Name", "Sort" },
                values: new object[,]
                {
                    { 1L, "liters", 1 },
                    { 2L, "meters", 2 },
                    { 3L, "milliliters", 3 },
                    { 4L, "centimeters", 4 }
                });

            migrationBuilder.InsertData(
                table: "Materials",
                columns: new[] { "Id", "ManufacturerCode", "Partnumber", "Price", "UnitOfIssueId" },
                values: new object[] { new Guid("464c51a1-e342-4271-b122-b8c516c7dd65"), 778, "LX777", 108, 2L });

            migrationBuilder.InsertData(
                table: "Materials",
                columns: new[] { "Id", "ManufacturerCode", "Partnumber", "Price", "UnitOfIssueId" },
                values: new object[] { new Guid("49f9d456-7be0-4791-b623-b634a404cdf0"), 48, "AX778", 78, 1L });

            migrationBuilder.InsertData(
                table: "Materials",
                columns: new[] { "Id", "ManufacturerCode", "Partnumber", "Price", "UnitOfIssueId" },
                values: new object[] { new Guid("dfc27adf-dd5f-4ebc-ab14-d9aebe482c77"), 66549, "PO846", 999, 3L });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Description", "MaterialId", "Name", "TotalDuration" },
                values: new object[] { new Guid("561ce88a-5ce6-4a04-9842-ee41bf28fdac"), 777, new Guid("464c51a1-e342-4271-b122-b8c516c7dd65"), "Filtrage TAX", 30 });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Description", "MaterialId", "Name", "TotalDuration" },
                values: new object[] { new Guid("862667cc-d2d4-4a4b-84d5-4ea0282511d0"), 66, new Guid("49f9d456-7be0-4791-b623-b634a404cdf0"), "Reparation FTX", 60 });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Description", "MaterialId", "Name", "TotalDuration" },
                values: new object[] { new Guid("98829086-241d-448a-9394-19f1d47831bd"), 63549, new Guid("dfc27adf-dd5f-4ebc-ab14-d9aebe482c77"), "Assemblage FII", 10 });

            migrationBuilder.InsertData(
                table: "TaskMaterialUsage",
                columns: new[] { "Id", "Amount", "MaterialId", "TaskId", "UnitOfMeasurementId" },
                values: new object[] { new Guid("50a3ba32-b14e-44e8-beb6-97d6d52b5334"), 5000, new Guid("dfc27adf-dd5f-4ebc-ab14-d9aebe482c77"), new Guid("98829086-241d-448a-9394-19f1d47831bd"), 1L });

            migrationBuilder.InsertData(
                table: "TaskMaterialUsage",
                columns: new[] { "Id", "Amount", "MaterialId", "TaskId", "UnitOfMeasurementId" },
                values: new object[] { new Guid("bdc565a0-4ee5-4e06-b24c-84ad0551caaf"), 100, new Guid("49f9d456-7be0-4791-b623-b634a404cdf0"), new Guid("862667cc-d2d4-4a4b-84d5-4ea0282511d0"), 3L });

            migrationBuilder.InsertData(
                table: "TaskMaterialUsage",
                columns: new[] { "Id", "Amount", "MaterialId", "TaskId", "UnitOfMeasurementId" },
                values: new object[] { new Guid("e08fe392-2a93-43c3-a0e3-df186768091f"), 30, new Guid("464c51a1-e342-4271-b122-b8c516c7dd65"), new Guid("561ce88a-5ce6-4a04-9842-ee41bf28fdac"), 4L });

            migrationBuilder.CreateIndex(
                name: "IX_Materials_UnitOfIssueId",
                table: "Materials",
                column: "UnitOfIssueId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskMaterialUsage_MaterialId",
                table: "TaskMaterialUsage",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskMaterialUsage_TaskId",
                table: "TaskMaterialUsage",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskMaterialUsage_UnitOfMeasurementId",
                table: "TaskMaterialUsage",
                column: "UnitOfMeasurementId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_MaterialId",
                table: "Tasks",
                column: "MaterialId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskMaterialUsage");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "UnitMeasure");
        }
    }
}
