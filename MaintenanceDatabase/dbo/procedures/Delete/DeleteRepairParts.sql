CREATE PROCEDURE [dbo].[DeleteRepairParts]
	@Id int
AS
DELETE FROM [dbo].[RepairParts]
      WHERE Id = @Id
RETURN 0
