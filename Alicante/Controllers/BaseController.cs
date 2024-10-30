using Alicante.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Alicante.Controllers;

public class BaseController : ControllerBase
{
    protected readonly IRepository _repo;
    protected IMapper _mapper;

    public BaseController(IRepository repository)
    {
        _repo = repository;
    }
}
