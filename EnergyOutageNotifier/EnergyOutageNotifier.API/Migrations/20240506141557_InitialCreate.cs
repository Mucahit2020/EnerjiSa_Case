using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergyOutageNotifier.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotificationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    NotificationType = table.Column<byte>(type: "tinyint", nullable: false),
                    AffectedArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotificationDetail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.NotificationId);
                });

            migrationBuilder.CreateTable(
                name: "Outage",
                columns: table => new
                {
                    OutageId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OutageStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OutageETD = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OutageEndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AffectedArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutageCause = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotificationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Outage", x => x.OutageId);
                    table.ForeignKey(
                        name: "FK_Outage_Notification_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notification",
                        principalColumn: "NotificationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Outage_NotificationId",
                table: "Outage",
                column: "NotificationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Outage");

            migrationBuilder.DropTable(
                name: "Notification");
        }
    }
}
