CREATE PROCEDURE [dbo].[GetAppUsersWithSupervisorsNotesOpenClosed]
AS
SELECT Id, FirstName, LastName, SUM([Open]) AS [Open], Sum([Closed]) AS [Closed]
FROM
(
SELECT AspNetUsers.Id as Id, AspNetUsers.FirstName as FirstName, AspNetUsers.LastName as LastName, 0 AS [Open], 0 AS [Closed]
FROM AspNetUsers
LEFT JOIN AspNetUserRoles
	ON AspNetUserRoles.UserId = AspNetUsers.Id
LEFT JOIN AspNetRoles
	ON AspNetRoles.Id = AspNetUserRoles.RoleId
WHERE NormalizedName like 'Supervisor' or NormalizedName like 'Admin'
UNION
SELECT AspNetUsers.Id as Id, AspNetUsers.FirstName as FirstName, AspNetUsers.LastName as LastName, count(SupervisorsNotes_FollowUps.Id) AS [Open], 0 AS [Closed]
FROM SupervisorsNotes
LEFT JOIN SupervisorsNotes_FollowUps
	ON SupervisorsNotes.Id = SupervisorsNotes_FollowUps.SupervisorsNoteId
LEFT JOIN AspNetUsers
	ON SupervisorsNotes_FollowUps.EmployeeId = AspNetUsers.id
LEFT JOIN AspNetUserRoles
	ON AspNetUserRoles.UserId = AspNetUsers.Id
LEFT JOIN AspNetRoles
	ON AspNetRoles.Id = AspNetUserRoles.RoleId
WHERE 
 SupervisorsNotes.Completed is null
GROUP BY AspNetUsers.Id, AspNetUsers.FirstName, AspNetUsers.LastName
UNION

SELECT AspNetUsers.Id AS Id, AspNetUsers.FirstName as FirstName, AspNetUsers.LastName as LastName, 0 AS [Open], count(SupervisorsNotes_FollowUps.Id) AS [Closed]
  FROM [dbo].[SupervisorsNotes]
  LEFT JOIN [SupervisorsNotes_FollowUps]
    ON [SupervisorsNotes_FollowUps].SupervisorsNoteId = [SupervisorsNotes].Id
  LEFT JOIN [AspNetUsers]
    ON [AspNetUsers].Id = [SupervisorsNotes_FollowUps].EmployeeId
LEFT JOIN AspNetUserRoles
	ON AspNetUserRoles.UserId = AspNetUsers.Id
LEFT JOIN AspNetRoles
	ON AspNetRoles.Id = AspNetUserRoles.RoleId
WHERE SupervisorsNotes.Completed is not null
GROUP BY AspNetUsers.Id, AspNetUsers.FirstName, AspNetUsers.LastName
) as a
WHERE Id is not null
GROUP BY Id, FirstName, LastName
ORDER BY LastName, FirstName


RETURN 0
