using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WasteMan.Common.Data;
using WasteMan.Web.Api.Processors;

namespace WasteMan.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IResultProcessor _resultProcessor;
        public ResultController(IResultProcessor resultProcessor)
        {
            _resultProcessor = resultProcessor;
        }

        [HttpGet("{source}")]
        public async Task<ActionResult<ResultDto>> Get(string source)
        {
            var result = await _resultProcessor.Get(source);

            if (result is null)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet("{date:datetime}")]
        public async Task<ActionResult<ResultDto>> Get(DateTime date)
        {
            var result = await _resultProcessor.Get(date);

            if (result is null)
            {
                return NoContent();
            }

            return Ok(result);
        }  
    }
}