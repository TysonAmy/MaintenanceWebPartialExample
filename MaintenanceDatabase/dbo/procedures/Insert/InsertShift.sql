CREATE PROCEDURE [dbo].[InsertShift]
    @Initials nvarchar(5)
    ,@Name nvarchar(50)
AS
INSERT INTO [dbo].[Shifts]
           ([Initials]
           ,[Name])
     VALUES
           (@Initials
           ,@Name)
RETURN 0
