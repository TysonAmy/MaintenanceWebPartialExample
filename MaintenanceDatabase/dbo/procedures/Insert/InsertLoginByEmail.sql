CREATE PROCEDURE [dbo].[InsertLoginByEmail]
	@Email varchar(256)
AS
	INSERT INTO Logins (UserId, LoginTime)
	SELECT Id, CURRENT_TIMESTAMP
	FROM AspNetUsers
	WHERE NormalizedEmail = UPPER(@Email)
RETURN 0
