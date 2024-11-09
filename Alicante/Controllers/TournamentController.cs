using Alicante.Client.Models;
using Alicante.Data;
using Microsoft.AspNetCore.Mvc;

namespace Alicante.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TournamentController : BaseController
{
    public TournamentController(IRepository repository) : base(repository)
    {
    }

    // GET: api/Tournament
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BaseResponseModel>>> GetTournaments()
    {
        var model = await _repo.GetTournaments();
        return Ok(new BaseResponseModel { Success = true, Data = model });
    }

    // GET: api/Tournament/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponseModel>> GetTournament(int id)
    {
        var model = await _repo.GetTournament(id);
        if (model == null)
        {
            return Ok(new BaseResponseModel { Success = false, ErrorMessage = "Not found" });
        }
        return Ok(new BaseResponseModel { Success = true, Data = model });
    }

    // POST: api/Tournament
    [HttpPost]
    public async Task<ActionResult<BaseResponseModel>> UpsertTournament(TournamentModel model)
    {
        if (model == null)
        {
            return Ok(new BaseResponseModel { Success = false, ErrorMessage = "No data" });
        }
        try
        {
            var newModel = await _repo.UpsertTournament(model);
            return Ok(new BaseResponseModel { Success = true, Data = newModel });
        }
        catch (Exception e)
        {
            return Ok(new BaseResponseModel { Success = false, ErrorMessage = e.Message });
        }
    }

    // POST: api/Tournament
    [HttpPost("active")]
    public async Task<ActionResult<BaseResponseModel>> SetActiveTournament([FromBody]int id)
    {
        var r = Request.Body;
        try
        {
            var newModel = await _repo.SetActiveTournament(id);
            return Ok(new BaseResponseModel { Success = true });
        }
        catch (Exception e)
        {
            return Ok(new BaseResponseModel { Success = false, ErrorMessage = e.Message });
        }
    }

    // DELETE: api/Tournament/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTournament(int id)
    {
        int i = await _repo.DeleteTournament( id );
        return NoContent();
    }
}