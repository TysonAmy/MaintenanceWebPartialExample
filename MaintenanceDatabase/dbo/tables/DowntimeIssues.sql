CREATE TABLE [dbo].[DowntimeIssues]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Created] DATETIME NOT NULL, 
    [EmployeeId] NVARCHAR(450) NOT NULL, 
    [EquipmentId] INT NOT NULL, 
    [IssueResolution] NVARCHAR(500) NOT NULL, 
    [DownTime] INT NOT NULL, 
    [Completed] DATETIME NULL, 
    CONSTRAINT [FK_DowntimeIssues_Equipment] FOREIGN KEY (EquipmentId) REFERENCES Equipment(Id) 
)

GO

CREATE INDEX [IX_DowntimeIssues_Created] ON [dbo].[DowntimeIssues] ([Created])

GO

CREATE INDEX [IX_DowntimeIssues_Created_EquipmentId] ON [dbo].[DowntimeIssues] ([Created], [EmployeeId])

GO

CREATE INDEX [IX_DowntimeIssues_EquipmentId] ON [dbo].[DowntimeIssues] ([EquipmentId])
