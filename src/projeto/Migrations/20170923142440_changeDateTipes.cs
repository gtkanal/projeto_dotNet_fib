using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace projeto.Migrations
{
    public partial class changeDateTipes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DataInicio",
                table: "Emprestimo",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "DataFim",
                table: "Emprestimo",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataDevolucao",
                table: "Emprestimo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataInicio",
                table: "Emprestimo",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataFim",
                table: "Emprestimo",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataDevolucao",
                table: "Emprestimo",
                nullable: false);
        }
    }
}
