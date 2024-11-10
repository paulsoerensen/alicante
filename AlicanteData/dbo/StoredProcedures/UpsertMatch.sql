CREATE PROCEDURE [dbo].[UpsertMatch]
    @MatchId INT OUTPUT,
    @MatchDate DATETIME2(0),
    @CourseId INT,
    @TournamentId INT,
    @SecondRound bit
AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @NewMatchId INT;

    MERGE INTO [dbo].[Match] AS T
    USING (
		SELECT @MatchId AS MatchId, @MatchDate AS MatchDate, @SecondRound as SecondRound,
			@CourseId AS CourseId, @TournamentId AS TournamentId
	) AS S
    ON (T.MatchId = S.MatchId)
    WHEN MATCHED THEN
        UPDATE SET 
            MatchDate = S.MatchDate,
            CourseId = S.CourseId,
            TournamentId = S.TournamentId,
			SecondRound = S.SecondRound
    WHEN NOT MATCHED THEN
        INSERT (MatchDate, CourseId, TournamentId, SecondRound)
        VALUES (S.MatchDate, S.CourseId, S.TournamentId, S.SecondRound)
        OUTPUT inserted.*;

END
GO

