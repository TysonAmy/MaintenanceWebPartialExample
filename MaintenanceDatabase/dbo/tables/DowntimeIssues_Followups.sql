CREATE TABLE [dbo].[DowntimeIssues_Followups]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [DowntimeIssueId] INT NOT NULL, 
    [FollowUpRequest] DATETIME NOT NULL, 
    [FollowingUpReason] NVARCHAR(300) NOT NULL, 
    [SupervisorComments] NVARCHAR(300) NULL, 
    [EmployeeId] NVARCHAR(450) NULL, 
    [SupervisorFollowUp] DATETIME NULL, 
    CONSTRAINT [FK_DowntimeIssues_Followups_DowntimeIssueId] FOREIGN KEY (DowntimeIssueId) REFERENCES DowntimeIssues(Id) 
)

GO

CREATE INDEX [IX_DowntimeIssues_Followups_EmployeeId] ON [dbo].[DowntimeIssues_Followups] ([EmployeeId])
