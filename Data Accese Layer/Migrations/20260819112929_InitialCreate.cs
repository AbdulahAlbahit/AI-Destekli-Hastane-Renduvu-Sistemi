using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Accese_Layer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "HOSPITAL_SYSTEM");

            migrationBuilder.CreateTable(
                name: "DEPARTMENTS",
                schema: "HOSPITAL_SYSTEM",
                columns: table => new
                {
                    DEPT_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DEPT_NAME = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DEPARTME__512A59AC4D33B8B5", x => x.DEPT_ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CLINICS",
                schema: "HOSPITAL_SYSTEM",
                columns: table => new
                {
                    CLINIC_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CLINIC_NUMBER = table.Column<int>(type: "int", nullable: false),
                    DEPT_ID = table.Column<int>(type: "int", nullable: true),
                    DOCTOR_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CLINICS__7A56A460F09ACA09", x => x.CLINIC_ID);
                    table.ForeignKey(
                        name: "FK_CLINICS_DEPARTMENTS",
                        column: x => x.DEPT_ID,
                        principalSchema: "HOSPITAL_SYSTEM",
                        principalTable: "DEPARTMENTS",
                        principalColumn: "DEPT_ID");
                });

            migrationBuilder.CreateTable(
                name: "PATIENTS",
                schema: "HOSPITAL_SYSTEM",
                columns: table => new
                {
                    PATIENT_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PATIENT_NAME = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DATE_OF_BIRTH = table.Column<DateOnly>(type: "date", nullable: false),
                    GENDER = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    PHONE = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PATIENTS__AA0B6068ECFD06AA", x => x.PATIENT_ID);
                    table.ForeignKey(
                        name: "FK_PATIENTS_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DOCTORS",
                schema: "HOSPITAL_SYSTEM",
                columns: table => new
                {
                    DOCTOR_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DOCTOR_NAME = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    SPECIALIZATION = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    PHONE = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    EMAIL = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CLINIC_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DOCTORS__596ABDB06355F749", x => x.DOCTOR_ID);
                    table.ForeignKey(
                        name: "FK_DOCTORS_CLINICS",
                        column: x => x.CLINIC_ID,
                        principalSchema: "HOSPITAL_SYSTEM",
                        principalTable: "CLINICS",
                        principalColumn: "CLINIC_ID");
                });

            migrationBuilder.CreateTable(
                name: "APPOINTMENTS",
                schema: "HOSPITAL_SYSTEM",
                columns: table => new
                {
                    APPOINTMENT_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    THE_DATE = table.Column<DateOnly>(type: "date", nullable: false),
                    THE_TIME = table.Column<TimeOnly>(type: "time", nullable: false),
                    THE_STATUS = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    DOCTOR_ID = table.Column<int>(type: "int", nullable: true),
                    PATIENT_ID = table.Column<int>(type: "int", nullable: true),
                    CLINIC_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__APPOINTM__49B308C65E500E5D", x => x.APPOINTMENT_ID);
                    table.ForeignKey(
                        name: "FK_APPOINTMENTS_CLINICS",
                        column: x => x.CLINIC_ID,
                        principalSchema: "HOSPITAL_SYSTEM",
                        principalTable: "CLINICS",
                        principalColumn: "CLINIC_ID");
                    table.ForeignKey(
                        name: "FK_APPOINTMENTS_DOCTORS",
                        column: x => x.DOCTOR_ID,
                        principalSchema: "HOSPITAL_SYSTEM",
                        principalTable: "DOCTORS",
                        principalColumn: "DOCTOR_ID");
                    table.ForeignKey(
                        name: "FK_APPOINTMENTS_PATIENTS",
                        column: x => x.PATIENT_ID,
                        principalSchema: "HOSPITAL_SYSTEM",
                        principalTable: "PATIENTS",
                        principalColumn: "PATIENT_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_APPOINTMENTS_CLINIC_ID",
                schema: "HOSPITAL_SYSTEM",
                table: "APPOINTMENTS",
                column: "CLINIC_ID");

            migrationBuilder.CreateIndex(
                name: "IX_APPOINTMENTS_DOCTOR_ID",
                schema: "HOSPITAL_SYSTEM",
                table: "APPOINTMENTS",
                column: "DOCTOR_ID");

            migrationBuilder.CreateIndex(
                name: "IX_APPOINTMENTS_PATIENT_ID",
                schema: "HOSPITAL_SYSTEM",
                table: "APPOINTMENTS",
                column: "PATIENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CLINICS_DEPT_ID",
                schema: "HOSPITAL_SYSTEM",
                table: "CLINICS",
                column: "DEPT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_DOCTORS_CLINIC_ID",
                schema: "HOSPITAL_SYSTEM",
                table: "DOCTORS",
                column: "CLINIC_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PATIENTS_UserId",
                schema: "HOSPITAL_SYSTEM",
                table: "PATIENTS",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APPOINTMENTS",
                schema: "HOSPITAL_SYSTEM");

            migrationBuilder.DropTable(
                name: "DOCTORS",
                schema: "HOSPITAL_SYSTEM");

            migrationBuilder.DropTable(
                name: "PATIENTS",
                schema: "HOSPITAL_SYSTEM");

            migrationBuilder.DropTable(
                name: "CLINICS",
                schema: "HOSPITAL_SYSTEM");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "DEPARTMENTS",
                schema: "HOSPITAL_SYSTEM");
        }
    }
}
