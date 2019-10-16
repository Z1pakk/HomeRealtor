using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeRealtorApi.Migrations
{
    public partial class Initialize2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "tbl_Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "tbl_Users",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "tbl_Advertisings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Image = table.Column<string>(nullable: false),
                    StateName = table.Column<string>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Contacts = table.Column<string>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Advertisings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Advertisings_tbl_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Advertisings_UserId",
                table: "tbl_Advertisings",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Advertisings");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "tbl_Users");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "tbl_Users");
        }
    }
}
