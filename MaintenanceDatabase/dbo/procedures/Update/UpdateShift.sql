CREATE PROCEDURE [dbo].[UpdateShift]
	 @Id int
	,@Initials nvarchar(5)
	,@Name nvarchar(50)
AS
UPDATE [dbo].[Shifts]
   SET [Initials] = @Initials
      ,[Name] = @Name
 WHERE Id = @Id
 RETURN 0
