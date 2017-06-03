Team


# ATP Tennis Statistics

## [Requirements for teamwork](https://github.com/TelerikAcademy/Databases/tree/master/Teamwork/2017)

## Data to store in database
- Players
    - Id (int)
    - FirstName (string)
    - LastName (string)
    - Ranking (int)
    - BirthDate (dateTime)
    - CityId (int, esidence)
- Cities
    - Id (int)
    - Name (string)
    - TournamentId (int, many to many table)
    - CountryId (int)
- Countries
    - Id (int)
    - Name (string)
- Tournaments-Cities
    - CityId (int)
    - TournamentId (int)
- Tournaments
    - Id (int)
    - Name (string)
    - StartDate (dateTime)
    - EndDate (dateTime)
    - SurfaceId ( int, surfaces can be reused in maches )
    - PrizeMoney (money)
    - CityId (int, many to many table)
- Matches
    - Id
    - DatePlayed
    - FirstPlayerId
    - SecondPlayerId
    - Result (string)
    - WinnerId
    - TournamentId
- Surfaces
    - Id
    - Name (string, clay, grass, hard, ...)

## Optional Data (tables)
Can Introduse other tables if needed:
- Coaches
- Umpires
- Rounds (1/16, 1/4)
- TournamentRanks (250, 500, Grand Slam)

## Data to be Extracted
Possible type of data that can be extracted
- Players list with data
- Tournaments schedule by date, town, country
- Matches in tournament - results and winner
- Head to head matches and statistics
- Any other combination

## Data to be added
Simple ways to add data
- New Player
- New City
- New Tournament (or list of tournaments, city needs to exist)
- New Match that was played (or list of matches, players and tournament need to exist)
