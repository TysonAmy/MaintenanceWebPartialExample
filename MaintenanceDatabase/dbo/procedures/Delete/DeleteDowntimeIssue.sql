CREATE PROCEDURE [dbo].[DeleteDowntimeIssue]
	@Id int
AS
DELETE FROM [dbo].[DowntimeIssues]
      WHERE Id = @Id
RETURN 0
