

CREATE view [dbo].[vMatch] as 
SELECT t.TournamentId, t.TournamentName, t.Active, m.MatchId, m.MatchDate, m.CourseId,
	c.CourseName, c.CourseRating, c.Slope, c.Par
FROM	dbo.Match AS m 
			INNER JOIN dbo.Course AS c 
				ON m.CourseId = c.CourseId 
			INNER JOIN dbo.Tournament AS t 
				ON m.TournamentId = t.TournamentId
GO

