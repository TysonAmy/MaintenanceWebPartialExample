CREATE PROCEDURE [dbo].[InsertArea]
	@Name nvarchar(50)
AS
    INSERT INTO [dbo].[Areas]
               ([Name])
         VALUES
               (@Name)

RETURN 0