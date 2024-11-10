CREATE TABLE [dbo].[Result] (
    [ResultId] INT IDENTITY (1, 1) NOT NULL,
    [PlayerId] INT NOT NULL,
    [MatchId]  INT NOT NULL,
    [Hcp]      INT NOT NULL,
    [Score]    INT NOT NULL,
    [Birdies]  INT NOT NULL,
    [Par3]     INT NOT NULL
);
GO

ALTER TABLE [dbo].[Result]
    ADD CONSTRAINT [DF_Result_Par3] DEFAULT ((20)) FOR [Par3];
GO

ALTER TABLE [dbo].[Result]
    ADD CONSTRAINT [DF_Result_Birdies] DEFAULT ((0)) FOR [Birdies];
GO

ALTER TABLE [dbo].[Result]
    ADD CONSTRAINT [DF_Result_Score] DEFAULT ((100)) FOR [Score];
GO

ALTER TABLE [dbo].[Result]
    ADD CONSTRAINT [DF_Result_Hcp] DEFAULT ((54)) FOR [Hcp];
GO

ALTER TABLE [dbo].[Result]
    ADD CONSTRAINT [PK__Result] PRIMARY KEY CLUSTERED ([ResultId] ASC);
GO

ALTER TABLE [dbo].[Result]
    ADD CONSTRAINT [FK_Result_Player] FOREIGN KEY ([PlayerId]) REFERENCES [dbo].[Player] ([PlayerId]) ON DELETE CASCADE;
GO

ALTER TABLE [dbo].[Result]
    ADD CONSTRAINT [FK_Result_Match] FOREIGN KEY ([MatchId]) REFERENCES [dbo].[Match] ([MatchId]) ON DELETE CASCADE;
GO

