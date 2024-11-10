CREATE PROCEDURE [dbo].[UpsertPlayer]
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

