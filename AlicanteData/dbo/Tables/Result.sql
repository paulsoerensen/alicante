CREATE TABLE [dbo].[Result] (
    [ResultId] INT NOT NULL,
    [PlayerId] INT NOT NULL,
    [MatchId]  INT NOT NULL,
    [Hcp]      INT CONSTRAINT [DF_Result_Hcp] DEFAULT ((54)) NOT NULL,
    [Score]    INT CONSTRAINT [DF_Result_Score] DEFAULT ((100)) NOT NULL,
    [Birdies]  INT CONSTRAINT [DF_Result_Birdies] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK__Result] PRIMARY KEY CLUSTERED ([ResultId] ASC),
    CONSTRAINT [FK_Result_Match] FOREIGN KEY ([MatchId]) REFERENCES [dbo].[Match] ([MatchId]),
    CONSTRAINT [FK_Result_Player] FOREIGN KEY ([PlayerId]) REFERENCES [dbo].[Player] ([PlayerId])
);


GO

