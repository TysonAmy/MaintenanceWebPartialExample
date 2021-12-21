CREATE PROCEDURE [dbo].[InsertDowntimeIssue]
	@EmployeeId nvarchar(450),
    @EquipmentId int,
    @IssueResolution nvarchar(500),
    @DownTime int
AS
INSERT INTO [dbo].[DowntimeIssues]
           ([Created]
           ,[EmployeeId]
           ,[EquipmentId]
           ,[IssueResolution]
           ,[DownTime])
     VALUES
           (getdate()
           ,@EmployeeId
           ,@EquipmentId
           ,@IssueResolution
           ,@DownTime)
SELECT SCOPE_IDENTITY() as LastIdentityValue;

RETURN 0
