CREATE PROCEDURE [dbo].[InsertEquipment]
    @Name nvarchar(50)
    ,@AreaId int
AS
INSERT INTO [dbo].[Equipment]
           ([Name]
           ,[AreaId])
     VALUES
           (@Name
           ,@AreaId)
RETURN 0
