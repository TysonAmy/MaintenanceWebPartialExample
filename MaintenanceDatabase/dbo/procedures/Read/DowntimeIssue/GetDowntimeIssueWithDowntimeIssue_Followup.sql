CREATE PROCEDURE [dbo].[GetDowntimeIssueWithDowntimeIssue_Followup]
AS
	SELECT 
	   [DowntimeIssues].[Id]
      ,[DowntimeIssues].[Created]
      ,[DowntimeIssues].[EmployeeId]
      ,[DowntimeIssues].[EquipmentId]
      ,[DowntimeIssues].[IssueResolution]
      ,[DowntimeIssues].[DownTime]
      ,'' AS [DowntimeIssues_Followups],
      [DowntimeIssues_Followups].Id,
      [DowntimeIssues_Followups].EmployeeId,
      [DowntimeIssues_Followups].FollowingUpReason,
      [DowntimeIssues_Followups].FollowUpRequest,
      [DowntimeIssues_Followups].SupervisorComments,
      [DowntimeIssues_Followups].SupervisorFollowUp,
      '' AS [RepairPerson],
      [RepairPerson].Id,
      [RepairPerson].ShiftId,
      [RepairPerson].FirstName,
      [RepairPerson].LastName,
      [RepairPerson].SupervisorId,
      '' AS [Equipment],
      [Equipment].Id,
      [Equipment].Name,
      [Equipment].AreaId,
      '' AS [Areas],
      [Areas].Id,
      [Areas].Name,
      '' AS [FollowingUpEmployee],
      [FollowingUpEmployee].Id,
      [FollowingUpEmployee].FirstName,
      [FollowingUpEmployee].LastName,
      [FollowingUpEmployee].Email,
      [FollowingUpEmployee].ShiftId,
      [FollowingUpEmployee].SupervisorId



    FROM [DowntimeIssues]
    LEFT JOIN [DowntimeIssues_Followups]
        ON [DowntimeIssues_Followups].DowntimeIssueId = [DowntimeIssues].Id
    LEFT JOIN [AspNetUsers] AS [RepairPerson]
        on [RepairPerson].Id = [DowntimeIssues].EmployeeId
    LEFT JOIN [Equipment]
        ON [Equipment].id = [DowntimeIssues].[EquipmentId]
    LEFT JOIN [Areas]
        ON [Areas].Id = [Equipment].AreaId
    LEFT JOIN [AspNetUsers] AS [FollowingUpEmployee]
        ON [DowntimeIssues_Followups].EmployeeId = [FollowingUpEmployee].Id
    ORDER BY [DowntimeIssues_Followups].Id DESC
        

RETURN 0
