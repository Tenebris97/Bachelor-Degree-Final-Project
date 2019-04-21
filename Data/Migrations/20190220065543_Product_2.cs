using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FinalProject.Data.Migrations
{
    public partial class Product_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_AuthorId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_AuthorId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "products");

            migrationBuilder.CreateIndex(
                name: "IX_products_CategoryId",
                table: "products",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_CategoryId",
                table: "products",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_CategoryId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_CategoryId",
                table: "products");

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_AuthorId",
                table: "products",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_AuthorId",
                table: "products",
                column: "AuthorId",
                principalTable: "categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
