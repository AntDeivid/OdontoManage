using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace OdontoManage.API.Controllers;

[Route("ping")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class PingController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("pong");
    }
}