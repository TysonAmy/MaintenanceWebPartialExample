CREATE PROCEDURE [dbo].[GetAppUsersWithShiftAndSupervisorBySupervisorId]
	@SupervisorId nvarchar(450)
AS
	SELECT 
		AppUser.Id, 
		AppUser.FirstName, 
		AppUser.LastName, 
		AppUser.Email,
		AppUser.ShiftId, 
		AppUser.SupervisorId,
		AppUser.PhoneNumber,
		AppUser.UserName,
		'' AS SID,
		Shifts.Id,
		Shifts.Initials, 
		Shifts.Name, 
		'' AS SUID,
		Supervisors.Id,
		Supervisors.FirstName,
		Supervisors.LastName,
		Supervisors.Email,
		Supervisors.PhoneNumber,
		Supervisors.UserName
	FROM AspNetUsers as AppUser
	LEFT JOIN Shifts
		ON AppUser.ShiftId = Shifts.Id
	LEFT JOIN AspNetUsers as Supervisors
		ON AppUser.SupervisorId = Supervisors.id
	WHERE AppUser.SupervisorId = @SupervisorId	AND AppUser.IsEnabled = 1	

RETURN 0

