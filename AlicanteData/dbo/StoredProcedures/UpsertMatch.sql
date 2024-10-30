CREATE PROCEDURE [dbo].[UpsertMatch]
    @MatchId INT,
    @MatchDate DATETIME2(0),
    @CourseId INT,
    @TournamentId INT,
    @SecondRound bit
AS
BEGIN
    SET NOCOUNT ON;

    MERGE INTO [dbo].[Match] AS target
    USING (
		SELECT @MatchId AS MatchId, @MatchDate AS MatchDate, @SecondRound as SecondRound,
			@CourseId AS CourseId, @TournamentId AS TournamentId
	) AS S
    ON (target.MatchId = S.MatchId)
    WHEN MATCHED THEN
        UPDATE SET 
            MatchDate = S.MatchDate,
            CourseId = S.CourseId,
            TournamentId = S.TournamentId,
			SecondRound = S.SecondRound
    WHEN NOT MATCHED THEN
        INSERT (MatchDate, CourseId, TournamentId, SecondRound)
        VALUES (S.MatchDate, S.CourseId, S.TournamentId, S.SecondRound);
END

GO

