CREATE PROCEDURE [dbo].[UpsertTournament]
    @TournamentId INT = NULL, -- Nullable, since for a new record, it might not be provided
    @TournamentName VARCHAR(255),
    @Active BIT
AS
BEGIN
    SET NOCOUNT ON;

    -- Declare a table variable to hold the output (the modified record)
    DECLARE @ModifiedRecords TABLE
    (
        TournamentId INT,
        TournamentName VARCHAR(255),
        Active BIT
    );

    -- Merge operation between Source (S) and Target (T) tables
    MERGE INTO [dbo].[Tournament] AS T
    USING (SELECT @TournamentId AS TournamentId, @TournamentName AS TournamentName, @Active AS Active) AS S
    ON T.TournamentId = S.TournamentId
    WHEN MATCHED THEN
        UPDATE SET 
            T.TournamentName = S.TournamentName,
            T.Active = S.Active
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (TournamentName, Active)
        VALUES (S.TournamentName, S.Active)
    OUTPUT inserted.TournamentId, inserted.TournamentName, inserted.Active
    INTO @ModifiedRecords;

    -- Return the modified record(s)
    SELECT * FROM @ModifiedRecords;
END

GO

