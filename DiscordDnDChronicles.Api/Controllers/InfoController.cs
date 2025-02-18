using Microsoft.AspNetCore.Mvc;

namespace DiscordDnDChronicles.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InfoController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok("Discord DnD Chronicles API is running!");
}
