using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentSchedulingApi.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedAppointmentDoctorId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Appointments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Appointments",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
