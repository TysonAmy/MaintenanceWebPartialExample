CREATE PROCEDURE [dbo].[GetAppUsers]
AS
	SELECT 
		AppUser.Id, 
		AppUser.FirstName, 
		AppUser.LastName, 
		AppUser.Email,
		AppUser.ShiftId, 
		AppUser.SupervisorId, 
		AppUser.UserName,
		AppUser.PhoneNumber
	FROM AspNetUsers as AppUser
	WHERE AppUser.IsEnabled = 1 or AppUser.IsEnabled is null

RETURN 0

