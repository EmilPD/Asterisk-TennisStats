select distinct Name, 
date as StartDate,
 DATEADD(DAY, +7, date) as EndDate, 
 '1000000' as PrizeMoney,
 'ATP-250' as Category, 
 '32' as PlayersCount, 
 Name as City,
 'Australia' as Country,
 'Hard' as Surface,
 'Fast' as Speed
into tournaments 
from atp_matches_2016 where Name not like 'Davis%'

--------------------------------
update tournaments
set PrizeMoney = ABS(CONVERT(BIGINT,CONVERT(BINARY(8), NEWID())))%1000000

--------------------------------
select SUBSTRING(Winner,0,CHARINDEX(' ',Winner,0)) as FirstName,  
SUBSTRING(Winner,CHARINDEX(' ',Winner,0)+1,LEN(Winner)) as LastName,  
(select top(1) Winner_Rank from atp_matches_2016 where Winner = m.Winner) as Ranking, 
(select top(1) DATEADD(YEAR, -CAST(Winner_Age as float), GETDATE()) from atp_matches_2016 where Winner = m.Winner) as BirthDate,
(select top(1) Winner_HT from atp_matches_2016 where Winner = m.Winner) as Height,
Null as City,
Null as Country,
(select top(1) Winner_Co from atp_matches_2016 where Winner = m.Winner) as CountryCode
into players
from atp_matches_2016 m
group by Winner