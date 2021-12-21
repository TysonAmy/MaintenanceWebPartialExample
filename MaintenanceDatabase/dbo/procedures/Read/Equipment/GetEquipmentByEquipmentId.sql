CREATE PROCEDURE [dbo].[GetEquipmentByEquipmentId]
	@EquipmentId  int
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
	WHERE [Equipment].[Id] = @EquipmentId
RETURN 0
