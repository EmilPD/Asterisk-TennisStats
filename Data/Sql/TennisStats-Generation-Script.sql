USE [master]
GO
/****** Object:  Database [TennisStats]    Script Date: 6/4/2017 2:04:26 AM ******/
CREATE DATABASE [TennisStats]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TennisStats', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\TennisStats.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TennisStats_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\TennisStats_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [TennisStats] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TennisStats].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TennisStats] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TennisStats] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TennisStats] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TennisStats] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TennisStats] SET ARITHABORT OFF 
GO
ALTER DATABASE [TennisStats] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TennisStats] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TennisStats] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TennisStats] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TennisStats] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TennisStats] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TennisStats] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TennisStats] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TennisStats] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TennisStats] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TennisStats] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TennisStats] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TennisStats] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TennisStats] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TennisStats] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TennisStats] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TennisStats] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TennisStats] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TennisStats] SET  MULTI_USER 
GO
ALTER DATABASE [TennisStats] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TennisStats] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TennisStats] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TennisStats] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TennisStats] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TennisStats] SET QUERY_STORE = OFF
GO
USE [TennisStats]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [TennisStats]
GO
/****** Object:  Table [dbo].[Cities]    Script Date: 6/4/2017 2:04:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](40) NULL,
	[TournamentId] [int] NULL,
	[CountryId] [int] NULL,
 CONSTRAINT [PK_Cities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Coaches]    Script Date: 6/4/2017 2:04:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coaches](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](40) NOT NULL,
	[LastName] [nvarchar](40) NOT NULL,
	[BirthDate] [smalldatetime] NULL,
 CONSTRAINT [PK_Coaches] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 6/4/2017 2:04:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](40) NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Matches]    Script Date: 6/4/2017 2:04:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Matches](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WinnerId] [int] NOT NULL,
	[LoserId] [int] NOT NULL,
	[Result] [nvarchar](40) NULL,
	[TournamentId] [int] NULL,
	[DatePlayed] [smalldatetime] NULL,
	[UmpireId] [int] NULL,
	[RoundId] [int] NULL,
 CONSTRAINT [PK_Matches] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Players]    Script Date: 6/4/2017 2:04:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Players](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](40) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Height] [numeric](3, 0) NULL,
	[Weight] [numeric](3, 0) NULL,
	[BirthDate] [smalldatetime] NULL,
	[CityId] [int] NULL,
	[CoachId] [int] NULL,
	[Ranking] [int] NOT NULL,
 CONSTRAINT [PK_Players] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PointDistributionKeys]    Script Date: 6/4/2017 2:04:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PointDistributionKeys](
	[RoundId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Points] [int] NULL,
 CONSTRAINT [PK_PointDistributionKeys] PRIMARY KEY CLUSTERED 
(
	[RoundId] ASC,
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rounds]    Script Date: 6/4/2017 2:04:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rounds](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](40) NULL,
 CONSTRAINT [PK_Rounds] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Surfaces]    Script Date: 6/4/2017 2:04:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Surfaces](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](40) NOT NULL,
	[Speed] [nvarchar](40) NULL,
 CONSTRAINT [PK_Surfaces] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TournamentCategories]    Script Date: 6/4/2017 2:04:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TournamentCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Category] [nvarchar](40) NOT NULL,
	[PlayersNumber] [int] NULL,
 CONSTRAINT [PK_Series] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tournaments]    Script Date: 6/4/2017 2:04:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tournaments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](40) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[StartDate] [smalldatetime] NOT NULL,
	[EndDate] [smalldatetime] NOT NULL,
	[SurfaceId] [int] NOT NULL,
	[PrizeMoney] [money] NOT NULL,
	[CityId] [int] NOT NULL,
 CONSTRAINT [PK_Tournaments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Umpires]    Script Date: 6/4/2017 2:04:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Umpires](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](40) NOT NULL,
	[LastName] [nvarchar](40) NOT NULL,
	[YearActive] [smallint] NULL,
 CONSTRAINT [PK_Umpires] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cities] ON 

INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (1, N'Doha      ', 6, 1)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (2, N'Chennai   ', 8, 2)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (3, N'Brisbane  ', 11, 3)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (4, N'Auckland  ', 12, 4)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (5, N'Sidney    ', 13, 3)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (6, N'Melbourne ', 14, 3)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (24, N'Montpellier', 16, 6)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (25, N'Sofia', 19, 7)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (26, N'Rotterdam', 22, 9)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (27, N'Memphis', NULL, 10)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (28, N'Buenos Aires', NULL, 5)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (29, N'Rio de Janeiro', NULL, 11)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (30, N'Marsellie', NULL, 6)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (31, N'Delray Beach', NULL, 10)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (32, N'Dubai', NULL, 12)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (33, N'Sao Paulo', NULL, 11)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (34, N'Indian Wells', NULL, 10)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (35, N'Houston', NULL, 10)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (36, N'Monte Carlo', NULL, 15)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (37, N'Barcelona', NULL, 16)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (38, N'Budapest', NULL, 17)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (39, N'Estoril', NULL, 18)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (40, N'Munich', NULL, 19)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (41, N'Istanbul', NULL, 20)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (42, N'Madrid', NULL, 16)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (43, N'Rome', NULL, 21)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (44, N'Geneva', NULL, 22)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (45, N'Lyon', NULL, 6)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (46, N'Paris', NULL, 6)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (47, N'Acapulco', NULL, 13)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (48, N'Marracesh', NULL, 14)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (49, N'Quito', 20, 8)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (50, N'Haskovo', NULL, 7)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (51, N'Glasgow', NULL, 23)
INSERT [dbo].[Cities] ([Id], [Name], [TournamentId], [CountryId]) VALUES (52, N'Belgrade', NULL, 24)
SET IDENTITY_INSERT [dbo].[Cities] OFF
SET IDENTITY_INSERT [dbo].[Coaches] ON 

INSERT [dbo].[Coaches] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (1, N'Magnus', N'Norman', CAST(N'1976-05-30T00:00:00' AS SmallDateTime))
INSERT [dbo].[Coaches] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (3, N'Jamie', N'Delgado', CAST(N'1977-03-21T00:00:00' AS SmallDateTime))
INSERT [dbo].[Coaches] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (4, N'Daniel', N'Valverdu', CAST(N'1986-03-17T00:00:00' AS SmallDateTime))
INSERT [dbo].[Coaches] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (5, N'Andre', N'Agasi', CAST(N'1970-04-29T00:00:00' AS SmallDateTime))
INSERT [dbo].[Coaches] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (6, N'Toni', N'Nadal', CAST(N'1961-02-21T00:00:00' AS SmallDateTime))
INSERT [dbo].[Coaches] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (7, N'Ivan', N'Ljubicic', CAST(N'1979-03-19T00:00:00' AS SmallDateTime))
INSERT [dbo].[Coaches] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (8, N'Franco', N'Davin', NULL)
INSERT [dbo].[Coaches] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (9, N'Ivan', N'Lendl', NULL)
INSERT [dbo].[Coaches] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (10, N'Boris', N'Becker', CAST(N'1967-10-22T00:00:00' AS SmallDateTime))
INSERT [dbo].[Coaches] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (11, N'Dante', N'Bottini', NULL)
INSERT [dbo].[Coaches] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (13, N'Riccardo', N'Piatti', NULL)
INSERT [dbo].[Coaches] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (15, N'Gunter', N'Bresnik', NULL)
SET IDENTITY_INSERT [dbo].[Coaches] OFF
SET IDENTITY_INSERT [dbo].[Countries] ON 

INSERT [dbo].[Countries] ([Id], [Name]) VALUES (1, N'Quatar')
INSERT [dbo].[Countries] ([Id], [Name]) VALUES (2, N'India')
INSERT [dbo].[Countries] ([Id], [Name]) VALUES (3, N'Australia')
INSERT [dbo].[Countries] ([Id], [Name]) VALUES (4, N'New Zealand')
INSERT [dbo].[Countries] ([Id], [Name]) VALUES (5, N'Argentina')
INSERT [dbo].[Countries] ([Id], [Name]) VALUES (6, N'France')
INSERT [dbo].[Countries] ([Id], [Name]) VALUES (7, N'Bulgaria')
INSERT [dbo].[Countries] ([Id], [Name]) VALUES (8, N'Equador')
INSERT [dbo].[Countries] ([Id], [Name]) VALUES (9, N'Netherlands')
INSERT [dbo].[Countries] ([Id], [Name]) VALUES (10, N'United States')
INSERT [dbo].[Countries] ([Id], [Name]) VALUES (11, N'Brazil')
INSERT [dbo].[Countries] ([Id], [Name]) VALUES (12, N'United Arab Emirates')
INSERT [dbo].[Countries] ([Id], [Name]) VALUES (13, N'Mexico')
INSERT [dbo].[Countries] ([Id], [Name]) VALUES (14, N'Morocco')
INSERT [dbo].[Countries] ([Id], [Name]) VALUES (15, N'Monaco')
INSERT [dbo].[Countries] ([Id], [Name]) VALUES (16, N'Spain')
INSERT [dbo].[Countries] ([Id], [Name]) VALUES (17, N'Hungary')
INSERT [dbo].[Countries] ([Id], [Name]) VALUES (18, N'Portugal')
INSERT [dbo].[Countries] ([Id], [Name]) VALUES (19, N'Germany')
INSERT [dbo].[Countries] ([Id], [Name]) VALUES (20, N'Turkey')
INSERT [dbo].[Countries] ([Id], [Name]) VALUES (21, N'Italy')
INSERT [dbo].[Countries] ([Id], [Name]) VALUES (22, N'Switzerland')
INSERT [dbo].[Countries] ([Id], [Name]) VALUES (23, N'Scotland')
INSERT [dbo].[Countries] ([Id], [Name]) VALUES (24, N'Serbia')
SET IDENTITY_INSERT [dbo].[Countries] OFF
SET IDENTITY_INSERT [dbo].[Matches] ON 

INSERT [dbo].[Matches] ([Id], [WinnerId], [LoserId], [Result], [TournamentId], [DatePlayed], [UmpireId], [RoundId]) VALUES (1, 6, 4, N'6–3, 5–7, 6–4', 1, CAST(N'2017-01-08T00:00:00' AS SmallDateTime), 1, 11)
INSERT [dbo].[Matches] ([Id], [WinnerId], [LoserId], [Result], [TournamentId], [DatePlayed], [UmpireId], [RoundId]) VALUES (2, 13, 14, N'6-4, 6-1', 2, CAST(N'2017-01-07T00:00:00' AS SmallDateTime), 2, 10)
INSERT [dbo].[Matches] ([Id], [WinnerId], [LoserId], [Result], [TournamentId], [DatePlayed], [UmpireId], [RoundId]) VALUES (3, 1, 15, N'6–2, 2–6, 6–3', 3, CAST(N'2017-01-08T00:00:00' AS SmallDateTime), 5, 11)
INSERT [dbo].[Matches] ([Id], [WinnerId], [LoserId], [Result], [TournamentId], [DatePlayed], [UmpireId], [RoundId]) VALUES (4, 10, 8, N'6–4, 7–5', 11, CAST(N'2017-03-12T00:00:00' AS SmallDateTime), 4, 11)
INSERT [dbo].[Matches] ([Id], [WinnerId], [LoserId], [Result], [TournamentId], [DatePlayed], [UmpireId], [RoundId]) VALUES (9, 6, 9, N'6-1, 7-6', 4, CAST(N'2017-01-11T00:00:00' AS SmallDateTime), 6, 8)
INSERT [dbo].[Matches] ([Id], [WinnerId], [LoserId], [Result], [TournamentId], [DatePlayed], [UmpireId], [RoundId]) VALUES (11, 9, 11, N'6–3, 5–7, 6–3', 5, CAST(N'2017-01-14T00:00:00' AS SmallDateTime), 2, 9)
INSERT [dbo].[Matches] ([Id], [WinnerId], [LoserId], [Result], [TournamentId], [DatePlayed], [UmpireId], [RoundId]) VALUES (13, 10, 9, N'6–4, 3–6, 6–1, 3–6, 6–3', 6, CAST(N'2017-01-23T00:00:00' AS SmallDateTime), 1, 11)
SET IDENTITY_INSERT [dbo].[Matches] OFF
SET IDENTITY_INSERT [dbo].[Players] ON 

INSERT [dbo].[Players] ([Id], [FirstName], [LastName], [Height], [Weight], [BirthDate], [CityId], [CoachId], [Ranking]) VALUES (1, N'Grigor', N'Dimitrov', CAST(191 AS Numeric(3, 0)), CAST(80 AS Numeric(3, 0)), CAST(N'1991-05-16T00:00:00' AS SmallDateTime), 50, 4, 13)
INSERT [dbo].[Players] ([Id], [FirstName], [LastName], [Height], [Weight], [BirthDate], [CityId], [CoachId], [Ranking]) VALUES (4, N'Andy', N'Murray', CAST(190 AS Numeric(3, 0)), CAST(84 AS Numeric(3, 0)), CAST(N'1987-05-15T00:00:00' AS SmallDateTime), 51, 9, 1)
INSERT [dbo].[Players] ([Id], [FirstName], [LastName], [Height], [Weight], [BirthDate], [CityId], [CoachId], [Ranking]) VALUES (6, N'Novak', N'Djokovic', CAST(188 AS Numeric(3, 0)), CAST(77 AS Numeric(3, 0)), CAST(N'1987-05-22T00:00:00' AS SmallDateTime), 52, 10, 2)
INSERT [dbo].[Players] ([Id], [FirstName], [LastName], [Height], [Weight], [BirthDate], [CityId], [CoachId], [Ranking]) VALUES (8, N'Stan', N'Wawrinka', CAST(183 AS Numeric(3, 0)), CAST(81 AS Numeric(3, 0)), CAST(N'1985-03-28T00:00:00' AS SmallDateTime), 44, 1, 3)
INSERT [dbo].[Players] ([Id], [FirstName], [LastName], [Height], [Weight], [BirthDate], [CityId], [CoachId], [Ranking]) VALUES (9, N'Rafael', N'Nadal', CAST(185 AS Numeric(3, 0)), CAST(85 AS Numeric(3, 0)), CAST(N'1986-06-03T00:00:00' AS SmallDateTime), 42, 6, 4)
INSERT [dbo].[Players] ([Id], [FirstName], [LastName], [Height], [Weight], [BirthDate], [CityId], [CoachId], [Ranking]) VALUES (10, N'Roger', N'Federer', CAST(185 AS Numeric(3, 0)), CAST(85 AS Numeric(3, 0)), CAST(N'1981-08-08T00:00:00' AS SmallDateTime), 44, 7, 5)
INSERT [dbo].[Players] ([Id], [FirstName], [LastName], [Height], [Weight], [BirthDate], [CityId], [CoachId], [Ranking]) VALUES (11, N'Milos', N'Raonic', CAST(196 AS Numeric(3, 0)), CAST(98 AS Numeric(3, 0)), CAST(N'1990-12-27T00:00:00' AS SmallDateTime), 36, 13, 6)
INSERT [dbo].[Players] ([Id], [FirstName], [LastName], [Height], [Weight], [BirthDate], [CityId], [CoachId], [Ranking]) VALUES (13, N'Tomas', N'Berdych', CAST(196 AS Numeric(3, 0)), CAST(91 AS Numeric(3, 0)), CAST(N'1985-09-17T00:00:00' AS SmallDateTime), NULL, NULL, 14)
INSERT [dbo].[Players] ([Id], [FirstName], [LastName], [Height], [Weight], [BirthDate], [CityId], [CoachId], [Ranking]) VALUES (14, N'Fernando', N'Verdasco', CAST(185 AS Numeric(3, 0)), CAST(87 AS Numeric(3, 0)), CAST(N'1983-11-15T00:00:00' AS SmallDateTime), 42, NULL, 37)
INSERT [dbo].[Players] ([Id], [FirstName], [LastName], [Height], [Weight], [BirthDate], [CityId], [CoachId], [Ranking]) VALUES (15, N'Kei', N'Nishikori', CAST(178 AS Numeric(3, 0)), CAST(75 AS Numeric(3, 0)), CAST(N'1989-12-29T00:00:00' AS SmallDateTime), NULL, NULL, 9)
SET IDENTITY_INSERT [dbo].[Players] OFF
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (1, 1, NULL)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (1, 2, NULL)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (1, 3, NULL)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (1, 4, NULL)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (1, 5, NULL)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (1, 6, NULL)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (1, 7, NULL)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (1, 8, NULL)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (2, 1, NULL)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (2, 2, 6)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (2, 3, 3)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (2, 4, 10)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (2, 5, 4)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (2, 6, 16)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (2, 7, 200)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (2, 8, 8)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (3, 1, NULL)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (3, 2, NULL)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (3, 3, NULL)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (3, 4, NULL)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (3, 5, NULL)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (3, 6, NULL)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (3, 7, 200)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (3, 8, 16)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (4, 1, 10)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (4, 2, 20)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (4, 3, 10)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (4, 5, 25)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (4, 6, 16)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (4, 7, 200)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (4, 8, 25)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (5, 1, NULL)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (5, 2, NULL)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (5, 3, NULL)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (5, 4, NULL)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (5, 5, NULL)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (5, 6, 10)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (5, 7, 200)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (5, 8, 10)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (6, 1, 0)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (6, 2, NULL)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (6, 3, 0)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (6, 4, NULL)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (6, 5, 10)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (6, 6, 25)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (6, 7, 200)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (6, 8, 45)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (7, 1, 0)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (7, 2, 10)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (7, 3, 0)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (7, 4, 20)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (7, 5, 45)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (7, 6, 45)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (7, 7, 200)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (7, 8, 90)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (8, 1, 20)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (8, 2, 20)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (8, 3, 45)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (8, 4, 45)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (8, 5, 90)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (8, 6, 90)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (8, 7, 200)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (8, 8, 180)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (9, 1, 45)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (9, 2, 45)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (9, 3, 90)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (9, 4, 90)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (9, 5, 180)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (9, 6, 180)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (9, 7, 200)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (9, 8, 360)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (10, 1, 90)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (10, 2, 90)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (10, 3, 180)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (10, 4, 180)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (10, 5, 360)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (10, 6, 360)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (10, 7, 600)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (10, 8, 720)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (11, 1, 150)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (11, 2, 150)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (11, 3, 300)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (11, 4, 300)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (11, 5, 600)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (11, 6, 600)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (11, 7, 1000)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (11, 8, 1200)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (12, 1, 250)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (12, 2, 250)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (12, 3, 500)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (12, 4, 500)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (12, 5, 1000)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (12, 6, 1000)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (12, 7, 1500)
INSERT [dbo].[PointDistributionKeys] ([RoundId], [CategoryId], [Points]) VALUES (12, 8, 2000)
SET IDENTITY_INSERT [dbo].[Rounds] ON 

INSERT [dbo].[Rounds] ([Id], [Name]) VALUES (1, N'Q-1')
INSERT [dbo].[Rounds] ([Id], [Name]) VALUES (2, N'Q-2')
INSERT [dbo].[Rounds] ([Id], [Name]) VALUES (3, N'Q-3')
INSERT [dbo].[Rounds] ([Id], [Name]) VALUES (4, N'Q')
INSERT [dbo].[Rounds] ([Id], [Name]) VALUES (5, N'R-128')
INSERT [dbo].[Rounds] ([Id], [Name]) VALUES (6, N'R-64')
INSERT [dbo].[Rounds] ([Id], [Name]) VALUES (7, N'R-32')
INSERT [dbo].[Rounds] ([Id], [Name]) VALUES (8, N'R-16')
INSERT [dbo].[Rounds] ([Id], [Name]) VALUES (9, N'Quarter Final')
INSERT [dbo].[Rounds] ([Id], [Name]) VALUES (10, N'Semi Final')
INSERT [dbo].[Rounds] ([Id], [Name]) VALUES (11, N'Final')
INSERT [dbo].[Rounds] ([Id], [Name]) VALUES (12, N'Winner')
SET IDENTITY_INSERT [dbo].[Rounds] OFF
SET IDENTITY_INSERT [dbo].[Surfaces] ON 

INSERT [dbo].[Surfaces] ([Id], [Type], [Speed]) VALUES (1, N'Clay', N'Slow')
INSERT [dbo].[Surfaces] ([Id], [Type], [Speed]) VALUES (2, N'Carpet', N'Medium')
INSERT [dbo].[Surfaces] ([Id], [Type], [Speed]) VALUES (3, N'Hard', N'Fast')
INSERT [dbo].[Surfaces] ([Id], [Type], [Speed]) VALUES (4, N'Grass', N'Super Fast')
SET IDENTITY_INSERT [dbo].[Surfaces] OFF
SET IDENTITY_INSERT [dbo].[TournamentCategories] ON 

INSERT [dbo].[TournamentCategories] ([Id], [Category], [PlayersNumber]) VALUES (1, N'ATP-250', 32)
INSERT [dbo].[TournamentCategories] ([Id], [Category], [PlayersNumber]) VALUES (2, N'ATP-250', 48)
INSERT [dbo].[TournamentCategories] ([Id], [Category], [PlayersNumber]) VALUES (3, N'ATP-500', 32)
INSERT [dbo].[TournamentCategories] ([Id], [Category], [PlayersNumber]) VALUES (4, N'ATP-500', 48)
INSERT [dbo].[TournamentCategories] ([Id], [Category], [PlayersNumber]) VALUES (5, N'Masters-1000', 56)
INSERT [dbo].[TournamentCategories] ([Id], [Category], [PlayersNumber]) VALUES (6, N'Masters-1000', 96)
INSERT [dbo].[TournamentCategories] ([Id], [Category], [PlayersNumber]) VALUES (7, N'ATP-Finals', 8)
INSERT [dbo].[TournamentCategories] ([Id], [Category], [PlayersNumber]) VALUES (8, N'Grand Slam', 128)
SET IDENTITY_INSERT [dbo].[TournamentCategories] OFF
SET IDENTITY_INSERT [dbo].[Tournaments] ON 

INSERT [dbo].[Tournaments] ([Id], [Name], [CategoryId], [StartDate], [EndDate], [SurfaceId], [PrizeMoney], [CityId]) VALUES (1, N'Qatar Open', 1, CAST(N'2017-01-02T00:00:00' AS SmallDateTime), CAST(N'2017-01-09T00:00:00' AS SmallDateTime), 3, 1334270.0000, 1)
INSERT [dbo].[Tournaments] ([Id], [Name], [CategoryId], [StartDate], [EndDate], [SurfaceId], [PrizeMoney], [CityId]) VALUES (2, N'Chennai Open', 1, CAST(N'2017-01-02T00:00:00' AS SmallDateTime), CAST(N'2017-01-09T00:00:00' AS SmallDateTime), 3, 505730.0000, 2)
INSERT [dbo].[Tournaments] ([Id], [Name], [CategoryId], [StartDate], [EndDate], [SurfaceId], [PrizeMoney], [CityId]) VALUES (3, N'Brisbane International', 1, CAST(N'2017-01-02T00:00:00' AS SmallDateTime), CAST(N'2017-01-09T00:00:00' AS SmallDateTime), 3, 495630.0000, 3)
INSERT [dbo].[Tournaments] ([Id], [Name], [CategoryId], [StartDate], [EndDate], [SurfaceId], [PrizeMoney], [CityId]) VALUES (4, N'Auckland Open', 1, CAST(N'2017-01-09T00:00:00' AS SmallDateTime), CAST(N'2017-01-15T00:00:00' AS SmallDateTime), 3, 508360.0000, 4)
INSERT [dbo].[Tournaments] ([Id], [Name], [CategoryId], [StartDate], [EndDate], [SurfaceId], [PrizeMoney], [CityId]) VALUES (5, N'Sydney International', 1, CAST(N'2017-01-09T00:00:00' AS SmallDateTime), CAST(N'2017-01-15T00:00:00' AS SmallDateTime), 3, 495630.0000, 5)
INSERT [dbo].[Tournaments] ([Id], [Name], [CategoryId], [StartDate], [EndDate], [SurfaceId], [PrizeMoney], [CityId]) VALUES (6, N'Australian Open', 8, CAST(N'2017-01-16T00:00:00' AS SmallDateTime), CAST(N'2017-01-23T00:00:00' AS SmallDateTime), 3, 22624000.0000, 6)
INSERT [dbo].[Tournaments] ([Id], [Name], [CategoryId], [StartDate], [EndDate], [SurfaceId], [PrizeMoney], [CityId]) VALUES (7, N'Open Sud de France', 1, CAST(N'2017-02-06T00:00:00' AS SmallDateTime), CAST(N'2017-02-12T00:00:00' AS SmallDateTime), 3, 540000.0000, 24)
INSERT [dbo].[Tournaments] ([Id], [Name], [CategoryId], [StartDate], [EndDate], [SurfaceId], [PrizeMoney], [CityId]) VALUES (8, N'Sofia Open', 1, CAST(N'2017-02-06T00:00:00' AS SmallDateTime), CAST(N'2017-02-12T00:00:00' AS SmallDateTime), 3, 540310.0000, 25)
INSERT [dbo].[Tournaments] ([Id], [Name], [CategoryId], [StartDate], [EndDate], [SurfaceId], [PrizeMoney], [CityId]) VALUES (9, N'Ecuador Open', 1, CAST(N'2017-02-06T00:00:00' AS SmallDateTime), CAST(N'2017-02-12T00:00:00' AS SmallDateTime), 1, 540300.0000, 49)
INSERT [dbo].[Tournaments] ([Id], [Name], [CategoryId], [StartDate], [EndDate], [SurfaceId], [PrizeMoney], [CityId]) VALUES (10, N'Rotterdam Open', 3, CAST(N'2017-02-13T00:00:00' AS SmallDateTime), CAST(N'2017-02-19T00:00:00' AS SmallDateTime), 1, 1854365.0000, 26)
INSERT [dbo].[Tournaments] ([Id], [Name], [CategoryId], [StartDate], [EndDate], [SurfaceId], [PrizeMoney], [CityId]) VALUES (11, N'Indian Wells', 6, CAST(N'2017-03-06T00:00:00' AS SmallDateTime), CAST(N'2017-03-12T00:00:00' AS SmallDateTime), 1, 7913400.0000, 34)
SET IDENTITY_INSERT [dbo].[Tournaments] OFF
SET IDENTITY_INSERT [dbo].[Umpires] ON 

INSERT [dbo].[Umpires] ([Id], [FirstName], [LastName], [YearActive]) VALUES (1, N'Pascal', N'Maria', 2000)
INSERT [dbo].[Umpires] ([Id], [FirstName], [LastName], [YearActive]) VALUES (2, N'Carlos', N'Ramos', 2002)
INSERT [dbo].[Umpires] ([Id], [FirstName], [LastName], [YearActive]) VALUES (3, N'Carlos', N'Bernardes', 1990)
INSERT [dbo].[Umpires] ([Id], [FirstName], [LastName], [YearActive]) VALUES (4, N'Cedric', N'Mourier', 1999)
INSERT [dbo].[Umpires] ([Id], [FirstName], [LastName], [YearActive]) VALUES (5, N'Damian', N'Steiner', 2005)
INSERT [dbo].[Umpires] ([Id], [FirstName], [LastName], [YearActive]) VALUES (6, N'Emmanuel', N'Joseph', 2005)
INSERT [dbo].[Umpires] ([Id], [FirstName], [LastName], [YearActive]) VALUES (7, N'Felix', N'Torralba', 2013)
SET IDENTITY_INSERT [dbo].[Umpires] OFF
ALTER TABLE [dbo].[Cities]  WITH CHECK ADD  CONSTRAINT [FK_Cities_Countries] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([Id])
GO
ALTER TABLE [dbo].[Cities] CHECK CONSTRAINT [FK_Cities_Countries]
GO
ALTER TABLE [dbo].[Matches]  WITH CHECK ADD  CONSTRAINT [FK_Matches_Players] FOREIGN KEY([LoserId])
REFERENCES [dbo].[Players] ([Id])
GO
ALTER TABLE [dbo].[Matches] CHECK CONSTRAINT [FK_Matches_Players]
GO
ALTER TABLE [dbo].[Matches]  WITH CHECK ADD  CONSTRAINT [FK_Matches_Players1] FOREIGN KEY([WinnerId])
REFERENCES [dbo].[Players] ([Id])
GO
ALTER TABLE [dbo].[Matches] CHECK CONSTRAINT [FK_Matches_Players1]
GO
ALTER TABLE [dbo].[Matches]  WITH CHECK ADD  CONSTRAINT [FK_Matches_Rounds] FOREIGN KEY([RoundId])
REFERENCES [dbo].[Rounds] ([Id])
GO
ALTER TABLE [dbo].[Matches] CHECK CONSTRAINT [FK_Matches_Rounds]
GO
ALTER TABLE [dbo].[Matches]  WITH CHECK ADD  CONSTRAINT [FK_Matches_Tournaments] FOREIGN KEY([TournamentId])
REFERENCES [dbo].[Tournaments] ([Id])
GO
ALTER TABLE [dbo].[Matches] CHECK CONSTRAINT [FK_Matches_Tournaments]
GO
ALTER TABLE [dbo].[Matches]  WITH CHECK ADD  CONSTRAINT [FK_Matches_Umpires] FOREIGN KEY([UmpireId])
REFERENCES [dbo].[Umpires] ([Id])
GO
ALTER TABLE [dbo].[Matches] CHECK CONSTRAINT [FK_Matches_Umpires]
GO
ALTER TABLE [dbo].[Players]  WITH CHECK ADD  CONSTRAINT [FK_Players_Cities] FOREIGN KEY([CityId])
REFERENCES [dbo].[Cities] ([Id])
GO
ALTER TABLE [dbo].[Players] CHECK CONSTRAINT [FK_Players_Cities]
GO
ALTER TABLE [dbo].[Players]  WITH CHECK ADD  CONSTRAINT [FK_Players_Coaches1] FOREIGN KEY([CoachId])
REFERENCES [dbo].[Coaches] ([Id])
GO
ALTER TABLE [dbo].[Players] CHECK CONSTRAINT [FK_Players_Coaches1]
GO
ALTER TABLE [dbo].[PointDistributionKeys]  WITH CHECK ADD  CONSTRAINT [FK_PointDistributionKeys_Rounds] FOREIGN KEY([RoundId])
REFERENCES [dbo].[Rounds] ([Id])
GO
ALTER TABLE [dbo].[PointDistributionKeys] CHECK CONSTRAINT [FK_PointDistributionKeys_Rounds]
GO
ALTER TABLE [dbo].[PointDistributionKeys]  WITH CHECK ADD  CONSTRAINT [FK_PointDistributionKeys_TournamentCategories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[TournamentCategories] ([Id])
GO
ALTER TABLE [dbo].[PointDistributionKeys] CHECK CONSTRAINT [FK_PointDistributionKeys_TournamentCategories]
GO
ALTER TABLE [dbo].[Tournaments]  WITH CHECK ADD  CONSTRAINT [FK_Tournaments_Cities] FOREIGN KEY([CityId])
REFERENCES [dbo].[Cities] ([Id])
GO
ALTER TABLE [dbo].[Tournaments] CHECK CONSTRAINT [FK_Tournaments_Cities]
GO
ALTER TABLE [dbo].[Tournaments]  WITH CHECK ADD  CONSTRAINT [FK_Tournaments_Surfaces] FOREIGN KEY([SurfaceId])
REFERENCES [dbo].[Surfaces] ([Id])
GO
ALTER TABLE [dbo].[Tournaments] CHECK CONSTRAINT [FK_Tournaments_Surfaces]
GO
ALTER TABLE [dbo].[Tournaments]  WITH CHECK ADD  CONSTRAINT [FK_Tournaments_TournamentCategories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[TournamentCategories] ([Id])
GO
ALTER TABLE [dbo].[Tournaments] CHECK CONSTRAINT [FK_Tournaments_TournamentCategories]
GO
USE [master]
GO
ALTER DATABASE [TennisStats] SET  READ_WRITE 
GO
