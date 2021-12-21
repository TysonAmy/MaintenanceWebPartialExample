CREATE PROCEDURE [dbo].[UpdateEquipment]
	@Id int
	,@Name nvarchar(50)
    ,@AreaId int
	,@Deactivate datetime
AS
UPDATE [dbo].[Equipment]
   SET [Name] = @Name
      ,[AreaId] = @AreaId
	  ,[Deactivate] = @Deactivate
 WHERE Id = @Id
RETURN 0
