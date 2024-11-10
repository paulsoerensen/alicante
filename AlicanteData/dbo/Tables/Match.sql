CREATE TABLE [dbo].[Match] (
    [MatchId]      INT  IDENTITY (1, 1) NOT NULL,
    [CourseId]     INT  NOT NULL,
    [TournamentId] INT  NOT NULL,
    [MatchDate]    DATE NOT NULL
);
GO

ALTER TABLE [dbo].[Match]
    ADD CONSTRAINT [FK_Match_Course] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Course] ([CourseId]) ON DELETE CASCADE ON UPDATE CASCADE;
GO

ALTER TABLE [dbo].[Match]
    ADD CONSTRAINT [FK_Match_Tournament] FOREIGN KEY ([TournamentId]) REFERENCES [dbo].[Tournament] ([TournamentId]) ON DELETE CASCADE ON UPDATE CASCADE;
GO

ALTER TABLE [dbo].[Match]
    ADD CONSTRAINT [DF_Match_MatchDate] DEFAULT (sysdatetime()) FOR [MatchDate];
GO

ALTER TABLE [dbo].[Match]
    ADD CONSTRAINT [PK__Match] PRIMARY KEY CLUSTERED ([MatchId] ASC);
GO

CREATE UNIQUE NONCLUSTERED INDEX [UNC_Match_MatchDate_CourseId]
    ON [dbo].[Match]([MatchDate] ASC, [CourseId] ASC);
GO

