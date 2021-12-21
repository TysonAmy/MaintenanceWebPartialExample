CREATE PROCEDURE [dbo].[UpdateArea]
	@Id int,
	@Name nvarchar(50)
	,@Deactivate datetime2(7)
AS
UPDATE [dbo].[Areas]
   SET [Name] = @Name
   	  ,[Deactivate] = @Deactivate
 WHERE Id = @Id

RETURN 0
