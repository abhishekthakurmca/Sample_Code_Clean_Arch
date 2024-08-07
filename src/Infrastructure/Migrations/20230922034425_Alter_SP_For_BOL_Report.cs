using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Alter_SP_For_BOL_Report : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"
GO

IF EXISTS(SELECT 1 FROM sys.procedures 
          WHERE Name = 'spReports_LogisticsReports')
BEGIN
    DROP PROCEDURE dbo.spReports_LogisticsReports
END 

/****** Object:  StoredProcedure [dbo].[spReports_LogisticsReports]    Script Date: 22/09/2023 9:15:42 am ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:      Justin Page
-- Create Date: 10/18/2020
-- Description: BOL/ShipmentDetails Info Pull
--Modified by Sourav Added new new columns in select query
-- =============================================
CREATE PROCEDURE [dbo].[spReports_LogisticsReports]
(
    @ShipmentID as INT
)
AS
BEGIN
    SET NOCOUNT ON

    SELECT
	ISNULL(RS.Route_Id, 1) as RouteNumber,
	R.PickUpDate,
	R.DockTime,
	RS.FreightType, 
	RS.PlannedQty,
	RS.StatusId,
	RS.ShipmentStatus, 
	RS.SiteID,
	R.Company_Id,
	R.DockDate,
	R.Quote,
	FC.[Name] as FreightClient,
	RS.InternalNotes,
	FC.Address as FCAddr,
	FC.Address1 as FCAddr2,
	FC.City as FCCity,
	LUS.Abbr as FCState,
	FC.ZipCode as FCZip,
	R.DriverName,
	R.TruckNumber,
	R.SealNumber,
	R.TrailerNumber,
	R.DriverID,
	R.FreightCompanyName,
	RS.CollectionPoint,  
    RS.CollectionPointAddress,  
    RS.CollectionPointCity,  
    RS.CollectionPointState,  
    RS.CollectionPointZip,  
    RS.IsInterCompany,  
   Rs.IsTransfer  
FROM RouteShipments RS
LEFT JOIN Route R ON R.Id = RS.Route_Id
LEFT JOIN FreightCompany FC ON R.Company_Id = FC.Id
LEFT JOIN LU_State LUS ON LUS.ID = FC.State
WHERE RS.ShipmentId = @ShipmentID order by R.Id DESC
END
GO
");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE dbo.spReports_LogisticsReports GO");
        }
    }
}
