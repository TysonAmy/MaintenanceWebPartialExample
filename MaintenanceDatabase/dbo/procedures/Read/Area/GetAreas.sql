CREATE PROCEDURE [dbo].[GetAreas]
AS
	SELECT 
		Areas.Id, 
		Areas.Name,
		
		Equipment.Id,
		Equipment.Name
	FROM Areas
	LEFT JOIN Equipment	
		ON Areas.Id = Equipment.AreaId
	and Equipment.Deactivate is null

RETURN 0
