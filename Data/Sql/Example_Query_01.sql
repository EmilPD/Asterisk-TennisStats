SELECT CONVERT(VARCHAR(11), m.DatePlayed, 106) AS [Match Date], 
	p.FirstName + ' ' + p.LastName AS [Winner],
	pp.FirstName + ' ' + pp.LastName AS [Loser],
	m.Result,
	po.Points AS [Points Won],
	t.Name AS [Tournament],
    c.Name AS [City],
	co.Name AS [Country] 
FROM Matches m
JOIN Tournaments t ON t.Id = m.TournamentId
JOIN PointDistributionKeys po ON po.CategoryId = t.CategoryId AND po.RoundId = (m.RoundId + 1)
JOIN Cities c ON c.Id = t.CityId
JOIN Players p ON m.WinnerId = p.Id
JOIN Players pp ON m.LoserId = pp.Id
JOIN Countries co ON c.CountryId = co.Id