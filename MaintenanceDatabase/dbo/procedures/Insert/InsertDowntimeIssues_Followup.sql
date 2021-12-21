CREATE PROCEDURE [dbo].[InsertDowntimeIssues_Followup]
            @DowntimeIssueId int
           ,@FollowUpRequest datetime
           ,@FollowingUpReason nvarchar(300)
           ,@SupervisorComments nvarchar(300)
           ,@EmployeeId nvarchar(450)
           ,@SupervisorFollowUp datetime
AS
INSERT INTO [dbo].[DowntimeIssues_Followups]
           ([DowntimeIssueId]
           ,[FollowUpRequest]
           ,[FollowingUpReason]
           ,[SupervisorComments]
           ,[EmployeeId]
           ,[SupervisorFollowUp])
     VALUES
           (@DowntimeIssueId
           ,@FollowUpRequest
           ,@FollowingUpReason
           ,@SupervisorComments
           ,@EmployeeId
           ,@SupervisorFollowUp)
RETURN 0
