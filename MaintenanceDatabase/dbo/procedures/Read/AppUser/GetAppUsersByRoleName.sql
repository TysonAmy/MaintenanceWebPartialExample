CREATE PROCEDURE [dbo].[GetAppUsersByRoleName]
	@RoleName1 nvarchar(256),
	@RoleName2 nvarchar(256),
	@RoleName3 nvarchar(256)
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
	LEFT JOIN AspNetUserRoles
		ON AspNetUserRoles.UserId = AppUser.Id
	LEFT JOIN AspNetRoles
		ON AspNetUserRoles.RoleId = AspNetRoles.Id
	WHERE AppUser.IsEnabled = 1 or AppUser.IsEnabled is null
	AND (UPPER(@RoleName1) = AspNetRoles.NormalizedName
	OR UPPER(@RoleName2) = AspNetRoles.NormalizedName
	OR UPPER(@RoleName3) = AspNetRoles.NormalizedName)
RETURN 0

