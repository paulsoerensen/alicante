using Alicante.Client.Models;
using Alicante.Data;
using Microsoft.AspNetCore.Mvc;


namespace Alicante.Controllers;


[Route("api/[controller]")]
[ApiController]
public class PlayerController : BaseController
{
    public PlayerController(IRepository repository) : base(repository)
    {
    }

    // GET: api/Match/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponseModel>> GetPlayer(int id)
    {
        PlayerModel model = await _repo.GetPlayer(id);
        if (model == null)
        {
            return Ok(new BaseResponseModel { Success = false, ErrorMessage = "Not found" });
        }
        return Ok(new BaseResponseModel { Success = true, Data = model });
    }


    // GET: api/Player
    [HttpGet]
    public async Task<ActionResult<BaseResponseModel>> GetPlayers()
    {
        var models = await _repo.FindAllPlayer();
        return Ok(new BaseResponseModel { Success = true, Data = models });
    }

    // GET: api/Player/match/matchId
    [HttpGet("match/{matchId}")]
    public async Task<ActionResult<BaseResponseModel>> GetPlayersForMatch(int matchId)
    {
        var models = await _repo.GetPlayersForMatch(matchId);
        return Ok(new BaseResponseModel { Success = true, Data = models });
    }

    // POST: api/player
    [HttpPost]
    public async Task<ActionResult<BaseResponseModel>> UpsertPlayer(PlayerModel model)
    {
        if (model == null)
        {
            return Ok(new BaseResponseModel { Success = false, ErrorMessage = "No data" });
        }
        try
        {
            var newModel = await _repo.UpsertPlayer(model);
            return Ok(new BaseResponseModel { Success = true, Data = newModel });
        }
        catch (Exception e)
        {
            return Ok(new BaseResponseModel { Success = false, ErrorMessage = e.Message });
        }
    }

    // DELETE: api/Match/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlayer(int id)
    {
        try
        {
            int i = await _repo.DeletePlayer(id);
            return Ok(new BaseResponseModel { Success = true });
        }
        catch (Exception e)
        {
            return Ok(new BaseResponseModel { Success = false, ErrorMessage = e.Message });
        }
    }
}