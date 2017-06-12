# Team Asterisk - Teamwork Project

**Telerik Academy - Season 8 - Databases Course**

## Team Members

| Name | Telerik Student system username |
|:----:|:-----------------------:|
| [Захари Димитров](https://github.com/zachdimitrov) | ZachD |
| [Иван Петров](https://github.com/tinmanjk) | tinman |
| [Емил Димитров](https://github.com/EmilPD) | qwerty123 |

## ATP Tennis Statistics
ATP Tennis Stats is a simple CLI program that stores and displays data for tennis events, players and matches with a variety of data for each item. It uses 3 different databases with Entity Framework to access them using code-first approach. All databases are created using thos approach and no need of DB management is required outside of the C# source code.

This is how databases are used:
| Database | Data stored |
|:----:|:-----------------------|
| [MS SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-2016) | All tennis statistics tables |
| [Postgre SQL](https://www.postgresql.org/) | ticket store service data - tickets, events |
| [SQLite](https://www.sqlite.org/) | Logs table for events and exceptions that occur while application is used |

### [Requirements for teamwork](https://github.com/TelerikAcademy/Databases/tree/master/Teamwork/2017)

### Data to store in database
#### SQL Server 
- [Players](http://www.atpworldtour.com/en/rankings/singles)
    - Id (int)
    - FirstName (string)
    - LastName (string)
    - Ranking (int)
    - Height (float)
    - Weight (float)
    - BirthDate (dateTime)
    - CityId (int, residence, country derived from here)
    - Ranking (optional)
- Cities
    - Id (int)
    - Name (string)
    - TournamentId (int, Many tournaments can be made in one City)
    - CountryId (int)
- Countries
    - Id (int)
    - Name (string)
- [Tournaments](http://www.atpworldtour.com/en/tournaments)
    - Id (int)
    - Name (string)
    - StartDate (dateTime)
    - EndDate (dateTime)
    - SurfaceId ( int, surfaces can be reused in maches )
    - PrizeMoney (money)
    - Category (TournamentCategory)
    - CityId (one City can have many Tournaments!)
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
    - Type (string - clay, grass, hard, ...)
    - Speed (string - slow, fast, super-fast)
- [TournamentCategories](https://en.wikipedia.org/wiki/Association_of_Tennis_Professionals) (250, 500, Grand Slam)
    - Id
    - Category (string)
    - PlayersCount (int)
- [Rounds](https://en.wikipedia.org/wiki/Single-elimination_tournament) (1/16, 1/4)
    - Id
    - Stage (RoundStage - enumeration - QF, R32, R64, ..., SF, F)
- [PointDistribution](https://en.wikipedia.org/wiki/2017_ATP_World_Tour)
    - Id
    - TournamentCategory
    - Round
    - Points (int - points if round is passed)

### Extracted data in application
- Players list with simple data
- Full data for a single player (id provided from players list)
- Tournaments schedule by date, town, country
- Full data for single tournament (id provided from tournaments list)
- Matches in tournament - results and winner
- Full data for single match (id provided from matches list)
- Logs Table

### PDF reports can be created for
- Matches data
- Players ranking list

### Different ways for importing data
#### With import from **excell**
- Tournaments list
- Matches list with full matches data
- Players list
- Points distribution for each round in every tournament category
- Full Sample data (all lists at once)
#### With JSON Importer
- List of all countries

### Data to be added with console application
Simple ways to add data
- New Player
- New City
- New Country
- New Tournament (with 4 to 10 arguments)
- New Match that was played (with 8 arguments)

### Ticket store
- Shows available tickets for sale
- Shows list of events
- Provides logic for ticket buying

## Command Line Interface
### Commands and usage
- type the command shown in [square brackets] to perform operation

**Header**
```
< WELCOME TO ASTERISK - TENNIS STATS >
----------------------------------------
   Please input the command from the 
   options below and press <enter>
----------------------------------------
```

 **Main Menu**
```
 [r] Tennis reporters
 [i] Import data
 [s] Tennis statistics menu
 [t] Ticket store menu
 [l] Show all logs
 [a] Team Asterisk info

 [exit]
```
 **IMporters Menu**
```
 [importm] Import matches
 [importp] Import Players
 [importpd] Tennis point distribution
 [importt] Import tournaments
 [importsd] Import sample data

 [menu]
```
  **PDF Reports Menu**
```
 [pdfm] Create PDF report for matches
 [pdfr] Create PDF report for ranking

 [menu]
```
 **Ticket Store Menu**
```
 [allt] Show all tickets
 [alle] Show all events
 [buyt (id)] buy a ticket with (id)

 [menu]
```
 **Available Data Menus**
```
 [show] Show tennis data menu
 [add] Add tennis data menu

 [menu]
```
 **Generate Data Menu**
```
 [addco (name)] Add new country
 [addct (name) (country)] Add new city
 [addp (2 - 7 arguments)] Add new player
 [updatep (id)] Update Player with id
 [addt (4 - 10 arguments)] Add new tournament
 [addm (6 arguments)] Add new match
 [delm (id)] Delete Match with id

 [menu] [show]
```
 **Show Data Menu**
```
 [showp (id)] Show all players of filter by id
 [showt (id)] Show all tournaments of filter by id
 [showm (id)] Show all mathes of filter by id

 [menu] [add]
```