using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Planner.Db.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendees_AttendeeGroups_AttendeeGroupId",
                table: "Attendees");

            migrationBuilder.DropTable(
                name: "AttendeeGroups");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.RenameColumn(
                name: "AttendeeGroupId",
                table: "Attendees",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendees_AttendeeGroupId",
                table: "Attendees",
                newName: "IX_Attendees_EventId");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfSeats",
                table: "Attendees",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SeatIdentifier",
                table: "Attendees",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rows",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    StartingSeat = table.Column<int>(type: "INTEGER", nullable: false),
                    Letter = table.Column<string>(type: "TEXT", nullable: false),
                    EventId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rows_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rows_EventId",
                table: "Rows",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendees_Events_EventId",
                table: "Attendees",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendees_Events_EventId",
                table: "Attendees");

            migrationBuilder.DropTable(
                name: "Rows");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropColumn(
                name: "NumberOfSeats",
                table: "Attendees");

            migrationBuilder.DropColumn(
                name: "SeatIdentifier",
                table: "Attendees");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Attendees",
                newName: "AttendeeGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendees_EventId",
                table: "Attendees",
                newName: "IX_Attendees_AttendeeGroupId");

            migrationBuilder.CreateTable(
                name: "AttendeeGroups",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendeeGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    AttendeeId = table.Column<string>(type: "TEXT", nullable: true),
                    Category = table.Column<int>(type: "INTEGER", nullable: false),
                    OwnerName = table.Column<string>(type: "TEXT", nullable: true),
                    SeatPosition = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_Attendees_AttendeeId",
                        column: x => x.AttendeeId,
                        principalTable: "Attendees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seats_AttendeeId",
                table: "Seats",
                column: "AttendeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendees_AttendeeGroups_AttendeeGroupId",
                table: "Attendees",
                column: "AttendeeGroupId",
                principalTable: "AttendeeGroups",
                principalColumn: "Id");
        }
    }
}
