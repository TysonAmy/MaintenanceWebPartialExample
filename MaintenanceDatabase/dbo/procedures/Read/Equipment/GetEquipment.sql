CREATE PROCEDURE [dbo].GetEquipment
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
	WHERE [Equipment].Deactivate is null
RETURN 0