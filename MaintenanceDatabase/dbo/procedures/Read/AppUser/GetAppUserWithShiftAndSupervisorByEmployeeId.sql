CREATE PROCEDURE [dbo].[GetAppUserWithShiftAndSupervisorByEmployeeId]
	@EmployeeId varchar(450)
AS
	SELECT 
		AppUser.Id, 
		AppUser.FirstName, 
		AppUser.LastName, 
		AppUser.Email,
		AppUser.ShiftId, 
		AppUser.SupervisorId, 
		AppUser.PhoneNumber,
		AppUser.IsEnabled,
		'' AS SID,
		Shifts.Id,
		Shifts.Initials, 
		Shifts.Name, 
		'' AS SUID,
		Supervisors.Id,
		Supervisors.FirstName,
		Supervisors.LastName,
		Supervisors.Email,
		Supervisors.PhoneNumber
	FROM AspNetUsers AS AppUser
	LEFT JOIN Shifts
		ON AppUser.ShiftId = Shifts.Id
	LEFT JOIN AspNetUsers as Supervisors
		ON AppUser.SupervisorId = Supervisors.id
	WHERE AppUser.id = @EmployeeId

RETURN 0

