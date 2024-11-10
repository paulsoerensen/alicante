IF  EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_DiagramPaneCount' , N'SCHEMA',N'al', N'VIEW',N'vResult', NULL,NULL))
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPaneCount' , @level0type=N'SCHEMA',@level0name=N'al', @level1type=N'VIEW',@level1name=N'vResult'
GO
IF  EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_DiagramPane2' , N'SCHEMA',N'al', N'VIEW',N'vResult', NULL,NULL))
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane2' , @level0type=N'SCHEMA',@level0name=N'al', @level1type=N'VIEW',@level1name=N'vResult'
GO
IF  EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'al', N'VIEW',N'vResult', NULL,NULL))
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane1' , @level0type=N'SCHEMA',@level0name=N'al', @level1type=N'VIEW',@level1name=N'vResult'
GO
/****** Object:  StoredProcedure [al].[UpsertTournament]    Script Date: 10/11/2024 22.36.28 ******/
DROP PROCEDURE IF EXISTS [al].[UpsertTournament]
GO
/****** Object:  StoredProcedure [al].[UpsertResult]    Script Date: 10/11/2024 22.36.28 ******/
DROP PROCEDURE IF EXISTS [al].[UpsertResult]
GO
/****** Object:  StoredProcedure [al].[UpsertPlayer]    Script Date: 10/11/2024 22.36.28 ******/
DROP PROCEDURE IF EXISTS [al].[UpsertPlayer]
GO
/****** Object:  StoredProcedure [al].[UpsertMatch]    Script Date: 10/11/2024 22.36.28 ******/
DROP PROCEDURE IF EXISTS [al].[UpsertMatch]
GO
/****** Object:  StoredProcedure [al].[UpsertCourse]    Script Date: 10/11/2024 22.36.28 ******/
DROP PROCEDURE IF EXISTS [al].[UpsertCourse]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[al].[Result]') AND type in (N'U'))
ALTER TABLE [al].[Result] DROP CONSTRAINT IF EXISTS [FK_Result_Player]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[al].[Result]') AND type in (N'U'))
ALTER TABLE [al].[Result] DROP CONSTRAINT IF EXISTS [FK_Result_Match]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[al].[Match]') AND type in (N'U'))
ALTER TABLE [al].[Match] DROP CONSTRAINT IF EXISTS [FK_Match_Tournament]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[al].[Match]') AND type in (N'U'))
ALTER TABLE [al].[Match] DROP CONSTRAINT IF EXISTS [FK_Match_Course]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[al].[Tournament]') AND type in (N'U'))
ALTER TABLE [al].[Tournament] DROP CONSTRAINT IF EXISTS [DF_Tournament_Active]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[al].[Result]') AND type in (N'U'))
ALTER TABLE [al].[Result] DROP CONSTRAINT IF EXISTS [DF_Result_Par3]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[al].[Result]') AND type in (N'U'))
ALTER TABLE [al].[Result] DROP CONSTRAINT IF EXISTS [DF_Result_Birdies]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[al].[Result]') AND type in (N'U'))
ALTER TABLE [al].[Result] DROP CONSTRAINT IF EXISTS [DF_Result_Score]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[al].[Result]') AND type in (N'U'))
ALTER TABLE [al].[Result] DROP CONSTRAINT IF EXISTS [DF_Result_Hcp]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[al].[Match]') AND type in (N'U'))
ALTER TABLE [al].[Match] DROP CONSTRAINT IF EXISTS [DF_Match_MatchDate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[al].[Course]') AND type in (N'U'))
ALTER TABLE [al].[Course] DROP CONSTRAINT IF EXISTS [DF_Course_Par]
GO
/****** Object:  Index [UNC_Match_MatchDate_CourseId]    Script Date: 10/11/2024 22.36.28 ******/
DROP INDEX IF EXISTS [UNC_Match_MatchDate_CourseId] ON [al].[Match]
GO
/****** Object:  View [al].[vPlayersForMatch]    Script Date: 10/11/2024 22.36.28 ******/
DROP VIEW IF EXISTS [al].[vPlayersForMatch]
GO
/****** Object:  View [al].[vResult]    Script Date: 10/11/2024 22.36.28 ******/
DROP VIEW IF EXISTS [al].[vResult]
GO
/****** Object:  Table [al].[Result]    Script Date: 10/11/2024 22.36.28 ******/
DROP TABLE IF EXISTS [al].[Result]
GO
/****** Object:  Table [al].[Player]    Script Date: 10/11/2024 22.36.28 ******/
DROP TABLE IF EXISTS [al].[Player]
GO
/****** Object:  View [al].[vMatch]    Script Date: 10/11/2024 22.36.28 ******/
DROP VIEW IF EXISTS [al].[vMatch]
GO
/****** Object:  Table [al].[Tournament]    Script Date: 10/11/2024 22.36.28 ******/
DROP TABLE IF EXISTS [al].[Tournament]
GO
/****** Object:  Table [al].[Match]    Script Date: 10/11/2024 22.36.28 ******/
DROP TABLE IF EXISTS [al].[Match]
GO
/****** Object:  Table [al].[Course]    Script Date: 10/11/2024 22.36.28 ******/
DROP TABLE IF EXISTS [al].[Course]
GO
/****** Object:  Schema [al]    Script Date: 10/11/2024 22.36.28 ******/
DROP SCHEMA IF EXISTS [al]
GO
/****** Object:  Schema [al]    Script Date: 10/11/2024 22.36.28 ******/
CREATE SCHEMA [al]
GO
/****** Object:  Table [al].[Course]    Script Date: 10/11/2024 22.36.28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [al].[Course](
	[CourseId] [int] IDENTITY(1,1) NOT NULL,
	[CourseName] [varchar](255) NOT NULL,
	[CourseRating] [decimal](3, 1) NOT NULL,
	[Slope] [int] NOT NULL,
	[Par] [int] NOT NULL,
 CONSTRAINT [PK__Course] PRIMARY KEY CLUSTERED 
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [al].[Match]    Script Date: 10/11/2024 22.36.28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [al].[Match](
	[MatchId] [int] IDENTITY(1,1) NOT NULL,
	[CourseId] [int] NOT NULL,
	[TournamentId] [int] NOT NULL,
	[MatchDate] [date] NOT NULL,
 CONSTRAINT [PK__Match] PRIMARY KEY CLUSTERED 
(
	[MatchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [al].[Tournament]    Script Date: 10/11/2024 22.36.28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [al].[Tournament](
	[TournamentId] [int] IDENTITY(1,1) NOT NULL,
	[TournamentName] [varchar](255) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK__Tourname] PRIMARY KEY CLUSTERED 
(
	[TournamentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [al].[vMatch]    Script Date: 10/11/2024 22.36.28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE view [al].[vMatch] as 
SELECT t.TournamentId, t.TournamentName, t.Active, m.MatchId, m.MatchDate, m.CourseId,
	c.CourseName, c.CourseRating, c.Slope, c.Par
FROM	al.Match AS m 
			INNER JOIN al.Course AS c 
				ON m.CourseId = c.CourseId 
			INNER JOIN al.Tournament AS t 
				ON m.TournamentId = t.TournamentId
GO
/****** Object:  Table [al].[Player]    Script Date: 10/11/2024 22.36.28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [al].[Player](
	[PlayerId] [int] IDENTITY(1,1) NOT NULL,
	[PlayerName] [varchar](50) NULL,
	[HcpIndex] [decimal](3, 1) NULL,
	[Email] [varchar](100) NULL,
 CONSTRAINT [PK__Player] PRIMARY KEY CLUSTERED 
(
	[PlayerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [al].[Result]    Script Date: 10/11/2024 22.36.28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [al].[Result](
	[ResultId] [int] IDENTITY(1,1) NOT NULL,
	[PlayerId] [int] NOT NULL,
	[MatchId] [int] NOT NULL,
	[Hcp] [int] NOT NULL,
	[Score] [int] NOT NULL,
	[Birdies] [int] NOT NULL,
	[Par3] [int] NOT NULL,
 CONSTRAINT [PK__Result] PRIMARY KEY CLUSTERED 
(
	[ResultId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [al].[vResult]    Script Date: 10/11/2024 22.36.28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [al].[vResult]
AS
SELECT  r.ResultId, t.TournamentId, t.TournamentName, t.Active, m.MatchId, m.MatchDate, m.CourseId, c.CourseName, 
		c.CourseRating, c.Slope, c.Par, r.Hcp, r.Score, r.Birdies, r.Par3, r.PlayerId, p.PlayerName, p.HcpIndex
FROM            [al].Result AS r INNER JOIN
                         [al].Match AS m ON r.MatchId = m.MatchId INNER JOIN
                         [al].Player AS p ON r.PlayerId = p.PlayerId INNER JOIN
                         [al].Course AS c ON m.CourseId = c.CourseId RIGHT OUTER JOIN
                         [al].Tournament AS t ON m.TournamentId = t.TournamentId
WHERE t.Active = 1
GO
/****** Object:  View [al].[vPlayersForMatch]    Script Date: 10/11/2024 22.36.28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE view [al].[vPlayersForMatch] as 
with p as (
SELECT 
    p.PlayerId, p.PlayerName, MatchId, cast(((p.HcpIndex * m.Slope / 113) + m.CourseRating - m.Par) as int) AS Hcp
FROM 
    al.vMatch as m, Player as p
)
select p.PlayerId, PlayerName, p.Hcp, p.MatchId, r.ResultId
FROM p
LEFT JOIN al.Result as r
    ON r.PlayerId = p.PlayerId AND p.MatchId = r.MatchId
--where r.ResultId is null

GO
SET IDENTITY_INSERT [al].[Course] ON 

INSERT [al].[Course] ([CourseId], [CourseName], [CourseRating], [Slope], [Par]) VALUES (1, N'Sletten/Parken', CAST(74.9 AS Decimal(3, 1)), 130, 73)
INSERT [al].[Course] ([CourseId], [CourseName], [CourseRating], [Slope], [Par]) VALUES (2, N'Skoven/Sletten', CAST(71.3 AS Decimal(3, 1)), 125, 72)
SET IDENTITY_INSERT [al].[Course] OFF
GO
SET IDENTITY_INSERT [al].[Match] ON 

INSERT [al].[Match] ([MatchId], [CourseId], [TournamentId], [MatchDate]) VALUES (5, 1, 1, CAST(N'2024-11-23' AS Date))
INSERT [al].[Match] ([MatchId], [CourseId], [TournamentId], [MatchDate]) VALUES (6, 2, 1, CAST(N'2024-10-29' AS Date))
SET IDENTITY_INSERT [al].[Match] OFF
GO
SET IDENTITY_INSERT [al].[Player] ON 

INSERT [al].[Player] ([PlayerId], [PlayerName], [HcpIndex], [Email]) VALUES (1, N'Madsen', CAST(12.2 AS Decimal(3, 1)), NULL)
INSERT [al].[Player] ([PlayerId], [PlayerName], [HcpIndex], [Email]) VALUES (2, N'Tiger', CAST(13.8 AS Decimal(3, 1)), NULL)
SET IDENTITY_INSERT [al].[Player] OFF
GO
SET IDENTITY_INSERT [al].[Result] ON 

INSERT [al].[Result] ([ResultId], [PlayerId], [MatchId], [Hcp], [Score], [Birdies], [Par3]) VALUES (7, 2, 5, 14, 89, 1, 14)
SET IDENTITY_INSERT [al].[Result] OFF
GO
SET IDENTITY_INSERT [al].[Tournament] ON 

INSERT [al].[Tournament] ([TournamentId], [TournamentName], [Active]) VALUES (1, N'Vejle VGC Tour2', 1)
INSERT [al].[Tournament] ([TournamentId], [TournamentName], [Active]) VALUES (5, N'Alicant 2024', 0)
SET IDENTITY_INSERT [al].[Tournament] OFF
GO
/****** Object:  Index [UNC_Match_MatchDate_CourseId]    Script Date: 10/11/2024 22.36.28 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UNC_Match_MatchDate_CourseId] ON [al].[Match]
(
	[MatchDate] ASC,
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [al].[Course] ADD  CONSTRAINT [DF_Course_Par]  DEFAULT ((72)) FOR [Par]
GO
ALTER TABLE [al].[Match] ADD  CONSTRAINT [DF_Match_MatchDate]  DEFAULT (sysdatetime()) FOR [MatchDate]
GO
ALTER TABLE [al].[Result] ADD  CONSTRAINT [DF_Result_Hcp]  DEFAULT ((54)) FOR [Hcp]
GO
ALTER TABLE [al].[Result] ADD  CONSTRAINT [DF_Result_Score]  DEFAULT ((100)) FOR [Score]
GO
ALTER TABLE [al].[Result] ADD  CONSTRAINT [DF_Result_Birdies]  DEFAULT ((0)) FOR [Birdies]
GO
ALTER TABLE [al].[Result] ADD  CONSTRAINT [DF_Result_Par3]  DEFAULT ((20)) FOR [Par3]
GO
ALTER TABLE [al].[Tournament] ADD  CONSTRAINT [DF_Tournament_Active]  DEFAULT ((0)) FOR [Active]
GO
ALTER TABLE [al].[Match]  WITH CHECK ADD  CONSTRAINT [FK_Match_Course] FOREIGN KEY([CourseId])
REFERENCES [al].[Course] ([CourseId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [al].[Match] CHECK CONSTRAINT [FK_Match_Course]
GO
ALTER TABLE [al].[Match]  WITH CHECK ADD  CONSTRAINT [FK_Match_Tournament] FOREIGN KEY([TournamentId])
REFERENCES [al].[Tournament] ([TournamentId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [al].[Match] CHECK CONSTRAINT [FK_Match_Tournament]
GO
ALTER TABLE [al].[Result]  WITH CHECK ADD  CONSTRAINT [FK_Result_Match] FOREIGN KEY([MatchId])
REFERENCES [al].[Match] ([MatchId])
ON DELETE CASCADE
GO
ALTER TABLE [al].[Result] CHECK CONSTRAINT [FK_Result_Match]
GO
ALTER TABLE [al].[Result]  WITH CHECK ADD  CONSTRAINT [FK_Result_Player] FOREIGN KEY([PlayerId])
REFERENCES [al].[Player] ([PlayerId])
ON DELETE CASCADE
GO
ALTER TABLE [al].[Result] CHECK CONSTRAINT [FK_Result_Player]
GO
/****** Object:  StoredProcedure [al].[UpsertCourse]    Script Date: 10/11/2024 22.36.28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [al].[UpsertCourse]
    @CourseId INT = NULL, -- Nullable, since this might be a new course
    @Par INT,
    @CourseName VARCHAR(255),
    @CourseRating DECIMAL(3, 1),
    @Slope INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Declare a table variable to hold the output (the modified record)
    DECLARE @ModifiedRecords TABLE
    (
        CourseId INT,
        Par INT,
		CourseName VARCHAR(255),
        CourseRating DECIMAL(3, 1),
        Slope INT
    );

    -- Merge operation between Source (S) and Target (T) tables
    MERGE INTO [dbo].[Course] AS T
    USING (SELECT @CourseId AS CourseId, @Par as Par, @CourseName AS CourseName, @CourseRating AS CourseRating, @Slope AS Slope) AS S
    ON T.CourseId = S.CourseId
    WHEN MATCHED THEN
        UPDATE SET 
            T.CourseName = S.CourseName,
            T.Par = S.Par,
            T.CourseRating = S.CourseRating,
            T.Slope = S.Slope
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (CourseName, Par, CourseRating, Slope)
        VALUES (S.CourseName, S.Par, S.CourseRating, S.Slope)
    OUTPUT inserted.CourseId, inserted.Par, inserted.CourseName, inserted.CourseRating, inserted.Slope
    INTO @ModifiedRecords;

    -- Return the modified record(s)
    SELECT * FROM @ModifiedRecords;
END;
GO
/****** Object:  StoredProcedure [al].[UpsertMatch]    Script Date: 10/11/2024 22.36.28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [al].[UpsertMatch]
    @MatchId INT OUTPUT,
    @MatchDate DATETIME2(0),
    @CourseId INT,
    @TournamentId INT,
    @SecondRound bit
AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @NewMatchId INT;

    MERGE INTO [dbo].[Match] AS T
    USING (
		SELECT @MatchId AS MatchId, @MatchDate AS MatchDate, @SecondRound as SecondRound,
			@CourseId AS CourseId, @TournamentId AS TournamentId
	) AS S
    ON (T.MatchId = S.MatchId)
    WHEN MATCHED THEN
        UPDATE SET 
            MatchDate = S.MatchDate,
            CourseId = S.CourseId,
            TournamentId = S.TournamentId,
			SecondRound = S.SecondRound
    WHEN NOT MATCHED THEN
        INSERT (MatchDate, CourseId, TournamentId, SecondRound)
        VALUES (S.MatchDate, S.CourseId, S.TournamentId, S.SecondRound)
        OUTPUT inserted.*;

END
GO
/****** Object:  StoredProcedure [al].[UpsertPlayer]    Script Date: 10/11/2024 22.36.28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [al].[UpsertPlayer]
    @PlayerId INT,
    @PlayerName VARCHAR(50),
    @HcpIndex DECIMAL(3, 1)
AS
BEGIN
    SET NOCOUNT ON;

    -- Declare a table variable to hold the output
    DECLARE @ModifiedRecords TABLE
    (
        PlayerId INT,
        PlayerName VARCHAR(50),
        HcpIndex DECIMAL(3, 1)
    );

    -- Merge operation between Source (S) and Target (T) tables
    MERGE INTO [dbo].[Player] AS T
    USING (SELECT @PlayerId AS PlayerId, @PlayerName AS PlayerName, @HcpIndex AS HcpIndex) AS S
    ON T.PlayerId = S.PlayerId
    WHEN MATCHED THEN
        UPDATE SET 
            T.PlayerName = S.PlayerName,
            T.HcpIndex = S.HcpIndex
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (PlayerName, HcpIndex)
        VALUES (S.PlayerName, S.HcpIndex)
    OUTPUT inserted.PlayerId, inserted.PlayerName, inserted.HcpIndex
    INTO @ModifiedRecords;

    -- Return the modified record(s)
    SELECT * FROM @ModifiedRecords;
END;
GO
/****** Object:  StoredProcedure [al].[UpsertResult]    Script Date: 10/11/2024 22.36.28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [al].[UpsertResult]
    @PlayerId INT,
    @MatchId INT,
    @Hcp INT,
    @Score INT,
    @Birdies INT,
    @Par3 INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Declare a table variable to hold the output (the modified record)
    DECLARE @ModifiedRecords TABLE
    (
        ResultId INT,
        PlayerId INT,
        MatchId INT,
        Hcp INT,
        Score INT,
        Birdies INT,
        Par3 INT
    );

    -- Merge operation between Source (S) and Target (T) tables
    MERGE INTO [dbo].[Result] AS T
    USING (
		SELECT	@PlayerId AS PlayerId, 
				@MatchId AS MatchId, 
				@Hcp AS Hcp, 
				@Score AS Score, 
				@Birdies AS Birdies, 
				@Par3 AS Par3) AS S
    ON T.PlayerId = S.PlayerId AND T.MatchId = S.MatchId
    WHEN MATCHED THEN
        UPDATE SET 
            T.Hcp = S.Hcp,
            T.Score = S.Score,
            T.Birdies = S.Birdies,
            T.Par3 = S.Par3
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (PlayerId, MatchId, Hcp, Score, Birdies, Par3)
        VALUES (S.PlayerId, S.MatchId, S.Hcp, S.Score, S.Birdies, S.Par3)
    OUTPUT inserted.ResultId, inserted.PlayerId, inserted.MatchId, inserted.Hcp, inserted.Score, inserted.Birdies, inserted.Par3
    INTO @ModifiedRecords;

    -- Return the modified record(s)
    SELECT * FROM @ModifiedRecords;
END;
GO
/****** Object:  StoredProcedure [al].[UpsertTournament]    Script Date: 10/11/2024 22.36.28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [al].[UpsertTournament]
    @TournamentId INT = NULL, -- Nullable, since for a new record, it might not be provided
    @TournamentName VARCHAR(255),
    @Active BIT
AS
BEGIN
    SET NOCOUNT ON;

    -- Declare a table variable to hold the output (the modified record)
    DECLARE @ModifiedRecords TABLE
    (
        TournamentId INT,
        TournamentName VARCHAR(255),
        Active BIT
    );

    -- Merge operation between Source (S) and Target (T) tables
    MERGE INTO [dbo].[Tournament] AS T
    USING (SELECT @TournamentId AS TournamentId, @TournamentName AS TournamentName, @Active AS Active) AS S
    ON T.TournamentId = S.TournamentId
    WHEN MATCHED THEN
        UPDATE SET 
            T.TournamentName = S.TournamentName,
            T.Active = S.Active
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (TournamentName, Active)
        VALUES (S.TournamentName, S.Active)
    OUTPUT inserted.TournamentId, inserted.TournamentName, inserted.Active
    INTO @ModifiedRecords;

    -- Return the modified record(s)
    SELECT * FROM @ModifiedRecords;
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[48] 4[13] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "c"
            Begin Extent = 
               Top = 136
               Left = 418
               Bottom = 265
               Right = 588
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "m"
            Begin Extent = 
               Top = 5
               Left = 203
               Bottom = 135
               Right = 373
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 20
               Left = 666
               Bottom = 133
               Right = 836
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pm"
            Begin Extent = 
               Top = 4
               Left = 462
               Bottom = 134
               Right = 632
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "t"
            Begin Extent = 
               Top = 136
               Left = 0
               Bottom = 249
               Right = 186
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output' , @level0type=N'SCHEMA',@level0name=N'al', @level1type=N'VIEW',@level1name=N'vResult'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N' = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'al', @level1type=N'VIEW',@level1name=N'vResult'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'al', @level1type=N'VIEW',@level1name=N'vResult'
GO
