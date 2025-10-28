using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocManagementSystem.Common.Migrations
{
    /// <inheritdoc />
    public partial class Add_FK_in_doctors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Doctors_DoctorId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_DoctorId",
                table: "Departments");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Doctors",
                type: "int",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Departments_DepartmentVMId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_DepartmentVMId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "DepartmentVMId",
                table: "Doctors");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_DoctorId",
                table: "Departments",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Doctors_DoctorId",
                table: "Departments",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
