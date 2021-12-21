CREATE TABLE [dbo].[Logins]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] NVARCHAR(450) NOT NULL, 
    [LoginTime] DATETIME NOT NULL 

)

GO

CREATE INDEX [IX_Logins_LoginTime] ON [dbo].[Logins] ([LoginTime])

GO

CREATE INDEX [IX_Logins_UserId] ON [dbo].[Logins] ([UserId])
