CREATE TABLE [dbo].[Equipment]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [AreaId] INT NOT NULL, 
    [Deactivate] DATETIME NULL, 
    CONSTRAINT [FK_Equipment_Area] FOREIGN KEY (AreaId) REFERENCES [Areas](Id)
)

GO

CREATE INDEX [IX_Equipment_Deactivate] ON [dbo].[Equipment] ([Deactivate])
