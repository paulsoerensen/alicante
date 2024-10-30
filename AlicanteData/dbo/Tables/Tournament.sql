CREATE TABLE [dbo].[Tournament] (
    [TournamentId]   INT           IDENTITY (1, 1) NOT NULL,
    [TournamentName] VARCHAR (255) NOT NULL,
    [Active]         BIT           CONSTRAINT [DF_Tournament_Active] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK__Tourname] PRIMARY KEY CLUSTERED ([TournamentId] ASC)
);


GO

