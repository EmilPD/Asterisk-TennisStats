# Team Asterisk - Teamwork Project

**Telerik Academy - Season 8 - Databases Course**

## Initial Project Structure

- TeamAsterisk.DataSources		// here are all XML, JSON and/or Excel Files
- TeamAsterisk.Reports			// here goes all PDF reports
- TeamAsterisk.XMLDataReader		//\
- TeamAsterisk.ExcelDataReader 	//	-- only one is required
- TeamAsterisk.JSONDataReader		///
- TeamAsterisk.ExportToPDF
- TeamAsterisk.Models
- TeamAsterisk.PostgreSQLData
- TeamAsterisk.SQLiteData
- TeamAsterisk.SQLServerData
- TeamAsterisk.Utils
- TeamAsterisk.Client				// WPF or Console App or ASP.NET Web App
- TeamAsterisk.UnitTests


## ATP Tennis Statistics

### [Requirements for teamwork](https://github.com/TelerikAcademy/Databases/tree/master/Teamwork/2017)

### Data to store in database
- [Players](http://www.atpworldtour.com/en/rankings/singles)
    - Id (int)
    - FirstName (string)
    - LastName (string)
    - Ranking (int)
    - BirthDate (dateTime)
    - CityId (int, residence, country derived from here)
    - CoachId (optional)
- Cities
    - Id (int)
    - Name (string)
    - TournamentId (int, many to many table)
    - CountryId (int)
- Countries
    - Id (int)
    - Name (string)
- Tournaments-Cities ( intermediate table, for many-to-many relation )
    - CityId (int)
    - TournamentId (int)
- [Tournaments](http://www.atpworldtour.com/en/tournaments)
    - Id (int)
    - Name (string)
    - StartDate (dateTime)
    - EndDate (dateTime)
    - SurfaceId ( int, surfaces can be reused in maches )
    - PrizeMoney (money)
    - CityId (int, many to many table)
- [Matches](http://www.tennis-data.co.uk/alldata.php)
    - Id
    - DatePlayed
    - WinnerId
    - LoserId
    - Result (can be represented by games won by each player, or simple)
        - `6–4, 4–6, 7–6(7–5)` - simple style
        - Set1, Set2, Set3, Set4, Set5 - more complex style
        - W1, L1, W2, L2, W3, L3, W4, L4, W5, L5 - every player games won
    - TournamentId (tousnament defines other details - surface)
- [Surfaces](http://sportsbyapt.com/types-tennis-courts/)
    - Id
    - Name (string, clay, grass, hard, ...)

### Optional Data (tables)
Can Introduse other tables if needed:
- [Coaches](http://www.atpworldtour.com/en/players/coaches)
- [Umpires](https://en.wikipedia.org/wiki/List_of_tennis_umpires)
- [Rounds](https://en.wikipedia.org/wiki/Single-elimination_tournament) (1/16, 1/4)
- [TournamentSeries](https://en.wikipedia.org/wiki/Association_of_Tennis_Professionals) (250, 500, Grand Slam)

### Data to be Extracted
Possible type of data that can be extracted
- Players list with data
- Tournaments schedule by date, town, country
- Matches in tournament - results and winner
- Head to head matches and statistics
- Any other combination

### Data to be added
Simple ways to add data
- New Player
- New City
- New Tournament (or list of tournaments, city needs to exist)
- New Match that was played (or list of matches, players and tournament need to exist)