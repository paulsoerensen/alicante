CREATE PROCEDURE [dbo].[UpsertPlayerMatch]
    @PlayerId INT,
    @MatchId INT,
    @Hcp INT,
    @Score INT,
    @Birdies INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Declare a table variable to hold the output (the modified record)
    DECLARE @ModifiedRecords TABLE
    (
        PlayerId INT,
        MatchId INT,
        Hcp INT,
        Score INT,
        Birdies INT
    );

    -- Merge operation between Source (S) and Target (T) tables
    MERGE INTO [dbo].[PlayerMatch] AS T
    USING (SELECT @PlayerId AS PlayerId, @MatchId AS MatchId, @Hcp AS Hcp, @Score AS Score, @Birdies AS Birdies) AS S
    ON T.PlayerId = S.PlayerId AND T.MatchId = S.MatchId
    WHEN MATCHED THEN
        UPDATE SET 
            T.Hcp = S.Hcp,
            T.Score = S.Score,
            T.Birdies = S.Birdies
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (PlayerId, MatchId, Hcp, Score, Birdies)
        VALUES (S.PlayerId, S.MatchId, S.Hcp, S.Score, S.Birdies)
    OUTPUT inserted.PlayerId, inserted.MatchId, inserted.Hcp, inserted.Score, inserted.Birdies
    INTO @ModifiedRecords;

    -- Return the modified record(s)
    SELECT * FROM @ModifiedRecords;
END;

GO

