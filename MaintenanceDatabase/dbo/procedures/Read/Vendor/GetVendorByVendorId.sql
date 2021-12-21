CREATE PROCEDURE [dbo].[GetVendorByVendorId] 
	@VendorId int
AS
	SELECT 
		Vendor.Id, 
		Vendor.Name

	FROM Vendor
	WHERE Vendor.Id = @VendorId
RETURN 0
