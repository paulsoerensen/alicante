
CREATE view [dbo].[vPlayersForMatch] as 
with p as (
SELECT 
    p.PlayerId, p.PlayerName, MatchId, cast(((p.HcpIndex * m.Slope / 113) + m.CourseRating - m.Par) as int) AS Hcp
FROM 
    dbo.vMatch as m, Player as p
)
select p.PlayerId, PlayerName, p.Hcp, p.MatchId, r.ResultId
FROM p
LEFT JOIN Result as r
    ON r.PlayerId = p.PlayerId AND p.MatchId = r.MatchId
--where r.ResultId is null
GO

