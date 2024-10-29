<<<<<<< HEAD:Alicante/Alicante/Controllers/BaseController.cs
﻿using Alicante.Data;
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


=======
﻿using Alicante.Data;
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


>>>>>>> 9613f99 (update):Alicante/Controllers/BaseController.cs
