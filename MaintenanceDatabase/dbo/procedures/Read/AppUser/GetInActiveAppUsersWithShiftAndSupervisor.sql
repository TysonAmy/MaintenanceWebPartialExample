CREATE PROCEDURE [dbo].[GetInActiveAppUsersWithShiftAndSupervisor]
AS
	SELECT 
		AppUsers.Id, 
		AppUsers.FirstName, 
		AppUsers.LastName, 
		AppUsers.Email,
		AppUsers.ShiftId, 
		AppUsers.SupervisorId, 
		AppUsers.UserName,
		AppUsers.PhoneNumber,
		'' AS SID,
		Shifts.Id,
		Shifts.Initials, 
		Shifts.Name, 
		'' AS SUID,
		Supervisors.Id,
		Supervisors.FirstName,
		Supervisors.LastName,
		Supervisors.Email,
		Supervisors.UserName,
		Supervisors.PhoneNumber
	FROM AspNetUsers as AppUsers
	LEFT JOIN Shifts
		ON AppUsers.ShiftId = Shifts.Id
	LEFT JOIN AspNetUsers as Supervisors
		ON AppUsers.SupervisorId = Supervisors.id 
	WHERE AppUsers.IsEnabled = 0 OR AppUsers.IsEnabled is null

RETURN 0

