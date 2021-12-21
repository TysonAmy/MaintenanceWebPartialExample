CREATE PROCEDURE [dbo].[GetShifts]
AS
	SELECT Shifts.Id, Shifts.Initials, Shifts.Name FROM Shifts
RETURN 0
