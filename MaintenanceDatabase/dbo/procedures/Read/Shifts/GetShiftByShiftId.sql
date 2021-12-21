CREATE PROCEDURE [dbo].[GetShiftByShiftId]
@ShiftId int
AS
	SELECT Shifts.Id, Shifts.Initials, Shifts.Name 
	FROM Shifts
	WHERE Shifts.Id = @ShiftId
RETURN 0
