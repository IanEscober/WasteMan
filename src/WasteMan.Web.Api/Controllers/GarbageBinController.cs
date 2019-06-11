using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WasteMan.Common.Data;
using WasteMan.Web.Api.Processors;

namespace WasteMan.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GarbageBinController : ControllerBase
    {
        private readonly IGarbageBinProcessor _garbageBinProcessor;
        public GarbageBinController(IGarbageBinProcessor garbageBinProcessor)
        {
            _garbageBinProcessor = garbageBinProcessor;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GarbageBinDto>>> Get()
        {
            var bins = await _garbageBinProcessor.Get();

            if (bins is null)
            {
                return NoContent();
            }

            return Ok(bins);
        }

        [HttpGet("{date:datetime}")]
        public async Task<ActionResult<IEnumerable<GarbageBinDto>>> Get(DateTime date)
        {
            var bins = await _garbageBinProcessor.Get(date);

            if(bins is null)
            {
                return NoContent();
            }

            return Ok(bins);
        }
    }
}