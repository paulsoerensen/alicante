CREATE TABLE [dbo].[Tournament] (
    [TournamentId]   INT           IDENTITY (1, 1) NOT NULL,
    [TournamentName] VARCHAR (255) NOT NULL,
    [Active]         BIT           NOT NULL
);
GO

ALTER TABLE [dbo].[Tournament]
    ADD CONSTRAINT [DF_Tournament_Active] DEFAULT ((0)) FOR [Active];
GO

ALTER TABLE [dbo].[Tournament]
    ADD CONSTRAINT [PK__Tourname] PRIMARY KEY CLUSTERED ([TournamentId] ASC);
GO

