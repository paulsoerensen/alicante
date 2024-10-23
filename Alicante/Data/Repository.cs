using Microsoft.Data.SqlClient;
using System.Data;
using Alicante.Client.Models;
using Dapper;
using System.Data.Common;

namespace Alicante.Data
{
    public class Repository : IRepository
    {

        private readonly IConfiguration _configuration;

        public Repository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("AlicanteDB"));
        }

        #region Course

        private const string courseSelect = @"SELECT [CourseId],[CourseName],[CourseRating],[Slope] 
                                                FROM Course";

        public async Task<CourseModel> GetCourse(int courseId)
        {
            using (IDbConnection db = CreateConnection())
            {
                const string getQuery = $"{courseSelect} WHERE CourseId = @courseId";
                return await db.QuerySingleAsync<CourseModel>(getQuery, new { courseId });
            }
        }

        public async Task<IEnumerable<CourseModel>> FindAllCourse()
        {
            using (IDbConnection db = CreateConnection())
            {
                const string findAllQuery = courseSelect;
                var results = await db.QueryAsync<CourseModel>(findAllQuery);
                return results;
            }
        }

        public async Task<CourseModel> UpsertCourse(CourseModel model)
        {
            var sql = "EXEC UpsertCourse @CourseId, @Par, @courseName, @courseRating, @slope";
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleAsync<CourseModel>(sql, new
                {
                    model.CourseId,
                    model.Par,
                    model.CourseName,
                    model.CourseRating,
                    model.Slope
                });
                return result;
            }
        }

        public async Task<int> DeleteCourse(int courseId)
        {
            using (IDbConnection db = CreateConnection())
            {
                const string deleteQuery = "DELETE Course WHERE CourseId = @courseId";
                var rowsAffected = await db.ExecuteScalarAsync<int>(deleteQuery, new { courseId });
                return rowsAffected;
            }
        }

        #endregion Course

        #region Match

        public async Task<MatchModel> GetMatch(int matchId)
        {
            using (IDbConnection db = CreateConnection())
            {
                const string query = "SELECT TournamentId, MatchId, MatchDate, CourseId, CourseName, " +
                                        "CourseRating, Slope, Par, SecondRound " +
                                        "FROM dbo.vMatch " +
                                        "WHERE MatchId = @matchId";
                return await db.QuerySingleAsync<MatchModel>(query, new { matchId });
            }
        }

        public async Task<IEnumerable<MatchViewModel>> FindAllMatch()
        {
            using (IDbConnection db = CreateConnection())
            {
                const string query = @"SELECT TournamentId, TournamentName, Active, " +
                    "                   MatchId, MatchDate, CourseId, CourseName, " +
                                        "CourseRating, Slope, Par, SecondRound " +
                                        "FROM dbo.vMatch " +
                                        "where Active = 1";

                var results = await db.QueryAsync<MatchViewModel>(query);
                return results;
            }
        }
        public async Task<MatchModel> UpsertMatch(MatchModel match)
        {
            var sql = "EXEC UpsertMatch @MatchId, @MatchDate, @CourseId, @TournamentId, @SecondRound";
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleAsync<MatchModel>(sql, new
                {
                    match.MatchId,
                    match.MatchDate,
                    match.CourseId,
                    match.TournamentId,
                    match.SecondRound
                });
                return result;
            }
        }

        public async Task<int> DeleteMatch(int matchId)
        {
            using (IDbConnection db = CreateConnection())
            {
                const string deleteQuery = "DELETE Match WHERE MatchId = @matchId";
                var rowsAffected = await db.ExecuteScalarAsync<int>(deleteQuery, new { matchId });
                return rowsAffected;
            }
        }

        // WARNING: The Dapper code generation tool doesn't currently generate merge method(s).

        #endregion Match

        #region Player

        public async Task<PlayerModel> GetPlayer(int playerId)
        {
            using (IDbConnection db = CreateConnection())
            {
                const string getQuery = "SELECT * FROM Player WHERE PlayerId = @playerId";
                return await db.QuerySingleAsync<PlayerModel>(getQuery, new { playerId });
            }
        }

        public async Task<IEnumerable<PlayerModel>> FindAllPlayer()
        {
            using (IDbConnection db = CreateConnection())
            {
                const string findAllQuery = "SELECT * FROM Player";
                var results = await db.QueryAsync<PlayerModel>(findAllQuery);
                return results;
            }
        }

        public async Task<IEnumerable<PlayerViewModel>> GetPlayersForMatch(int matchId)
        {
            var sql = @"SELECT PlayerId, PlayerName, Hcp " +
                "FROM dbo.vPlayersForMatch " +
                "where MatchId = @matchId";

            using (IDbConnection db = CreateConnection())
            {
                return await db.QueryAsync<PlayerViewModel>(sql, new { matchId });
            }
        }

        public async Task<PlayerModel> UpsertPlayer(PlayerModel player)
        {
            var sql = "EXEC UpsertPlayer @PlayerId, @PlayerName, @HcpIndex";
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleAsync<PlayerModel>(sql, new
                {
                    player.PlayerId,
                    player.PlayerName,
                    player.HcpIndex
                });
                return result;
            }
        }

        public async Task<int> DeletePlayer(int playerId)
        {
            using (IDbConnection db = CreateConnection())
            {
                const string deleteQuery = "DELETE Player WHERE PlayerId = @playerId";
                var rowsAffected = await db.ExecuteScalarAsync<int>(deleteQuery, new { playerId });
                return rowsAffected;
            }
        }
        #endregion Player

        #region PlayerMatch

        public async Task<IEnumerable<ResultModel>> GetPlayerMatches()
        {
            using (IDbConnection db = CreateConnection())
            {
                const string findAllQuery = "SELECT * FROM PlayerMatch";
                var results = await db.QueryAsync<ResultModel>(findAllQuery);
                return results;
            }
        }

        public async Task<ResultModel> UpsertPlayerMatch(ResultModel model)
        {
            var sql = "EXEC UpsertPlayerMatch @playerId, @matchId, @hcp, @score, @birdies";
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleAsync<ResultModel>(sql, new
                {
                    model.PlayerId,
                    model.MatchId,
                    model.Hcp,
                    model.Score,
                    model.Birdies
                });
                return result;
            }
        }
        #endregion PlayerMatch    

        #region Tournament

        public async Task<TournamentModel> GetTournament(int tournamentId)
        {
            using (IDbConnection db = CreateConnection())
            {
                const string getQuery = "SELECT TournamentId, TournamentName FROM Tournament WHERE TournamentId = @tournamentId";
                return await db.QuerySingleAsync<TournamentModel>(getQuery, new { tournamentId });
            }
        }

        public async Task<IEnumerable<TournamentModel>> GetTournaments()
        {
            using (IDbConnection db = CreateConnection())
            {
                const string findAllQuery = "SELECT TournamentId, TournamentName FROM Tournament";
                var results = await db.QueryAsync<TournamentModel>(findAllQuery);
                return results;
            }
        }

        public async Task<TournamentModel> UpsertTournament(TournamentModel model)
        {
            var sql = "EXEC UpsertTournament @TournamentId, @TournamentName, @Active";
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleAsync<TournamentModel>(sql, new
                {
                    model.TournamentId,
                    model.TournamentName,
                    model.Active
                });
                return result;
            }
        }

        public async Task<int> DeleteTournament(int tournamentId)
        {
            using (IDbConnection db = CreateConnection())
            {
                const string deleteQuery = "DELETE Player WHERE TournamentId = @tournamentId";
                var rowsAffected = await db.ExecuteScalarAsync<int>(deleteQuery, new { tournamentId });
                return rowsAffected;
            }
        }
        #endregion Tournament

        #region Result
        public async Task<ResultModel> GetResult(int id)
        {
            using (IDbConnection db = CreateConnection())
            {
                const string getQuery = "SELECT ResultId,PlayerId,MatchId,Hcp,Score,Birdies FROM Result WHERE ResultId = @id";
                return await db.QuerySingleAsync<ResultModel>(getQuery, new { id });
            }
        }
        
        public async Task<ResultModel> UpsertResult(ResultModel model)
        {
            var sql = "EXEC UpsertResult @PlayerId, @MatchId, @Hcp, @Score, @Birdies, @Par3";
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleAsync<ResultModel>(sql, new
                {
                    model.PlayerId,
                    model.MatchId,
                    model.Hcp,
                    model.Score,
                    model.Birdies,
                    model.Par3
                });
                return result;
            }
        }

        public async Task<IEnumerable<ResultViewModel>> GetResults()
        {
            var sql = @"SELECT 
                TournamentId, TournamentName, Active, MatchId, MatchDate, 
                CourseId, CourseName, CourseRating, Slope, Hcp, Score, 
                Birdies, PlayerId, PlayerName, HcpIndex
            FROM dbo.vResult"
            ;
            using (IDbConnection db = CreateConnection())
            {
                return await db.QueryAsync<ResultViewModel>(sql);
            }
        }

        public async Task<IEnumerable<ResultViewModel>> GetResults(int matchId)
        {
            var sql = @"SELECT 
                ResultId, TournamentId, TournamentName, Active, MatchId, MatchDate, 
                CourseId, CourseName, CourseRating, Slope, Hcp, Score, 
                Birdies, PlayerId, PlayerName, HcpIndex
                FROM dbo.vResult
                WHERE MatchId = @matchId"
            ;
            using (IDbConnection db = CreateConnection())
            {
                return await db.QueryAsync<ResultViewModel>(sql, new { matchId });
            }
        }

        public async Task<IEnumerable<ResultViewModel>> ResultFromMatch(int matchId)
        {
            var sql = @"SELECT 
                TournamentId, TournamentName, Active, MatchId, MatchDate, 
                CourseId, CourseName, CourseRating, Slope, Hcp, Score, 
                Birdies, PlayerId, Name AS PlayerName, HcpIndex
            FROM dbo.vPlayerMatch WHERE MatchId = @matchId"
            ;
            using (IDbConnection db = CreateConnection())
            {
                return await db.QueryAsync<ResultViewModel>(sql, new { matchId });
            }
        }

        #endregion
    }
}
