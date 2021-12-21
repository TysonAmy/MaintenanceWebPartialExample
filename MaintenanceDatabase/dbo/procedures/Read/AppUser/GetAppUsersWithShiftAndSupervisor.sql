CREATE PROCEDURE [dbo].[GetAppUsersWithShiftAndSupervisor]
AS
	SELECT 
		AppUser.Id, 
		AppUser.FirstName, 
		AppUser.LastName, 
		AppUser.Email,
		AppUser.ShiftId, 
		AppUser.SupervisorId, 
		AppUser.UserName,
		AppUser.PhoneNumber,
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
	FROM AspNetUsers as AppUser
	LEFT JOIN Shifts
		ON AppUser.ShiftId = Shifts.Id
	LEFT JOIN AspNetUsers as Supervisors
		ON AppUser.SupervisorId = Supervisors.id 
	WHERE AppUser.IsEnabled = 1 or AppUser.IsEnabled is null

RETURN 0

