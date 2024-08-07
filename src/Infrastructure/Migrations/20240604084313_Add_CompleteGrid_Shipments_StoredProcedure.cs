using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Add_CompleteGrid_Shipments_StoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApprovedInvoiceDetails");

            //Store Procedure 
            migrationBuilder.Sql(@"
IF EXISTS(SELECT 1 FROM sys.procedures 
          WHERE Name = 'GetCompleteShipmentsList')
BEGIN
    DROP PROCEDURE dbo.GetCompleteShipmentsList
END 

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCompleteShipmentsList]
    @OrderDateAgingDays NVARCHAR(50) = NULL,
    @ReceivingFormType NVARCHAR(50) = NULL,
    @RouteShipmentId INT = NULL,
    @CityState NVARCHAR(50) = NULL,
    @InBoundArrangedByCustomer NVARCHAR(50) = NULL,
	@ClientName NVARCHAR(100) = NULL,
    @FreightCompany NVARCHAR(100) = NULL,
    @DockDate DATETIME = NULL,
	@PickUpDate DATETIME = NULL,
	@ConfirmDate DATETIME=NULL,
    @ERISite NVARCHAR(100) = NULL,
    @Quote NVARCHAR(100) = NULL,
    @FreightCharges FLOAT = NULL,
    @ShipmentStatus NVARCHAR(50) = NULL,	
	@OrderAgingDay DATE = NULL,
    @Sort NVARCHAR(50) = NULL,
    @Ascending BIT = 1,
    @Page INT = 1,
    @PageSize INT = 15,
    @TotalRecordCount INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @date DATE = GETDATE();
    DECLARE @agingDays DATETIME;
    
    -- Convert aging days to a date
    IF ISNUMERIC(@OrderDateAgingDays) = 1
    BEGIN  SET @agingDays = DATEADD(DAY, -CAST(@OrderDateAgingDays AS INT), GETDATE());END
    ELSE
    BEGIN  SET @agingDays = NULL; END
    DECLARE @removedSites TABLE (SiteName NVARCHAR(100));
    DECLARE @completeSites TABLE (SiteName NVARCHAR(100));
    INSERT INTO @removedSites VALUES 
    ('badin, nc'), ('dallas, tx'), ('denver, co'), ('fresno, ca'), ('goodyear, az'), ('holliston, ma'), ('lincoln park, nj'),('plainfield, in'), ('seattle, wa');  
    INSERT INTO @completeSites VALUES 
    ('eri satellite site'), ('eri brokerage'), ('eri international site');

    SELECT @TotalRecordCount = COUNT(DISTINCT rs.Id)
    FROM RouteShipments rs
    JOIN Route r ON rs.Route_Id = r.Id
    LEFT JOIN FreightCompany c ON r.Company_Id = c.Id
    LEFT JOIN FreightInvoices fi ON r.Id = fi.Route_Id AND fi.IsDeleted = 0
    WHERE 
        (rs.Tendered = 1 AND rs.StatusId NOT IN (0,1, 2, 5,11)
        OR ( (NOT EXISTS (SELECT 1 FROM @removedSites rs1 WHERE rs1.SiteName = LOWER(rs.SiteName)) AND rs.StatusId = 2 AND r.DockDate < @date)OR EXISTS (SELECT 1 FROM @completeSites cs WHERE cs.SiteName = LOWER(rs.SiteName))))
        AND (@ClientName IS NULL OR rs.Client LIKE '%' + @ClientName + '%')
        AND (@FreightCompany IS NULL OR r.FreightCompanyName LIKE '%' + @FreightCompany + '%')
        AND (@DockDate IS NULL OR CONVERT(NVARCHAR(10), r.DockDate, 101) LIKE '%' + CONVERT(NVARCHAR(10), @DockDate, 101) + '%')
        AND (@PickUpDate IS NULL OR CONVERT(NVARCHAR(10), r.PickUpDate, 101) LIKE '%' + CONVERT(NVARCHAR(10), @PickUpDate, 101) + '%')
		AND (@OrderDateAgingDays IS NULL OR CONVERT(NVARCHAR(10), rs.OrderDate, 101) LIKE '%' + CONVERT(NVARCHAR(10), @OrderDateAgingDays, 101) + '%')
		AND (@ConfirmDate IS NULL OR CONVERT(NVARCHAR(10), rs.ApprovalDate, 101) LIKE '%' + CONVERT(NVARCHAR(10), @ConfirmDate, 101) + '%')
        AND (@ERISite IS NULL OR rs.SiteName LIKE '%' + @ERISite + '%')
		AND (@Quote IS NULL OR (CASE WHEN rs.IsInterCompany = 1 OR rs.IsTransfer = 1 THEN CASE WHEN rs.IsArrangedByCustomer = 1 THEN CASE WHEN rs.IsInbound = 1 THEN '0' ELSE CAST(r.Quote AS NVARCHAR) END ELSE CAST(r.Quote AS NVARCHAR) END ELSE CAST(r.Quote AS NVARCHAR) END LIKE '%' + @Quote + '%'))
        AND (@FreightCharges IS NULL OR (CASE WHEN rs.IsInterCompany = 1 OR rs.IsTransfer = 1 THEN 
		       CASE WHEN rs.IsInbound = 1 THEN '0' ELSE CASE WHEN EXISTS (SELECT 1 FROM FreightInvoices fi WHERE fi.Route_Id = r.Id AND fi.IsDeleted = 0) THEN
			    CAST((SELECT SUM(fi.GrandTotal) FROM FreightInvoices fi WHERE fi.Route_Id = r.Id AND fi.IsDeleted = 0) * (rs.FreightCostResponsibilityPercentage / 100) AS NVARCHAR)  
				   ELSE '0' END END END LIKE '%' + CAST(@FreightCharges AS NVARCHAR)) OR (rs.IsInterCompany = 0 AND rs.IsTransfer = 0 AND (SELECT SUM(fi.GrandTotal) FROM FreightInvoices fi WHERE fi.Route_Id = r.Id AND fi.IsDeleted = 0) / (SELECT COUNT(*) FROM RouteShipments WHERE Route_Id = r.Id) = @FreightCharges))
        AND (@ShipmentStatus IS NULL OR rs.ShipmentStatus LIKE '%' + @ShipmentStatus + '%')
        AND (@OrderAgingDay IS NULL OR (rs.ApprovalDate IS NOT NULL AND CAST(rs.ApprovalDate AS DATE) = @OrderAgingDay))
        AND (@ReceivingFormType IS NULL OR LOWER(rs.FreightType) LIKE '%' + LOWER(@ReceivingFormType) + '%' OR LOWER(rs.ReceivingTypeName) LIKE '%' + LOWER(@ReceivingFormType) + '%')
        AND (@RouteShipmentId IS NULL OR rs.Route_Id = @RouteShipmentId OR rs.ShipmentId = @RouteShipmentId)
        AND (@CityState IS NULL OR LOWER(rs.CollectionPointCity) LIKE '%' + @CityState + '%' OR LOWER(rs.CollectionPointState) LIKE '%' + @CityState + '%')
        AND (@InBoundArrangedByCustomer IS NULL AND rs.Route_Id IS NOT NULL OR (@InBoundArrangedByCustomer = 'in' AND rs.IsInbound = 1) OR (@InBoundArrangedByCustomer = 'out' AND rs.IsInbound = 0)  OR (@inBoundArrangedByCustomer = 'yes' AND rs.IsArrangedByCustomer = 1) OR (@inBoundArrangedByCustomer = 'no' AND rs.IsArrangedByCustomer = 0));       

    SELECT 
        rs.Id As Id,
        rs.ShipmentId as ShipmentId,
        rs.StatusId as StatusId,
        rs.Route_Id As Route_Id ,
        c.Name AS FreightCompany,
        rs.Client AS ClientName,
        rs.CollectionPoint,
        rs.CollectionPointAddress,
        ISNULL(rs.CollectionPointCity, '') AS City,
        ISNULL(rs.CollectionPointState, '') AS State,
        rs.CollectionPointAddress2,
        rs.FreightType as FreightType,
        rs.CollectionPointZip,
        rs.PlannedQty as PlannedQty,
        rs.Site as Site,
        rs.Tendered as Tendered,
	    CASE 
         WHEN rs.StatusId = 0 THEN 'Pending'
         WHEN rs.StatusId = 1 THEN 'Ready For Scheduling'
         WHEN rs.StatusId = 2 THEN 'Scheduled'
         WHEN rs.StatusId = 3 THEN 'Unloaded'
         WHEN rs.StatusId = 4 THEN 'Loaded'
         WHEN rs.StatusId = 5 THEN 'Canceled'
         WHEN rs.StatusId = 6 THEN 'Reviewed'
         WHEN rs.StatusId = 7 THEN 'WIP'
         WHEN rs.StatusId = 8 THEN 'Review Pending'
         WHEN rs.StatusId = 9 THEN 'Labels Generated'
         WHEN rs.StatusId = 10 THEN 'Delivered'
         WHEN rs.StatusId = 14 THEN 'Awaiting Pricing'
         WHEN rs.StatusId = 15 THEN 'Process Pending'
        END AS Status,
        rs.ShipmentStatus as ShipmentStatus,
        r.DockDate as DockDate,
        rs.SiteName AS ERISite,
        rs.ApprovalDate AS ConfirmDate,
		rs.ReceivingTypeName AS ReceivingType,
		rs.OrderDate AS OrderCreateDate,
        CASE  WHEN rs.ApprovalDate IS NOT NULL THEN DATEDIFF(DAY, rs.ApprovalDate, GETDATE())  ELSE 0  END AS AgingDays,
        rs.IsInbound,
        rs.IsArrangedByCustomer,
        rs.IsInterCompany,
        CASE WHEN rs.IsInterCompany = 1 OR rs.IsTransfer = 1 THEN CASE WHEN rs.IsArrangedByCustomer = 1 THEN CASE WHEN rs.IsInbound = 1 THEN '0' ELSE CAST(r.Quote AS NVARCHAR) END ELSE CAST(r.Quote AS NVARCHAR) END ELSE CAST(r.Quote AS NVARCHAR) END AS Quote,
        r.PickUpDate,
        CASE  WHEN r.PickUpTime IS NULL OR LOWER(r.PickUpTime) = 'null' THEN NULL  ELSE CAST(r.PickUpTime AS DATETIME)END AS PickUpTime,
        CASE  WHEN r.DockTime IS NULL OR LOWER(r.DockTime) = 'null' THEN NULL  ELSE CAST(r.DockTime AS DATETIME)END AS DockTime,
        rs.IsTransfer,
    CASE WHEN rs.IsInterCompany = 1 OR rs.IsTransfer = 1 THEN CASE WHEN rs.IsInbound = 1 THEN '0' ELSE CASE WHEN EXISTS (SELECT 1 FROM FreightInvoices fi WHERE fi.Route_Id = r.Id AND fi.IsDeleted = 0) THEN CAST((SELECT SUM(fi.GrandTotal) FROM FreightInvoices fi WHERE fi.Route_Id = r.Id AND fi.IsDeleted = 0) * (rs.FreightCostResponsibilityPercentage / 100) AS NVARCHAR) ELSE '0' END END ELSE CAST((SELECT SUM(fi.GrandTotal) FROM FreightInvoices fi WHERE fi.Route_Id = r.Id AND fi.IsDeleted = 0) / (SELECT COUNT(*) FROM RouteShipments WHERE Route_Id = r.Id) AS NVARCHAR) END AS FreightCharges
    FROM RouteShipments rs
    JOIN Route r ON rs.Route_Id = r.Id
    LEFT JOIN FreightCompany c ON r.Company_Id = c.Id
    LEFT JOIN FreightInvoices fi ON r.Id = fi.Route_Id AND fi.IsDeleted = 0
    WHERE 
        (rs.Tendered = 1 AND rs.StatusId NOT IN (0,1, 2, 5,11) OR ((NOT EXISTS (SELECT 1 FROM @removedSites rs1 WHERE rs1.SiteName = LOWER(rs.SiteName)) AND rs.StatusId = 2 AND r.DockDate < @date) OR EXISTS (SELECT 1 FROM @completeSites cs WHERE cs.SiteName = LOWER(rs.SiteName))))
        AND (@ClientName IS NULL OR rs.Client LIKE '%' + @ClientName + '%')
        AND (@FreightCompany IS NULL OR r.FreightCompanyName LIKE '%' + @FreightCompany + '%')
        AND (@DockDate IS NULL OR CONVERT(NVARCHAR(10), r.DockDate, 101) LIKE '%' + CONVERT(NVARCHAR(10), @DockDate, 101) + '%')
        AND (@PickUpDate IS NULL OR CONVERT(NVARCHAR(10), r.PickUpDate, 101) LIKE '%' + CONVERT(NVARCHAR(10), @PickUpDate, 101) + '%')
        AND (@ERISite IS NULL OR rs.SiteName LIKE '%' + @ERISite + '%')
		AND (@ConfirmDate IS NULL OR CONVERT(NVARCHAR(10), rs.ApprovalDate, 101) LIKE '%' + CONVERT(NVARCHAR(10), @ConfirmDate, 101) + '%')
		AND (@OrderDateAgingDays IS NULL OR CONVERT(NVARCHAR(10), rs.OrderDate, 101) LIKE '%' + CONVERT(NVARCHAR(10), @OrderDateAgingDays, 101) + '%')
        AND (@Quote IS NULL OR (CASE  WHEN (rs.IsInterCompany = 1 OR rs.IsTransfer = 1) AND rs.IsArrangedByCustomer = 1 AND rs.IsInbound = 1 THEN '0.000'ELSE CAST(r.Quote AS NVARCHAR) END LIKE '%' + @Quote + '%'))              
		AND (@FreightCharges IS NULL OR (CASE WHEN rs.IsInterCompany = 1 OR rs.IsTransfer = 1 THEN CASE WHEN rs.IsInbound = 1 THEN 0 ELSE CASE WHEN (SELECT COUNT(*) FROM FreightInvoices fi WHERE fi.Route_Id = r.Id AND fi.IsDeleted = 0) > 0 THEN (SELECT SUM(fi.GrandTotal) FROM FreightInvoices fi WHERE fi.Route_Id = r.Id AND fi.IsDeleted = 0) * (rs.FreightCostResponsibilityPercentage / 100) ELSE 0 END END ELSE COALESCE((SELECT SUM(fi.GrandTotal) FROM FreightInvoices fi WHERE fi.Route_Id = r.Id AND fi.IsDeleted = 0) / NULLIF((SELECT COUNT(*) FROM RouteShipments WHERE Route_Id = r.Id), 0), 0) END LIKE '%' + CONVERT(NVARCHAR(10), @FreightCharges, 101) + '%'))
        AND (@ShipmentStatus IS NULL OR rs.ShipmentStatus LIKE '%' + @ShipmentStatus + '%')
        AND (@OrderAgingDay IS NULL OR (rs.ApprovalDate IS NOT NULL AND CAST(rs.ApprovalDate AS DATE) = @OrderAgingDay))
        AND (@ReceivingFormType IS NULL OR LOWER(rs.FreightType) LIKE '%' + LOWER(@ReceivingFormType) + '%' OR LOWER(rs.ReceivingTypeName) LIKE '%' + LOWER(@ReceivingFormType) + '%')
        AND (@RouteShipmentId IS NULL OR rs.Route_Id = @RouteShipmentId OR rs.ShipmentId = @RouteShipmentId)
        AND (@CityState IS NULL OR LOWER(rs.CollectionPointCity) LIKE '%' + @CityState + '%' OR LOWER(rs.CollectionPointState) LIKE '%' + @CityState + '%')    
        AND (@InBoundArrangedByCustomer IS NULL AND rs.Route_Id IS NOT NULL OR (@InBoundArrangedByCustomer = 'in' AND rs.IsInbound = 1) OR (@InBoundArrangedByCustomer = 'out' AND rs.IsInbound = 0) OR (@inBoundArrangedByCustomer = 'yes' AND rs.IsArrangedByCustomer = 1)OR (@inBoundArrangedByCustomer = 'no' AND rs.IsArrangedByCustomer = 0))      
               ORDER BY     
		     CASE WHEN @Sort = 'Route_Id' AND @Ascending = 1 THEN rs.Route_Id END ASC,    
			 CASE WHEN @Sort = 'Route_Id' AND @Ascending = 0 THEN rs.Route_Id  END DESC, 
			 CASE WHEN @Sort = 'ShipmentId' AND @Ascending = 1 THEN rs.ShipmentId END ASC,    
			 CASE WHEN @Sort = 'ShipmentId' AND @Ascending = 0 THEN rs.ShipmentId  END DESC, 
			 CASE WHEN @Sort = 'ReceivingType' AND @Ascending = 1 THEN rs.ReceivingTypeName END ASC,    
			 CASE WHEN @Sort = 'ReceivingType' AND @Ascending = 0 THEN rs.ReceivingTypeName  END DESC, 
			 CASE WHEN @Sort = 'FreightCompany' AND @Ascending = 1 THEN c.Name END ASC,   
			 CASE WHEN @Sort = 'FreightCompany' AND @Ascending = 0 THEN c.Name  END DESC,                 			
		     CASE WHEN @Sort = 'ClientName' AND @Ascending = 1 THEN rs.Client END ASC,     
			 CASE WHEN @Sort = 'ClientName' AND @Ascending = 0 THEN rs.Client  END DESC,    
			 CASE WHEN @Sort = 'City' AND @Ascending = 1 THEN rs.CollectionPointCity END ASC,     
			 CASE WHEN @Sort = 'City' AND @Ascending = 0 THEN rs.CollectionPointCity  END DESC,  
		     CASE WHEN @Sort = 'ERISite' AND @Ascending = 1 THEN rs.Site END ASC,  
			 CASE WHEN @Sort = 'ERISite' AND @Ascending = 0 THEN rs.Site  END DESC,
             CASE WHEN @Sort = 'Quote' AND @Ascending = 1 THEN 
             CASE WHEN rs.IsInterCompany = 1 OR rs.IsTransfer = 1 THEN  CASE WHEN rs.IsArrangedByCustomer = 1 THEN  CASE  WHEN rs.IsInbound = 1 THEN '0' ELSE r.Quote END ELSE r.Quote END ELSE r.Quote  END  ELSE 0 END ASC,
             CASE WHEN @Sort = 'Quote' AND @Ascending = 0 THEN 
			 CASE WHEN rs.IsInterCompany = 1 OR rs.IsTransfer = 1 THEN  CASE   WHEN rs.IsArrangedByCustomer = 1 THEN CASE  WHEN rs.IsInbound = 1 THEN '0' ELSE r.Quote  END ELSE r.Quote  END ELSE r.Quote  END  ELSE 0 END DESC,
             CASE WHEN @Sort = 'PickUpDate' AND @Ascending = 1 THEN r.pickupDate END ASC, 
             CASE WHEN @Sort = 'PickUpDate' AND @Ascending = 0 THEN r.pickupDate  END DESC,
             CASE WHEN @Sort = 'DockDate' AND @Ascending = 1 THEN r.DockDate END ASC,
             CASE WHEN @Sort = 'DockDate' AND @Ascending = 0 THEN r.DockDate  END DESC,
             CASE WHEN @Sort = 'ConfirmDate' AND @Ascending = 1 THEN rs.ApprovalDate END ASC,
             CASE WHEN @Sort = 'ConfirmDate' AND @Ascending = 0 THEN rs.ApprovalDate END DESC,
	         CASE WHEN @Sort = 'IsInbound' AND @Ascending = 1 THEN rs.IsInbound END ASC,
             CASE WHEN @Sort = 'IsInbound' AND @Ascending = 0 THEN rs.IsInbound END DESC,
             CASE WHEN @Sort = 'FreightCharges' AND @Ascending = 1 THEN (CASE WHEN rs.IsInterCompany = 1 OR rs.IsTransfer = 1 THEN 
			 CASE WHEN rs.IsInbound = 1 THEN 0 ELSE CASE WHEN EXISTS (SELECT 1 FROM FreightInvoices fi WHERE fi.Route_Id = r.Id AND fi.IsDeleted = 0) THEN 
				(SELECT SUM(fi.GrandTotal) FROM FreightInvoices fi WHERE fi.Route_Id = r.Id AND fi.IsDeleted = 0) * (rs.FreightCostResponsibilityPercentage / 100)  
				 ELSE 0 END END ELSE COALESCE((SELECT SUM(fi.GrandTotal) FROM FreightInvoices fi WHERE fi.Route_Id = r.Id AND fi.IsDeleted = 0) / NULLIF((SELECT COUNT(*) FROM RouteShipments WHERE Route_Id = r.Id), 0), 0) END) END ASC,
             CASE WHEN @Sort = 'FreightCharges' AND @Ascending = 0 THEN (CASE WHEN rs.IsInterCompany = 1 OR rs.IsTransfer = 1 THEN
			   CASE WHEN rs.IsInbound = 1 THEN 0 ELSE CASE WHEN EXISTS (SELECT 1 FROM FreightInvoices fi WHERE fi.Route_Id = r.Id AND fi.IsDeleted = 0) THEN 
				(SELECT SUM(fi.GrandTotal) FROM FreightInvoices fi WHERE fi.Route_Id = r.Id AND fi.IsDeleted = 0) * (rs.FreightCostResponsibilityPercentage / 100)
				  ELSE 0 END END ELSE COALESCE((SELECT SUM(fi.GrandTotal) FROM FreightInvoices fi WHERE fi.Route_Id = r.Id AND fi.IsDeleted = 0) / NULLIF((SELECT COUNT(*) FROM RouteShipments WHERE Route_Id = r.Id), 0), 0) END) END DESC,
			 CASE WHEN @Ascending = 1 THEN 'ASC' ELSE 'DESC' END
            OFFSET (@Page - 1) * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY
END

");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApprovedInvoiceDetails",
                columns: table => new
                {
                    Route_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArriveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityToolTip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientNameToolTip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollectionPoint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollectionPointToolTip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreightInvoiceId = table.Column<long>(type: "bigint", nullable: false),
                    GrandTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InvoiceDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceDateToolTip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceNumberToolTip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quote = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShipmentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShipmentIdToolTip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteNameToolTip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateToolTip = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovedInvoiceDetails", x => x.Route_Id);
                });
        }
    }
}
