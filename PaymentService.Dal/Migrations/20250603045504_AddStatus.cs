using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentService.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:lookups.PaymentStatus", "Pending,Confirmed,Cancelled")
                .OldAnnotation("Npgsql:Enum:lookups.PaymentStatus", "Pending,Confirmed");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:lookups.PaymentStatus", "Pending,Confirmed")
                .OldAnnotation("Npgsql:Enum:lookups.PaymentStatus", "Pending,Confirmed,Cancelled");
        }
    }
}
