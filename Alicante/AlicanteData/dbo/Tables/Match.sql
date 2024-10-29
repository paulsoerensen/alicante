CREATE TABLE [dbo].[Match] (
    [MatchId]      INT  IDENTITY (1, 1) NOT NULL,
    [CourseId]     INT  NOT NULL,
    [TournamentId] INT  NOT NULL,
    [MatchDate]    DATE CONSTRAINT [DF_Match_MatchDate] DEFAULT (sysdatetime()) NOT NULL,
    [SecondRound]  BIT  CONSTRAINT [DF_Match_SecondRound] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK__Match] PRIMARY KEY CLUSTERED ([MatchId] ASC),
    CONSTRAINT [FK_Match_Course] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Course] ([CourseId]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_Match_Tournament] FOREIGN KEY ([TournamentId]) REFERENCES [dbo].[Tournament] ([TournamentId]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO

CREATE UNIQUE NONCLUSTERED INDEX [UNC_Match_MatchDate_CourseId_SecondRound]
    ON [dbo].[Match]([MatchDate] ASC, [CourseId] ASC, [SecondRound] ASC);


GO

