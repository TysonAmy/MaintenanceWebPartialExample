CREATE PROCEDURE [dbo].[GetAreaByAreaId] 
	@AreaId int
AS
	SELECT 
		Areas.Id, 
		Areas.Name,
		
		Equipment.Id,
		Equipment.Name

	FROM Areas
	LEFT JOIN Equipment	
		ON areas.Id = Equipment.AreaId
	WHERE Areas.Id = @AreaId
	and Equipment.Deactivate is null
RETURN 0
