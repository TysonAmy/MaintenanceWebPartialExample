CREATE PROCEDURE [dbo].[DeleteDowntimeIssue_Followup]
	@Id int
AS
DELETE FROM [dbo].[DowntimeIssues_Followups]
      WHERE Id = @Id
RETURN 0
