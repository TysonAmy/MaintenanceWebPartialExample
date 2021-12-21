CREATE PROCEDURE [dbo].GetCurrentFollowNumberOfActiveFollowupsByEmployeesId
	@EmployeeId nvarchar(450)
AS
SELECT count(id)
FROM
(
	SELECT DowntimeIssues_Followups.id as id
	FROM DowntimeIssues_Followups
	LEFT JOIN DowntimeIssues
	ON DowntimeIssues.Id = DowntimeIssues_Followups.DowntimeIssueId
	WHERE DowntimeIssues_Followups.EmployeeId = @EmployeeId
	AND DowntimeIssues.Completed is null
	UNION
	SELECT SupervisorsNotes_FollowUps.id as id
	FROM SupervisorsNotes_FollowUps
	LEFT JOIN SupervisorsNotes
	ON SupervisorsNotes_FollowUps.SupervisorsNoteId = SupervisorsNotes.Id
	WHERE SupervisorsNotes.Completed is null
	AND SupervisorsNotes_FollowUps.EmployeeId = @EmployeeId
) AS Merged_FollowUps
RETURN 0
