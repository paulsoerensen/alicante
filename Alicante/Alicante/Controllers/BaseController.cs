using Alicante.Data;
using Microsoft.AspNetCore.Mvc;

namespace Alicante.Controllers;

public class BaseController : ControllerBase
{
    protected readonly IRepository _repo;

    public BaseController(IRepository repository)
    {
        _repo = repository;
    }
}


