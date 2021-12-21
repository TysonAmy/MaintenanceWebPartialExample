CREATE PROCEDURE [dbo].[InsertVendor]
	@Name nvarchar(75)
AS
    INSERT INTO [dbo].[Vendor]
               ([Name])
         VALUES
               (@Name)
RETURN 0
