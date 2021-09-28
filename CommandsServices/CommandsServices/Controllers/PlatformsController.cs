using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandsServices.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController:ControllerBase

    {
        public PlatformsController()
        {

        }

        public ActionResult TestInboundRequest()
        {
            Console.WriteLine("incoming request");
            return Ok("Post Msg");
        }
    }
}
