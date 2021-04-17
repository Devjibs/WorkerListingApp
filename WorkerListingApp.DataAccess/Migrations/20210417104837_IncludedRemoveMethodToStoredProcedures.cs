using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkerListingApp.DataAccess.Migrations
{
    public partial class IncludedRemoveMethodToStoredProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROC usp_RemoveCompany @CompanyId int
                AS
                BEGIN
                    DELETE
                    FROM Companies
                    WHERE CompanyId = @CompanyId
                END
                GO
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
