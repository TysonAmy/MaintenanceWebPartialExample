CREATE PROCEDURE [dbo].[DeleteArea]
	@Id int
AS
DELETE FROM [dbo].[Areas]
      WHERE Id = @Id
RETURN 0
