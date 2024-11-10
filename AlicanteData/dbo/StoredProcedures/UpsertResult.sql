CREATE PROCEDURE [dbo].[UpsertResult]
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

