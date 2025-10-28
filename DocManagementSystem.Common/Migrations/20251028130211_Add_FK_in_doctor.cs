using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocManagementSystem.Common.Migrations
{
    /// <inheritdoc />
    public partial class Add_FK_in_doctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Departments_DepartmentVMId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_DepartmentVMId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "DepartmentVMId",
                table: "Doctors");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DepartmentId",
                table: "Doctors",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Departments_DepartmentId",
                table: "Doctors",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Departments_DepartmentId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_DepartmentId",
                table: "Doctors");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentVMId",
                table: "Doctors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DepartmentVMId",
                table: "Doctors",
                column: "DepartmentVMId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Departments_DepartmentVMId",
                table: "Doctors",
                column: "DepartmentVMId",
                principalTable: "Departments",
                principalColumn: "Id");
        }
    }
}
