USE ATPTennisStatSqlServer

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
	tc.Category AS [Category],
	tr.Stage AS [Round],
    c.Name AS [City],
	co.Name AS [Country],
	t.StartDate AS [StartDate],
	t.EndDate AS [EndDate],
	ts.Type AS [Surface],
	ts.Speed AS [Speed],
	t.PrizeMoney AS [PrizeMoney]
FROM Matches m
JOIN Tournaments t ON t.Id = m.Tournament_Id
JOIN TournamentCategories tc ON t.Category_Id = tc.Id
JOIN Rounds tr ON tr.Id = m.Round_Id
JOIN Surfaces ts ON ts.Id = t.Type_Id
JOIN PointDistributions pow ON pow.TournamentCategoryId = t.Category_Id AND pow.RoundId = (m.Round_Id)
JOIN PointDistributions pol ON pol.TournamentCategoryId = t.Category_Id AND pol.RoundId = (m.Round_Id-1)
JOIN Cities c ON c.Id = t.City_Id
JOIN Players p ON m.Winner_Id = p.Id
JOIN Players pp ON m.Loser_Id = pp.Id
JOIN Countries co ON c.Country_Id = co.Id

----------------------- SELECT PLAYERS DATA -----------------------

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
LEFT JOIN Cities ci ON ci.Id = p.City_Id
LEFT JOIN Countries co ON co.Id = ci.Country_Id