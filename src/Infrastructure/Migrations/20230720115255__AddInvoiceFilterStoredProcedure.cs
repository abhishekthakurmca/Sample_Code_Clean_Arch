using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Anubis.Infrastructure.Migrations
{
    public partial class _AddInvoiceFilterStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApprovedInvoiceDetails",
                columns: table => new
                {
                    Route_Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FreightInvoiceId = table.Column<long>(nullable: false),
                    GrandTotal = table.Column<decimal>(nullable: false),
                    Percentage = table.Column<decimal>(nullable: false),
                    Quote = table.Column<decimal>(nullable: false),
                    Approved = table.Column<bool>(nullable: false),
                    CollectionPoint = table.Column<string>(nullable: true),
                    CollectionPointToolTip = table.Column<string>(nullable: true),
                    SiteName = table.Column<string>(nullable: true),
                    SiteNameToolTip = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    ArriveDate = table.Column<DateTime>(nullable: true),
                    InvoiceNumber = table.Column<string>(nullable: true),
                    InvoiceNumberToolTip = table.Column<string>(nullable: true),
                    InvoiceDate = table.Column<string>(nullable: true),
                    InvoiceDateToolTip = table.Column<string>(nullable: true),
                    ClientName = table.Column<string>(nullable: true),
                    ClientNameToolTip = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    StateToolTip = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    CityToolTip = table.Column<string>(nullable: true),
                    ApprovedDate = table.Column<DateTime>(nullable: false),
                    ShipmentId = table.Column<string>(nullable: true),
                    ShipmentIdToolTip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovedInvoiceDetails", x => x.Route_Id);
                });

            migrationBuilder.Sql(@"
GO

IF EXISTS(SELECT 1 FROM sys.procedures 
          WHERE Name = 'spGetInvoiceList')
BEGIN
    DROP PROCEDURE dbo.spGetInvoiceList
END 


/****** Object:  StoredProcedure [dbo].[spGetInvoiceList]    Script Date: 7/26/2023 2:39:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Sourav>
-- Create date: <Create Date,7/26/2023,>
-- Description:	<Description,For Invoice Filter and Export function,>
-- =============================================
CREATE PROCEDURE [dbo].[spGetInvoiceList]
(
    @Route_Id bigint = NULL,
    @ShipmentId bigint = NULL,
    @CompanyName NVARCHAR(MAX) = NULL,
    @SiteName NVARCHAR(MAX) = NULL,
    @CollectionPoint NVARCHAR(MAX) = NULL,
    @InvoiceNumber NVARCHAR(MAX) = NULL,
    @InvoiceDate DATETIME = NULL,
    @ClientName NVARCHAR(MAX) = NULL,
    @State NVARCHAR(MAX) = NULL,
    @City NVARCHAR(MAX) = NULL,
    @Quote DECIMAL = NULL,
    @ApprovedDate DATETIME = NULL,
    @Percentage DECIMAL = NULL,
    @GrandTotal DECIMAL = NULL,
	@Approved BIT=1,
    @Sort NVARCHAR(20) = NULL,
    @Ascending BIT=1,
    @PageSize INT = 15,
    @Page INT = 1,
	@TotalRecordCount INT OUT
)
AS
BEGIN
    SET NOCOUNT ON;


SELECT @TotalRecordCount = COUNT(DISTINCT FI.Id)
       FROM FreightInvoices FI
       INNER JOIN [Route] R ON FI.Route_Id = R.Id
       INNER JOIN [FreightCompany] C ON R.Company_Id = C.Id
       LEFT JOIN RouteShipments RS ON FI.Route_Id = RS.Route_Id
       LEFT JOIN FreightInvoiceHeaders IH ON FI.Id = IH.FreightInvoice_Id
       WHERE FI.IsApproved = @Approved
       AND FI.IsDeleted != 1
       AND (@Route_Id IS NULL OR CAST(FI.Route_Id AS NVARCHAR(MAX)) LIKE '%' + CAST(@Route_Id AS NVARCHAR(MAX)) + '%')
       AND (@ShipmentId IS NULL OR CAST(RS.ShipmentId AS NVARCHAR(MAX)) LIKE '%' + CAST(@ShipmentId AS NVARCHAR(MAX)) + '%')
       AND (@CompanyName IS NULL OR C.Name LIKE '%' + @CompanyName + '%')
       AND (@SiteName IS NULL OR RS.Site LIKE '%' + @SiteName + '%')
       AND (@CollectionPoint IS NULL OR RS.CollectionPoint LIKE '%' + @CollectionPoint + '%')
       AND (@InvoiceNumber IS NULL OR IH.InvoiceNumber LIKE '%' + @InvoiceNumber + '%')
       AND (@InvoiceDate IS NULL OR CONVERT(NVARCHAR(10), IH.InvoiceDate, 101) LIKE '%' + CONVERT(NVARCHAR(10), @InvoiceDate, 101) + '%')
       AND (@ClientName IS NULL OR RS.Client LIKE '%' + @ClientName + '%')
       AND (@State IS NULL OR RS.CollectionPointState LIKE '%' + @State + '%')
       AND (@City IS NULL OR RS.CollectionPointCity LIKE '%' + @City + '%')
       AND (CONVERT(NVARCHAR(MAX), FI.Quote) LIKE '%' + CONVERT(NVARCHAR(MAX), @Quote) + '%' OR @Quote IS NULL)
       AND (CONVERT(NVARCHAR(MAX), FI.GrandTotal) LIKE '%' + CONVERT(NVARCHAR(MAX), @GrandTotal) + '%' OR @GrandTotal IS NULL)
       AND (CONVERT(NVARCHAR(MAX), FI.Percentage) LIKE '%' + CONVERT(NVARCHAR(MAX), @Percentage) + '%' OR @Percentage IS NULL)
       AND (@ApprovedDate IS NULL OR CONVERT(NVARCHAR(10), FI.ApprovedDate, 101) LIKE '%' + CONVERT(NVARCHAR(10), @ApprovedDate, 101) + '%');

   SELECT
  
       FI.Id AS FreightInvoiceId,
       R.DockDate AS ArriveDate,
       C.Name AS CompanyName,
       CASE
                WHEN RS.Count > 1 THEN CAST(RS.Count AS NVARCHAR(10))
                ELSE RS.CollectionPoint
                END AS CollectionPoint,
        CASE
                 WHEN RS.Count > 1 THEN
                (
                SELECT STRING_AGG(CONCAT(TT.ShipmentId, ' ', TT.CollectionPoint), ', ')
                FROM RouteShipments TT
                WHERE TT.Route_Id = R.Id
                )
                ELSE CONCAT(RS.ShipmentId, ' ', RS.CollectionPoint)
                END AS CollectionPointToolTip,
        CASE
                WHEN RS.Count > 1 THEN CAST(RS.Count AS NVARCHAR(10))
                ELSE RS.Site
                END AS SiteName,
		CASE
                WHEN RS.Count > 1 THEN
                (
                SELECT STRING_AGG(CONCAT(TT.ShipmentId, ' ', TT.Site), ', ')
                FROM RouteShipments TT
                WHERE TT.Route_Id = R.Id
                )
                ELSE CONCAT(RS.ShipmentId, ' ', RS.Site)
                END AS SiteNameToolTip,
        CASE
                WHEN RS.Count > 1 THEN CAST(RS.Count AS NVARCHAR(10))
                ELSE CAST(RS.ShipmentId AS NVARCHAR(10))
                END AS ShipmentId,
	    CASE
               WHEN RS.Count > 1 THEN
               (
                SELECT STRING_AGG(CAST(TT.ShipmentId AS NVARCHAR(10)), ', ')
                FROM RouteShipments TT
                WHERE TT.Route_Id = R.Id
                )
                ELSE CAST(RS.ShipmentId AS NVARCHAR(20))
                END AS ShipmentIdToolTip,
        CASE
                WHEN IH.Count > 1 THEN CAST(IH.Count AS NVARCHAR(10))
                ELSE IH.InvoiceNumber
                END AS InvoiceNumber,
		CASE
                WHEN IH.Count > 1 THEN
                (
                SELECT STRING_AGG(TT.InvoiceNumber, ', ')
                FROM FreightInvoiceHeaders TT
                WHERE TT.FreightInvoice_Id = FI.Id AND TT.IsDeleted != 1
                )
                ELSE IH.InvoiceNumber
               END AS InvoiceNumberToolTip,
        CASE
                WHEN IH.Count > 1 THEN CAST(IH.Count AS NVARCHAR(10))
                ELSE CONVERT(NVARCHAR(10), IH.InvoiceDate, 101)
                END AS InvoiceDate,
		CASE
                WHEN IH.Count > 1 THEN
                (
                SELECT STRING_AGG(CONVERT(NVARCHAR(10), TT.InvoiceDate, 101), ', ')
                FROM FreightInvoiceHeaders TT
                WHERE TT.FreightInvoice_Id = FI.Id AND TT.IsDeleted != 1
                )
                ELSE CONVERT(NVARCHAR(10), IH.InvoiceDate, 101)
                END AS InvoiceDateToolTip,
        CASE
                WHEN RS.Count > 1 THEN CAST(RS.Count AS NVARCHAR(10))
                ELSE RS.Client
                END AS ClientName,
        CASE
                WHEN RS.Count > 1 THEN
                (
                SELECT STRING_AGG(TT.Client, ', ')
                FROM RouteShipments TT
                WHERE TT.Route_Id = R.Id
                )
                ELSE RS.Client
                END AS ClientNameToolTip,
        CASE
                WHEN RS.Count > 1 THEN CAST(RS.Count AS NVARCHAR(10))
                ELSE RS.CollectionPointCity
                END AS City,
        CASE
                WHEN RS.Count > 1 THEN
                (
                SELECT STRING_AGG(TT.CollectionPointCity, ', ')
                FROM RouteShipments TT
                WHERE TT.Route_Id = R.Id
                )
                ELSE RS.CollectionPointCity
                END AS CityToolTip,
        CASE
                WHEN RS.Count > 1 THEN CAST(RS.Count AS NVARCHAR(10))
                ELSE RS.CollectionPointState 
                END AS State,
        CASE
                WHEN RS.Count > 1 THEN
                (
                SELECT STRING_AGG(TT.CollectionPointState, ', ')
                FROM RouteShipments TT
                WHERE TT.Route_Id = R.Id
                )
                ELSE RS.CollectionPointState
                END AS StateToolTip,
        FI.GrandTotal,
        FI.Quote,
        FI.IsApproved AS Approved,
        FI.Percentage,
        FI.Route_Id,
        FI.ApprovedDate
        FROM
            FreightInvoices FI
            INNER JOIN [Route] R ON FI.Route_Id = R.Id
            INNER JOIN [FreightCompany] C ON R.Company_Id = C.Id
            LEFT JOIN (
                SELECT
                    RS.Route_Id,
                    COUNT(*) AS Count,
                    MAX(RS.ShipmentId) AS ShipmentId,
                    MAX(RS.CollectionPoint) AS CollectionPoint,
                    MAX(RS.Site) AS Site,
                    MAX(RS.Client) AS Client,
                    MAX(RS.CollectionPointCity) AS CollectionPointCity,
                    MAX(RS.CollectionPointState) AS CollectionPointState
                FROM
                    RouteShipments RS
                GROUP BY
                    RS.Route_Id) RS ON FI.Route_Id = RS.Route_Id
            LEFT JOIN (
                SELECT
                    FIH.FreightInvoice_Id,
                    COUNT(*) AS Count,
                    MAX(FIH.InvoiceNumber) AS InvoiceNumber,
                    MAX(FIH.InvoiceDate) AS InvoiceDate
                FROM
                    FreightInvoiceHeaders FIH
                WHERE
                    FIH.IsDeleted != 1
                GROUP BY
                    FIH.FreightInvoice_Id) IH ON FI.Id = IH.FreightInvoice_Id
       WHERE
             FI.IsApproved = @Approved
             AND FI.IsDeleted != 1
             AND (@Route_Id IS NULL OR CAST(FI.Route_Id AS NVARCHAR(MAX)) LIKE '%' + CAST(@Route_Id AS NVARCHAR(MAX)) + '%')
             AND (@ShipmentId IS NULL OR CAST(RS.ShipmentId AS NVARCHAR(MAX)) LIKE '%' + CAST(@ShipmentId AS NVARCHAR(MAX)) + '%')
             AND (@CompanyName IS NULL OR C.Name LIKE '%' + @CompanyName + '%')
             AND (@SiteName IS NULL OR RS.Site LIKE '%' + @SiteName + '%')
             AND (@CollectionPoint IS NULL OR RS.CollectionPoint LIKE '%' + @CollectionPoint + '%')
             AND (@InvoiceNumber IS NULL OR IH.InvoiceNumber LIKE '%' + @InvoiceNumber + '%')
             AND (@InvoiceDate IS NULL OR CONVERT(NVARCHAR(10), IH.InvoiceDate, 101) LIKE '%' + CONVERT(NVARCHAR(10), @InvoiceDate, 101) + '%')
             AND (@ClientName IS NULL OR RS.Client LIKE '%' + @ClientName + '%')
             AND (@State IS NULL OR RS.CollectionPointState LIKE '%' + @State + '%')
             AND (@City IS NULL OR RS.CollectionPointCity LIKE '%' + @City + '%')
             AND (CONVERT(NVARCHAR(MAX), FI.Quote) LIKE '%' + CONVERT(NVARCHAR(MAX), @Quote) + '%' OR @Quote IS NULL)
		     AND (CONVERT(NVARCHAR(MAX), FI.GrandTotal) LIKE '%' + CONVERT(NVARCHAR(MAX), @GrandTotal) + '%' OR @GrandTotal IS NULL)
		     AND (CONVERT(NVARCHAR(MAX), FI.Percentage) LIKE '%' + CONVERT(NVARCHAR(MAX), @Percentage) + '%' OR @Percentage IS NULL)
             AND (@ApprovedDate IS NULL OR CONVERT(NVARCHAR(10), FI.ApprovedDate, 101) LIKE '%' + CONVERT(NVARCHAR(10), @ApprovedDate, 101) + '%')
    ORDER BY
             CASE 
                  WHEN @Sort = 'Route_Id' AND @Ascending=1 THEN FI.Route_Id END ASC,
             CASE 
                  WHEN @Sort = 'Route_Id' AND @Ascending=0 THEN FI.Route_Id END DESC,
		     CASE 
                  WHEN @Sort = 'ShipmentId' AND @Ascending = 1 THEN RS.ShipmentId END ASC,
			 CASE
                  WHEN @Sort = 'ShipmentId' AND @Ascending = 0 THEN RS.ShipmentId  END DESC,
			 CASE 
                  WHEN @Sort = 'CompanyName' AND @Ascending = 1 THEN C.Name END ASC,
			 CASE
                  WHEN @Sort = 'CompanyName' AND @Ascending = 0 THEN C.Name  END DESC,
			 CASE 
                  WHEN @Sort = 'InvoiceNumber' AND @Ascending = 1 THEN IH.InvoiceNumber END ASC,
			 CASE
                  WHEN @Sort = 'InvoiceNumber' AND @Ascending = 0 THEN IH.InvoiceNumber  END DESC,
			 CASE 
                  WHEN @Sort = 'InvoiceDate' AND @Ascending = 1 THEN IH.InvoiceDate END ASC,
			 CASE
                  WHEN @Sort = 'InvoiceDate' AND @Ascending = 0 THEN IH.InvoiceDate  END DESC,
		     CASE 
                  WHEN @Sort = 'ClientName' AND @Ascending = 1 THEN RS.Client END ASC,
			 CASE
                  WHEN @Sort = 'ClientName' AND @Ascending = 0 THEN RS.Client  END DESC,
			 CASE 
                  WHEN @Sort = 'State' AND @Ascending = 1 THEN RS.CollectionPointState END ASC,
			 CASE
                  WHEN @Sort = 'State' AND @Ascending = 0 THEN RS.CollectionPointState  END DESC,
			 CASE 
                  WHEN @Sort = 'City' AND @Ascending = 1 THEN RS.CollectionPointCity END ASC,
			 CASE
                  WHEN @Sort = 'City' AND @Ascending = 0 THEN RS.CollectionPointCity  END DESC,
			 CASE 
                  WHEN @Sort = 'CollectionPoint' AND @Ascending = 1 THEN RS.CollectionPoint END ASC,
			 CASE
                  WHEN @Sort = 'CollectionPoint' AND @Ascending = 0 THEN RS.CollectionPoint  END DESC,
			 CASE 
                  WHEN @Sort = 'SiteName' AND @Ascending = 1 THEN RS.Site END ASC,
		  	 CASE
                  WHEN @Sort = 'SiteName' AND @Ascending = 0 THEN RS.Site  END DESC,
			 CASE 
                  WHEN @Sort = 'Quote' AND @Ascending = 1 THEN FI.Quote END ASC,
			 CASE
                  WHEN @Sort = 'Quote' AND @Ascending = 0 THEN FI.Quote  END DESC,
			 CASE 
                  WHEN @Sort = 'GrandTotal' AND @Ascending = 1 THEN FI.GrandTotal END ASC,
			 CASE
                  WHEN @Sort = 'GrandTotal' AND @Ascending = 0 THEN FI.GrandTotal  END DESC,
			 CASE 
                  WHEN @Sort = 'Percentage' AND @Ascending = 1 THEN FI.Percentage END ASC,
			 CASE
                  WHEN @Sort = 'Percentage' AND @Ascending = 0 THEN FI.Percentage  END DESC,
			 CASE 
                  WHEN @Sort = 'ArriveDate' AND @Ascending = 1 THEN R.DockDate END ASC,
			 CASE
                  WHEN @Sort = 'ArriveDate' AND @Ascending = 0 THEN R.DockDate  END DESC,
			 CASE 
                  WHEN @Ascending = 1 THEN  'ASC' ELSE 'DESC'  END
				  
                  OFFSET (@Page - 1) * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY
				
END
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable( name: "ApprovedInvoiceDetails");
            migrationBuilder.Sql(@"DROP PROCEDURE dbo.spGetInvoiceList GO");
        }
    }
}
