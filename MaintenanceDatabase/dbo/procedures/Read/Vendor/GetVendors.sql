CREATE PROCEDURE [dbo].[GetVendors]
AS
	SELECT 
		Vendor.Id, 
		Vendor.Name
	FROM Vendor

RETURN 0
