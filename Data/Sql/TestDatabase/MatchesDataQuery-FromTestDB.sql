------------------- SELECT MATCHES DATA ----------------------
SELECT 
	m.Id AS MatchId,
	CONVERT(VARCHAR(11), m.DatePlayed, 106) AS [Match Date], 
	p.FirstName + ' ' + p.LastName AS [Winner],
	pp.FirstName + ' ' + pp.LastName AS [Loser],
	m.Result,
	pow.Points AS [Points Winner],
	pol.Points AS [Points Loser],
	t.Name AS [Tournament],
	t.StartDate AS [StartDate],
	t.EndDate AS [EndDate],
	t.PrizeMoney AS [PrizeMoney],
	cat.Category,
	cat.PlayersNumber,
	tr.Name AS [Round],
    c.Name AS [City],
	co.Name AS [Country],
	ts.Type AS [Surface],
	ts.Speed AS [Speed]
FROM Matches m
JOIN Tournaments t ON t.Id = m.TournamentId
JOIN TournamentCategories cat ON cat.Id = t.CategoryId
JOIN Rounds tr ON tr.Id = m.RoundId
JOIN Surfaces ts ON ts.Id = t.SurfaceId
JOIN PointDistributionKeys pow ON pow.CategoryId = t.CategoryId AND pow.RoundId = (m.RoundId + 1)
JOIN PointDistributionKeys pol ON pol.CategoryId = t.CategoryId AND pol.RoundId = (m.RoundId)
JOIN Cities c ON c.Id = t.CityId
JOIN Players p ON m.WinnerId = p.Id
JOIN Players pp ON m.LoserId = pp.Id
JOIN Countries co ON c.CountryId = co.Id

--------------------- SELECT PLAYERS DATA -----------------------

SELECT
	p.Id,
	p.FirstName,
	p.LastName,
	p.Ranking,
	p.BirthDate,
	p.Height,
	p.Weight,
	ci.Name AS [City],
	co.Name AS [Country]
FROM Players p
JOIN Cities ci ON ci.Id = p.CityId
JOIN Countries co ON co.Id = ci.CountryId

