CREATE PROCEDURE [dbo].[GetCurrentFollowNumberOfActiveFollowupsByEquipmentId]
	@EquipmentId int
AS
SELECT count(id)
FROM
(
	SELECT DowntimeIssues.id as id
	FROM DowntimeIssues_Followups
	LEFT JOIN DowntimeIssues
		ON DowntimeIssues_Followups.DowntimeIssueId = DowntimeIssues.id
	WHERE DowntimeIssues.EquipmentId = @EquipmentId
	AND DowntimeIssues.Completed is not null
	UNION
	SELECT SupervisorsNotes.Id as id
	FROM SupervisorsNotes
	WHERE SupervisorsNotes.Completed is null
	AND SupervisorsNotes.EquipmentId = @EquipmentId
) AS Merged_FollowUps
RETURN 0
