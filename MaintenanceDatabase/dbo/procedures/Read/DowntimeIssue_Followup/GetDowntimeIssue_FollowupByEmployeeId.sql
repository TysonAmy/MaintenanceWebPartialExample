CREATE PROCEDURE [dbo].[GetDowntimeIssue_FollowupByEmployeeId]
	@EmployeeId nvarchar(450)
AS
	SELECT 
       [DowntimeIssues_Followups].[Id]
      ,[DowntimeIssues_Followups].DowntimeIssueId
      ,[DowntimeIssues_Followups].[EmployeeId]
      ,[DowntimeIssues_Followups].FollowingUpReason
      ,[DowntimeIssues_Followups].FollowUpRequest
      ,[DowntimeIssues_Followups].SupervisorComments
      ,[DowntimeIssues_Followups].SupervisorFollowUp
      ,'' AS [DowntimeIssues]
      ,[DowntimeIssues].[Id]
      ,[DowntimeIssues].Created
      ,[DowntimeIssues].EmployeeId
      ,[DowntimeIssues].EquipmentId
      ,[DowntimeIssues].IssueResolution
      ,[DowntimeIssues].DownTime
      ,'' AS [SubmittingSupervisor]
      ,[SubmittingSupervisor].Id
      ,[SubmittingSupervisor].FirstName
      ,[SubmittingSupervisor].LastName
      ,[SubmittingSupervisor].Email
      ,'' AS [FollowingUpSupervisor]
      ,[FollowingUpSupervisor].Id
      ,[FollowingUpSupervisor].FirstName
      ,[FollowingUpSupervisor].LastName
      ,[FollowingUpSupervisor].Email
      ,'' AS [Equipment]
      ,[Equipment].id
      ,[Equipment].Name
      ,[Equipment].AreaId
      ,'' AS [Area]
      ,[Areas].[Id]
      ,[Areas].Name
      

    FROM [DowntimeIssues_Followups]
    LEFT JOIN [DowntimeIssues]
        ON [DowntimeIssues_Followups].DowntimeIssueId = [DowntimeIssues].[Id]
    LEFT JOIN [AspNetUsers] AS [SubmittingSupervisor]
        ON [SubmittingSupervisor].Id = [DowntimeIssues].EmployeeId
    LEFT JOIN [AspNetUsers] AS [FollowingUpSupervisor]
        on [FollowingUpSupervisor].Id = [DowntimeIssues_Followups].EmployeeId
    LEFT JOIN [Equipment]
        ON [Equipment].Id = DowntimeIssues.EquipmentId
    LEFT JOIN [Areas]
        ON  [Areas].Id = Equipment.AreaId
    WHERE [FollowingUpSupervisor].Id = @EmployeeId
RETURN 0
