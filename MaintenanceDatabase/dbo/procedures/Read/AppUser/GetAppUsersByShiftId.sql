CREATE PROCEDURE [dbo].[GetAppUsersByShiftId]
	@ShiftId int
AS
	SELECT 
		AppUsers.Id, 
		AppUsers.FirstName, 
		AppUsers.LastName, 
		AppUsers.Email,
		AppUsers.ShiftId, 
		AppUsers.SupervisorId, 
		AppUsers.UserName,
		AppUsers.PhoneNumber
	FROM AspNetUsers as AppUsers
	LEFT JOIN Shifts
		ON AppUsers.ShiftId = Shifts.Id
	WHERE AppUsers.IsEnabled = 1 AND Shifts.id = @ShiftId
RETURN 0

