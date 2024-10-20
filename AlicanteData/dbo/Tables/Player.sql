CREATE TABLE [dbo].[Player] (
    [PlayerId]   INT            IDENTITY (1, 1) NOT NULL,
    [PlayerName] VARCHAR (50)   NULL,
    [HcpIndex]   DECIMAL (3, 1) NULL,
    CONSTRAINT [PK__Player] PRIMARY KEY CLUSTERED ([PlayerId] ASC)
);


GO

