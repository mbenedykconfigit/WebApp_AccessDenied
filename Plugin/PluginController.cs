using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Plugin
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class PluginController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("GET from custom plugin");
    }
}
