using System;
using Microsoft.AspNetCore.Mvc;

namespace AccountPayable.API.Controllers
{
    [Route("api/[controller]")]
    //[TypeFilter(typeof(AuthorizationFilterAttribute))]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
    }
}

