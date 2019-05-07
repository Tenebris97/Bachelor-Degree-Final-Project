using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FinalProject.Data.Migrations
{
    public partial class AddLaptopCellphone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cellphones",
                columns: table => new
                {
                    CellphoneId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CameraHas = table.Column<string>(nullable: true),
                    CameraRecording = table.Column<string>(nullable: true),
                    CameraResolution = table.Column<string>(nullable: true),
                    ConnectionNetworks = table.Column<string>(nullable: true),
                    ConnectionTechnologies = table.Column<string>(nullable: true),
                    CpuChipset = table.Column<string>(nullable: true),
                    CpuCore = table.Column<string>(nullable: true),
                    CpuFrequency = table.Column<string>(nullable: true),
                    CpuType = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    GPU = table.Column<string>(nullable: true),
                    OS = table.Column<string>(nullable: true),
                    OSVersion = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    RAM = table.Column<int>(nullable: false),
                    ScreenProtector = table.Column<string>(nullable: true),
                    ScreenSize = table.Column<string>(nullable: true),
                    ScreenTechnology = table.Column<string>(nullable: true),
                    ScreenType = table.Column<string>(nullable: true),
                    SimcardCount = table.Column<int>(nullable: false),
                    SimcardDesc = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true),
                    Storage = table.Column<int>(nullable: false),
                    StorageSupport = table.Column<int>(nullable: false),
                    StorageType = table.Column<string>(nullable: true),
                    Weight = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cellphones", x => x.CellphoneId);
                    table.ForeignKey(
                        name: "FK_cellphones_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "laptops",
                columns: table => new
                {
                    LaptopId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BatteryType = table.Column<string>(nullable: true),
                    Bluetooth = table.Column<string>(nullable: true),
                    CpuCache = table.Column<int>(nullable: false),
                    CpuFrequency = table.Column<string>(nullable: true),
                    CpuManufactor = table.Column<string>(nullable: true),
                    CpuSeries = table.Column<string>(nullable: true),
                    CpuType = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    GPUManufactor = table.Column<string>(nullable: true),
                    GPUModel = table.Column<string>(nullable: true),
                    GPUSize = table.Column<string>(nullable: true),
                    HDMI = table.Column<string>(nullable: true),
                    Modem = table.Column<string>(nullable: true),
                    ODD = table.Column<string>(nullable: true),
                    OS = table.Column<string>(nullable: true),
                    OSVersion = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    RAM = table.Column<int>(nullable: false),
                    RAMType = table.Column<string>(nullable: true),
                    ScreenSize = table.Column<string>(nullable: true),
                    ScreenTechnology = table.Column<string>(nullable: true),
                    ScreenType = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true),
                    Speaker = table.Column<string>(nullable: true),
                    Storage = table.Column<string>(nullable: true),
                    StorageType = table.Column<string>(nullable: true),
                    USB2 = table.Column<string>(nullable: true),
                    USB3 = table.Column<string>(nullable: true),
                    VGA = table.Column<string>(nullable: true),
                    Webcam = table.Column<string>(nullable: true),
                    Weight = table.Column<int>(nullable: false),
                    Wifi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_laptops", x => x.LaptopId);
                    table.ForeignKey(
                        name: "FK_laptops_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cellphones_ProductId",
                table: "cellphones",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_laptops_ProductId",
                table: "laptops",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
