using Alicante.Client.Models;
using Alicante.Data;
using Microsoft.AspNetCore.Mvc;

namespace Alicante.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MatchController : BaseController
{
    public MatchController(IRepository repository) : base(repository)
    {
    }

    // GET: api/Match
    [HttpGet]
    public async Task<ActionResult<BaseResponseModel>> GetMatches()
    {
        var models = await _repo.FindAllMatch();
        return Ok(new BaseResponseModel { Success = true, Data = models });
    }

    // GET: api/Match/items
    [HttpGet("items")]
    public async Task<ActionResult<BaseResponseModel>> GetMatchItems()
    {
        var models = await _repo.FindAllMatch();
        return Ok(new BaseResponseModel
        {
            Success = true,
            Data = models.Select(m => new KeyValuePair<int, string>(
                m.MatchId, m.MatchText))
        });
    }

    // GET: api/Match/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponseModel>> GetMatch(int id)
    {
        var model = await _repo.GetMatch(id);
        if (model == null)
        {
            return Ok(new BaseResponseModel { Success = false, ErrorMessage = "Not found" });
        }
        return Ok(new BaseResponseModel { Success = false, Data = model });
    }

    // POST: api/Match
    [HttpPost]
    public async Task<ActionResult<BaseResponseModel>> UpsertMatch(MatchModel model)
    {
        if (model == null)
        {
            return Ok(new BaseResponseModel { Success = false, ErrorMessage = "No data" });
        }
        try
        {
            var newModel = await _repo.UpsertMatch(model);
            return Ok(new BaseResponseModel { Success = true, Data = newModel });
        }
        catch (Exception e)
        {
            return Ok(new BaseResponseModel { Success = false, ErrorMessage = e.Message });
        }
    }

    // DELETE: api/Match/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMatch(int id)
    {
        int i = await _repo.DeleteMatch( id );
        if (i == 0)
        {
            return NotFound();
        }
        return NoContent();
    }
}