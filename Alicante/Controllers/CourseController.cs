using Alicante.Client.Models;
using Alicante.Data;
using Microsoft.AspNetCore.Mvc;

namespace Alicante.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseController : BaseController
{
    public CourseController(IRepository repository) : base(repository)
    {
    }

    // GET: api/Course
    [HttpGet]
    public async Task<ActionResult<BaseResponseModel>> GetCourses()
    {
        var models = await _repo.FindAllCourse();
        return Ok(new BaseResponseModel { Success = true, Data = models });
    }

    // GET: api/Course/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponseModel>> GetCourse(int id)
    {
        var model = await _repo.GetCourse(id);
        if (model == null)
        {
            return Ok(new BaseResponseModel { Success = false, ErrorMessage = "Not found"});
        }
        return Ok(new BaseResponseModel { Success = true, Data = model });
    }

    // POST: api/Course
    [HttpPost]
    public async Task<ActionResult<BaseResponseModel>> UpsertCourse(CourseModel model)
    {
        if (model == null)
        {
            return Ok(new BaseResponseModel { Success = false, ErrorMessage = "No data" });
        }
        try
        {
            var newModel = await _repo.UpsertCourse(model);
            return Ok(new BaseResponseModel { Success = true, Data = newModel });
        }
        catch (Exception e)
        {
            return Ok(new BaseResponseModel { Success = false, ErrorMessage = e.Message });
        }
    }

    // DELETE: api/Course/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        int i = await _repo.DeleteCourse( id );
        if (i == 0)
        {
            return Ok(new BaseResponseModel { Success = false, ErrorMessage = "Not Found" });
        }
        return Ok(new BaseResponseModel { Success = true });
    }
}