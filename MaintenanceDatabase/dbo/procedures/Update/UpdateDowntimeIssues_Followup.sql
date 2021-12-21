CREATE PROCEDURE [dbo].[UpdateDowntimeIssues_Followup]
	@Id int
    ,@DowntimeIssueId int
    ,@FollowUpRequest datetime
    ,@FollowingUpReason nvarchar(300)
    ,@SupervisorComments nvarchar(300)
    ,@EmployeeId nvarchar(450)
    ,@SupervisorFollowUp datetime

AS
    UPDATE [dbo].[DowntimeIssues_Followups]
       SET [DowntimeIssueId] = @DowntimeIssueId
          ,[FollowUpRequest] = @FollowUpRequest
          ,[FollowingUpReason] = @FollowingUpReason
          ,[SupervisorComments] = @SupervisorComments
          ,[EmployeeId] = @EmployeeId 
          ,[SupervisorFollowUp] = @SupervisorFollowUp
     WHERE Id = @Id
RETURN 0
