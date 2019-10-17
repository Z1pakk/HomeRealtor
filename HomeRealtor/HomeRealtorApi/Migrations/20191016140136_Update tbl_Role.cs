using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeRealtorApi.Migrations
{
    public partial class Updatetbl_Role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tbl_Roles",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldMaxLength: 20);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "tbl_Roles",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);
        }
    }
}
