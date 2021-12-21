CREATE PROCEDURE [dbo].[GetPreviousLogin]
	@UserId nvarchar(450)
AS
	select LoginTime FROM
	(SELECT TOP 2 ROW_NUMBER() OVER(ORDER BY LoginTime DESC) as row, LoginTime
	FROM Logins
	WHERE UserId = @UserId 
	ORDER BY LoginTime DESC) as a
	WHERE row = 2
	
RETURN 0
