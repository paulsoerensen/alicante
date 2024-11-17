using Alicante.Client.Models;
using Alicante.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;


namespace Alicante.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ResultController : BaseController
{
    public ResultController(IRepository repository,
                            IMapper mapper) : base(repository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    // GET: api/Result/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponseModel>> GetResult(int id)
    {
        ResultModel model = await _repo.GetResult(id);
        if (model == null)
        {
            return Ok(new BaseResponseModel { Success = false, ErrorMessage = "Not found" });
        }
        return Ok(new BaseResponseModel { Success = true, Data = model });
    }


    // GET: api/result/match/matchId
    [HttpGet("match/{matchId}")]
    public async Task<ActionResult<BaseResponseModel>> GetResultList(int matchId)
    {
        try
        {
            var models = await _repo.GetResults(matchId);
            return Ok(new BaseResponseModel { Success = true, Data = models });
        }
        catch (Exception e)
        {
            return Ok(new BaseResponseModel { Success = false, ErrorMessage = e.Message});
        }
    }

    // GET: api/result/match/score/matchId
    [HttpGet("score/{tournamentId}")]
    public async Task<ActionResult<BaseResponseModel>> TournamentResult(int tournamentId)
    {
        try
        {
            IEnumerable<MatchResultViewModel> models = await _repo.TournamentResult(tournamentId);

            return Ok(new BaseResponseModel { Success = true, Data = models });
        }
        catch (Exception e)
        {
            return Ok(new BaseResponseModel { Success = false, ErrorMessage = e.Message });
        }
    }

    // POST: api/result
    [HttpPost]
    public async Task<ActionResult<BaseResponseModel>> UpsertResult(ResultModel model)
    {
        if (model == null)
        {
            return Ok(new BaseResponseModel { Success = false, ErrorMessage = "No data" });
        }        
        try
        {
            var newModel = await _repo.UpsertResult(model);
            return Ok(new BaseResponseModel { Success = true, Data = newModel });
        }
        catch (Exception e)
        {
            return Ok(new BaseResponseModel { Success = false, ErrorMessage = e.Message });
        }
    }

    // DELETE: api/Match/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteResult(int id)
    {
        try 
        { 
            int i = await _repo.DeleteResult(id);
            return Ok(new BaseResponseModel { Success = true });
        }
        catch (Exception e)
        {
            return Ok(new BaseResponseModel { Success = false, ErrorMessage = e.Message});
        }
    }
}