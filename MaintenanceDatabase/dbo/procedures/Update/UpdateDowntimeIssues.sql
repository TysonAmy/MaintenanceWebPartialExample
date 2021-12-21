CREATE PROCEDURE [dbo].[UpdateDowntimeIssues]
	@Id int
    ,@EmployeeId nvarchar(450)
    ,@EquipmentId int
    ,@IssueResolution nvarchar(500)
    ,@DownTime int
    ,@Complete datetime
AS
UPDATE [dbo].[DowntimeIssues]
   SET [EmployeeId] = @EmployeeId
      ,[EquipmentId] = @EquipmentId
      ,[IssueResolution] = @IssueResolution
      ,[DownTime] = @DownTime
      ,[Completed] = @Complete
 WHERE Id = @Id
RETURN 0
