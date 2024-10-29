using Alicante.Client.Models;

namespace Alicante.Data;


public interface IRepository
{
    Task<CourseModel> GetCourse(int courseId);
    Task<IEnumerable<CourseModel>> FindAllCourse();
    Task<CourseModel> UpsertCourse(CourseModel model);
    Task<int> DeleteCourse(int courseId);

    Task<MatchModel> GetMatch(int matchId);
    Task<IEnumerable<MatchViewModel>> FindAllMatch();
    Task<MatchModel> UpsertMatch(MatchModel match);
    Task<int> DeleteMatch(int matchId);

    Task<PlayerModel> GetPlayer(int playerId);
    Task<IEnumerable<PlayerModel>> FindAllPlayer();
    Task<IEnumerable<PlayerViewModel>> GetPlayersForMatch(int matchId);
    Task<PlayerModel> UpsertPlayer(PlayerModel player);
    Task<int> DeletePlayer(int playerId);

    //Task<IEnumerable<ResultModel>> GetPlayerMatches();
    //Task<int> DeletePlayerMatch(int playerId);

    Task<TournamentModel> GetTournament(int tournamentId);
    Task<IEnumerable<TournamentModel>> GetTournaments();
    Task<TournamentModel> UpsertTournament(TournamentModel model);
    Task<int> DeleteTournament(int tournamentId);

    Task<ResultModel> GetResult(int id);
    Task<ResultModel> UpsertResult(ResultModel model);

    Task<IEnumerable<ResultViewModel>> GetResults();
    Task<IEnumerable<ResultViewModel>> GetResults(int matchId);
    Task<IEnumerable<ResultViewModel>> ResultFromMatch(int matchId);
}