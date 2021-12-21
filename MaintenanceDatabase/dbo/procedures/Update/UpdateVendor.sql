CREATE PROCEDURE [dbo].[UpdateVendor]
	@Id int,
	@Name nvarchar(75)
AS
UPDATE [dbo].[Vendor]
   SET [Name] = @Name
 WHERE Id = @Id

RETURN 0
