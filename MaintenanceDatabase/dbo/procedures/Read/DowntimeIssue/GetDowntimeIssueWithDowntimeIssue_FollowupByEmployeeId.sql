CREATE PROCEDURE [dbo].[GetDowntimeIssueWithDowntimeIssue_FollowupByEmployeeId]
@EmployeeID nvarchar(450)
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
    WHERE DowntimeIssues.Id IN
        ( 
            SELECT DowntimeIssues_Followups.DowntimeIssueId
             FROM DowntimeIssues_Followups
             WHERE DowntimeIssues_Followups.SupervisorFollowUp is null
             AND DowntimeIssues_Followups.EmployeeId = @EmployeeID
        ) -- Show downtime issue with follow ups if current followup is assocated with employee.
    ORDER BY [DowntimeIssues_Followups].Id DESC

RETURN 0
