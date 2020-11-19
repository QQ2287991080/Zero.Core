using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zero.Core.Tasks.Base;

namespace Zero.Core.WebApi.Controllers.test
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTestController1 : ControllerBase
    {
        readonly IExample _example;
        public JobTestController1(IExample example)
        {
            _example = example;
        }

        [HttpGet("Start")]
        public async Task<string> Start()
        {
            await _example.Start();
            return "Success";
        }

        [HttpGet("Run")]
        public async Task<string> Run()
        {
            await _example.Run();
            return "Success";
        }
        [HttpGet("PauseJob")]
        public async Task<string> PauseJob()
        {
            await _example.PauseJob();
            return "Success";
        }
        [HttpGet("ResumeJob")]
        public async Task<string> ResumeJob()
        {
            await _example.ResumeJob();
            return "Success";
        }

    }
}
