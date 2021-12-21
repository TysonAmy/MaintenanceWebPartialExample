CREATE PROCEDURE [dbo].[DeleteShift]
	@Id int
AS
DELETE FROM [dbo].[Shifts]
      WHERE Id = @Id
RETURN 0
