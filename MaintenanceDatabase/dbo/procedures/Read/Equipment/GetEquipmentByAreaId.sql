CREATE PROCEDURE [dbo].[GetEquipmentByAreaId]
	@AreaId  int
AS
	SELECT 
		[Equipment].Id,
		[Equipment].Name,
		[Equipment].AreaId,
		[Equipment].Deactivate,
		'' AS [Areas],
		[Areas].Id,
		[Areas].Name
	FROM [Equipment]
	LEFT JOIN [Areas]
		ON [Areas].Id = [Equipment].AreaId
	WHERE [Equipment].AreaId = @AreaId
	AND [Equipment].Deactivate is null
RETURN 0